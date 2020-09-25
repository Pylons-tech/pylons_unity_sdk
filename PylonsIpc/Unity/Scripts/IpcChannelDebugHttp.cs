using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Net;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Diagnostics;
using System.Security.AccessControl;
using Debug = UnityEngine.Debug;

namespace PylonsIpc
{
    internal class IpcChannelDebugHttp : IpcChannel
    {
        public class IOEngine
        {
            public enum State
            {
                None = 0,
                WaitForHandshake = 1,
                Ready = 2,
                AwaitingRespoonse = 3
            }

            public State state { get; private set; } = State.WaitForHandshake;
            private event EventHandler onReady;
            private Thread thread;
            public static LinkedList<Exception> exceptions = new LinkedList<Exception>();
            public static IOEngine Instance { get; private set; }
            public Process TargetProcess { get; private set; }
            public Process TargetHostProcess { get; private set; }
            private FileSystemAccessRule accessRule = new FileSystemAccessRule("Users", FileSystemRights.FullControl, AccessControlType.Allow);
            public static bool AttachedToWallet => Instance?.TargetProcess != null && !Instance.TargetProcess.HasExited;
            public static string exportedResponse;
            private byte[] lastSent;
            private bool busy;

            private static bool TargetIsRunning => !Instance?.TargetProcess?.HasExited ?? false;

            static IOEngine() => Start();

            private void LogIncomingMessageToFilesystem(string msg)
            {
                MainThreadDispatchManager.Dispatch((s, e) =>
                {
                    var path = Path.Combine(UnityEngine.Application.persistentDataPath,
                        "ipc/logging/incoming");
                    var filename = $"msg__{UnityEngine.Application.productName}__{DateTime.Now.Ticks.ToString()}.json";
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                    File.WriteAllText(Path.Combine(path, filename), msg);
                });
            }

            private void LogOutgoingMessageToFilesystem(string msg)
            {
                MainThreadDispatchManager.Dispatch((s, e) => 
                {
                    var path = Path.Combine(UnityEngine.Application.persistentDataPath, 
                        "ipc/logging/outgoing");
                    var filename = $"msg__{UnityEngine.Application.productName}__{DateTime.Now.Ticks.ToString()}.json";
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                    File.WriteAllText(Path.Combine(path, filename), msg);
                });
            }

            private static void Cleanup()
            {
                if (Instance != null)
                {
                    if (Instance.TargetProcess != null && !Instance.TargetProcess.HasExited) Instance.TargetProcess.Kill();
                    if (Instance.TargetHostProcess != null && !Instance.TargetHostProcess.HasExited) Instance.TargetHostProcess.Kill();
                    if (Instance.thread != null && Instance.thread.IsAlive) Instance.thread.Abort();
                    Instance = null;
                }
            }

            private static void Start()
            {
                if (Instance != null) return;
                Instance = new IOEngine();
                Instance.thread = new Thread(Instance.Main);
                Instance.thread.Start();
#if UNITY_EDITOR
                AssemblyReloadEvents.beforeAssemblyReload += Instance.OnBeforeAssemblyReload;
#endif
            }

            public static void KillTarget() => Cleanup();

            public static void LaunchTarget()
            {
                Cleanup();
                Start();
            }

#if UNITY_EDITOR
            public void OnBeforeAssemblyReload()
            {
                AssemblyReloadEvents.beforeAssemblyReload -= OnBeforeAssemblyReload;
                KillTarget();
            }
#endif

            private void StartTargetExe ()
            {
                Debug.Log(IpcManager.target);
                if (IpcManager.target.devProcessIsHosted)
                {
                    Debug.Log(IpcManager.target.devProcessHostedStartArguments);
                    TargetProcess = Process.Start(IpcManager.target.devProcessHostPath, IpcManager.target.devProcessHostedStartArguments);
                    TargetHostProcess = TargetProcess;
                }
                else
                {
                    TargetProcess = Process.Start(IpcManager.target.devProcessPath, IpcManager.target.devProcessArguments);
                    TargetHostProcess = null;
                }
            }

            private void Shutdown ()
            {
                KillTargetHostProcess();
                KillTargetProcess();
                KillThread();
            }

            private void KillThread ()
            {
                if (thread != null) thread.Abort();
                thread = null;
            }

            private void KillTargetProcess ()
            {
                if (TargetProcess != null) TargetProcess.Kill();
                TargetProcess = null;
            }

            private void KillTargetHostProcess ()
            {
                if (TargetHostProcess != null) TargetHostProcess.Kill();
                TargetHostProcess = null;
            }

            /// <summary>
            /// Terminates worker thread and hosted processes if the system is in an invalid state
            /// </summary>
            private void HealthCheck ()
            {
                if (thread != null && thread.IsAlive)
                {
                    switch (thread.ThreadState)
                    {
                        case System.Threading.ThreadState.Aborted:
                        case System.Threading.ThreadState.AbortRequested:
                        case System.Threading.ThreadState.Stopped:
                            Shutdown();
                            break;
                    }
                }
                else Shutdown();
                if (IpcManager.target.devProcessIsHosted && (TargetHostProcess == null || TargetHostProcess.HasExited || !TargetHostProcess.Responding)) Shutdown();
                if (IpcManager.target.devProcessIsHosted && (TargetHostProcess == null || TargetHostProcess.HasExited || !TargetHostProcess.Responding)) Shutdown();
            }

            private byte[] GetBytes(DateTime timeoutDate = default)
            {
                // HACK/TO-DO: This Thread.Sleep() dodges a timing bug. I'm not sure what the source of it is. This is obviously not a good
                // enough solution though, so we need to identify why DataAvailable below sometimes hangs if we don't
                // do this.
                Thread.Sleep(1000);
                const double timeout = 30;
                if (timeoutDate == default) timeoutDate = DateTime.Now.AddSeconds(timeout);
                try
                {
                    using (var tcpClient = new TcpClient("127.0.0.1", IpcManager.target.devProcessComPort))
                    {
                        Debug.Log($"Connecting to 127.0.0.1:{IpcManager.target.devProcessComPort}...");
                        Debug.Log("(GET)");
                        using (var s = tcpClient.GetStream())
                        {
                            Debug.Log("Connected!");
                            while (!s.DataAvailable)
                            {
                                Debug.Log("No data available; retry in 250ms");
                                Thread.Sleep(250);
                                if (DateTime.Now > timeoutDate) throw new TimeoutException("Timed out waiting for data");
                            }
                            Debug.Log("Starting read");
                            byte[] lenBuffer = new byte[4];
                            var l = s.Read(lenBuffer, 0, 4);
                            if (l != 4) throw new Exception($"expected a 32-bit integer (4 octets), got {l} bytes down instead");
                            var len = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(lenBuffer, 0));
                            Debug.Log($"Reading {len} bytes");
                            byte[] dataBuffer = new byte[len]; // 512kb is overkill
                            l = s.Read(dataBuffer, 0, dataBuffer.Length);
                            Debug.Log($"{l} bytes down!\n{Encoding.ASCII.GetString(dataBuffer)}");
                            if (l == 0) return null;
                            tcpClient.Close();
                            busy = false;
                            return dataBuffer;
                        }
                    }
                }
                catch (SocketException e)
                {
                    if (e.SocketErrorCode == SocketError.ConnectionRefused)
                    {
                        if (DateTime.Now > timeoutDate) throw new TimeoutException(e.Message);
                        else return GetBytes(timeoutDate);
                    }
                    else
                    {
                        busy = false;
                        throw e;
                    }
                }
                catch (TimeoutException e)
                {
                    Debug.LogError("Couldn't connect to IPC target before timeout");
                    busy = false;
                    throw e;
                }
                catch (Exception e)
                {
                    exceptions.AddFirst(e);
                    busy = false;
                    throw e;
                }
            }

            private void SendBytes(byte[] bytes, DateTime timeoutDate = default)
            {
                const double timeout = 10;
                if (timeoutDate == default) timeoutDate = DateTime.Now.AddSeconds(timeout);
                try
                {           
                    Debug.Log($"Connecting to 127.0.0.1:{IpcManager.target.devProcessComPort}...");
                    Debug.Log("(SEND)");
                    using (var tcpClient = new TcpClient("127.0.0.1", IpcManager.target.devProcessComPort))
                    {
                        Debug.Log("Connected!");
                        using (var s = tcpClient.GetStream())
                        {
                            // send bytes
                            byte[] lenBytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(bytes.Length));
                            byte[] data = new byte[lenBytes.Length + bytes.Length];
                            lenBytes.CopyTo(data, 0);
                            bytes.CopyTo(data, lenBytes.Length);
                            Debug.Log($"Start write... ({data.Length}) bytes | {Encoding.ASCII.GetString(bytes)}");
                            s.Write(data, 0, data.Length);
                            s.Flush();
                            Debug.Log("Write complete");
                        }
                    }
                }
                catch (SocketException e)
                {
                    if (e.SocketErrorCode == SocketError.ConnectionRefused)
                    {
                        if (DateTime.Now > timeoutDate) throw new TimeoutException(e.Message);
                        else SendBytes(bytes, timeoutDate);
                    }
                    else
                    {
                        busy = false;
                        throw e;
                    }
                }
                catch (TimeoutException e)
                {
                    Debug.LogError("Couldn't connect to IPC target before timeout");
                    busy = false;
                    throw e;
                }
                catch (Exception e)
                {
                    exceptions.AddFirst(e);
                    busy = false;
                    throw e;
                }
            }

            private void Main ()
            {
                try
                {
                    while (true)
                    {
                        if (!TargetIsRunning)
                        {
                            if (state == State.AwaitingRespoonse)
                            {
                                thread?.Abort();
                                throw new Exception("Unrecoverable error state - target app was closed while awaiting a response.");
                            }
                            state = State.WaitForHandshake;
                            StartTargetExe();
                        }
                        switch (state)
                        {
                            case State.WaitForHandshake:
                                if (CheckForHandshake())
                                {
                                    state = State.Ready;                      
                                }
                                else Debug.LogError("Handshake w/ server failed");
                                break;
                            case State.Ready:
                                if (busy || IpcInteraction.awaitingResponseToSubmittedMessage) continue;
                                IpcInteraction.ProcessQueue();
                                if (onReady != null)
                                {
                                    var del = onReady.GetInvocationList()[0] as EventHandler;
                                    onReady -= del;
                                    del.Invoke(this, EventArgs.Empty);
                                    busy = true;
                                }
                                else Thread.Sleep(250);
                                break;
                            case State.AwaitingRespoonse:
                                string response;
                                if (CheckForResponse(out response)) ExportResponse(response);
                                else Thread.Sleep(250);
                                break;
                        }
                    }
                }
                catch (Exception e)
                {
                    exceptions.AddLast(e);
                    Debug.LogError(e.StackTrace);
                    throw e;
                }         
            }

            private void ExportResponse (string msg)
            {
                Debug.Log($"Exporting response:\n{msg}");
                exportedResponse = msg;
                state = State.Ready;
            }

            private const string HANDSHAKE_MAGIC = "__PYLONS_WALLET_SERVER";
            private const string HANDSHAKE_REPLY_MAGIC = "__PYLONS_WALLET_CLIENT";

            private bool CheckForHandshake()
            {
                byte[] m = GetBytes();
                if (m == null) return false;
                byte[] handshakeMagicBytes = Encoding.ASCII.GetBytes(HANDSHAKE_MAGIC);
                var lenExpected = handshakeMagicBytes.Length + 8 + 4;
                if (m.Length != lenExpected)
                {
                    Debug.Log($"Wrong length - got {m.Length}; expected {lenExpected}");
                    return false;
                }
                byte[] mHandshake = new byte[handshakeMagicBytes.Length];
                for (int i = 0; i < mHandshake.Length; i++) mHandshake[i] = m[i];
                for (int i = 0; i < mHandshake.Length; i++) if (mHandshake[i] != handshakeMagicBytes[i])
                {
                    Debug.Log("Wrong handshake");
                    return false;
                }
                Debug.Log("Got handshake magic!");
                HostId = IPAddress.HostToNetworkOrder(BitConverter.ToInt32(m, mHandshake.Length));
                Debug.Log("Got wallet ID!");
                int pid = (int)IPAddress.HostToNetworkOrder(BitConverter.ToInt64(m, mHandshake.Length + 4));
                Debug.Log("Got PID!");
                TargetProcess = Process.GetProcessById(pid);
                ExecuteHandshakeReply();
                Debug.Log("Accepted?");
                return HandshakeAccepted();
            }

            private bool HandshakeAccepted() => Encoding.ASCII.GetString(GetBytes()) == "OKfillerfillerfillerfillerfillerfiller";

            private void ExecuteHandshakeReply()
            {
                Debug.Log("Sending reply!");
                SendBytes(Encoding.ASCII.GetBytes($"{HANDSHAKE_REPLY_MAGIC}{ClientId}"));
                Debug.Log($"{HANDSHAKE_REPLY_MAGIC}{ClientId}");
            }

            private bool CheckForResponse(out string msg)
            {
                Debug.Log("Awaiting response...");
                msg = ReadMessageFromPipe();
                if (msg != null)
                {
                    Debug.Log("Got response!");
                    LogIncomingMessageToFilesystem(msg);
                }
                return msg != null;
            }

            private string ReadMessageFromPipe() => Encoding.ASCII.GetString(GetBytes());

            private void SendMessageToPipe(string m)
            {
                LogOutgoingMessageToFilesystem(m);
                SendBytes(Encoding.ASCII.GetBytes(m));
                state = State.AwaitingRespoonse;
            }

            public void SendOncePossible(string msg) => onReady += (s, e) => SendMessageToPipe(msg);
        }

        public override void Send(string message) => IOEngine.Instance.SendOncePossible(message);

        public static bool TryReceive(out string receivedMsg)
        {
            bool r = IOEngine.exportedResponse != null;
            receivedMsg = IOEngine.exportedResponse;
            IOEngine.exportedResponse = null;
            return r;
        }
    }
}