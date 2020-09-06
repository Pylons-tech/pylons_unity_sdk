#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Net;
using System.IO;
using UnityEditor;
using System.Diagnostics;
using System.Security.AccessControl;
using Debug = UnityEngine.Debug;

namespace CrossPlatformIpc
{
    public class EditorMessageEncoder : MessageEncoder
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
            private TcpClient tcpClient;
            private Thread thread;
            public static LinkedList<Exception> exceptions = new LinkedList<Exception>();
            public static IOEngine Instance { get; private set; }
            public Process TargetProcess { get; private set; }
            public Process TargetHostProcess { get; private set; }
            private Queue<EventHandler> onReadyQueue = new Queue<EventHandler>();
            private FileSystemAccessRule accessRule = new FileSystemAccessRule("Users", FileSystemRights.FullControl, AccessControlType.Allow);
            public static bool AttachedToWallet => Instance?.TargetProcess != null && !Instance.TargetProcess.HasExited;
            public static string exportedResponse;

            private static bool TargetIsRunning => !Instance?.TargetProcess?.HasExited ?? false;

            static long totalBytesWritten = 0;

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
                AssemblyReloadEvents.beforeAssemblyReload += Instance.OnBeforeAssemblyReload;
            }

            public static void KillTarget() => Cleanup();

            public static void LaunchTarget()
            {
                Cleanup();
                Start();
            }

            public void OnBeforeAssemblyReload()
            {
                AssemblyReloadEvents.beforeAssemblyReload -= OnBeforeAssemblyReload;
                KillTarget();
            }
 

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
                        while (tcpClient?.Connected != true)
                        {
                            Debug.Log($"Connecting to 127.0.0.1:{IpcManager.target.devProcessComPort}...");

                            const double timeout = 10;

                            var lastRetry = DateTime.Now;
                            var timeoutDate = DateTime.Now.AddSeconds(timeout);
                            try
                            {
                                tcpClient = new TcpClient("127.0.0.1", IpcManager.target.devProcessComPort);
                            }
                            catch (Exception e)
                            {
                                if (DateTime.Now > timeoutDate)
                                {
                                    Debug.LogError("Couldn't connect to IPC target before timeout");
                                    throw e;
                                }
                            }
                        }
                        switch (state)
                        {
                            case State.WaitForHandshake:
                                if (CheckForHandshake()) state = State.Ready;
                                else Debug.Log("Handshake w/ server failed");
                                break;
                            case State.Ready:
                                if (onReady != null)
                                {
                                    onReady.Invoke(null, EventArgs.Empty);
                                    if (onReadyQueue.Count > 0) onReady = onReadyQueue.Dequeue();
                                    else onReady = null;
                                    state = State.AwaitingRespoonse;
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

            private bool NextMessageEqualsString(string str)
            {
                try
                {
                    byte[] m = ReadNext();
                    if (m == null) return false;
                    string s = Encoding.ASCII.GetString(m);
                    return s == str;
                }
                catch (Exception e)
                {
                    exceptions.AddFirst(e);
                    throw e;
                }
            }

            private bool CheckForHandshake()
            {
                Debug.Log("Connected!");
                byte[] m = ReadNext();
                if (m == null) return false;
                byte[] handshakeMagicBytes = Encoding.ASCII.GetBytes(HANDSHAKE_MAGIC);
                if (m.Length != handshakeMagicBytes.Length + 8)
                {
                    Debug.Log("Wrong msg length " + m.Length);
                    return false;
                }
                byte[] mHandshake = new byte[handshakeMagicBytes.Length];
                for (int i = 0; i < mHandshake.Length; i++) mHandshake[i] = m[i];
                for (int i = 0; i < mHandshake.Length; i++) if (mHandshake[i] != handshakeMagicBytes[i])
                {
                    Debug.Log("Wrong handshake");
                    return false;
                }
                int pid = (int)IPAddress.HostToNetworkOrder(BitConverter.ToInt64(m, mHandshake.Length));
                TargetProcess = Process.GetProcessById(pid);
                Debug.Log("Got handshake magic!");
                WriteBytes(Encoding.ASCII.GetBytes(HANDSHAKE_REPLY_MAGIC));
                return true;
            }

            private bool CheckForResponse(out string msg)
            {
                Debug.Log("Looking for incoming message...");
                msg = ReadMessageFromPipe();
                if (msg != null)
                {
                    Debug.Log("Got incoming message!");
                    LogIncomingMessageToFilesystem(msg);
                }
                return msg != null;
            }

            private byte[] ReadNext()
            {           
                try
                {
                    Debug.Log("Starting read");
                    NetworkStream s = tcpClient.GetStream();
                    while (s.ReadByte() == -1) Thread.Sleep(100);
                    byte[] buffer = new byte[1024 * 512]; // 512kb is overkill
                    int len = s.Read(buffer, 0, buffer.Length);
                    Debug.Log(len + " bytes down");
                    if (len == 0) return null;
                    byte[] m = new byte[len];
                    for (int i = 0; i < len; i++) m[i] = buffer[i];
                    return m;
                }
                catch (Exception e)
                {
                    exceptions.AddFirst(e);
                    throw e;
                }
            }

            private void WriteBytes(byte[] bytes)
            {
                try
                {
                    totalBytesWritten += bytes.Length;
                    Debug.Log($"Start write... ({bytes.Length}) bytes | {Encoding.ASCII.GetString(bytes)} | {totalBytesWritten}");
                    NetworkStream s = tcpClient.GetStream();
                    s.WriteByte(0xFF); // meaningless, just prepending message w/ an irrelevant byte so we can use ReadByte() to check for stream end w/o losing data
                    s.Write(bytes, 0, bytes.Length);
                    s.Flush();
                    Debug.Log("Write complete");
                }
                catch (Exception e)
                {
                    exceptions.AddFirst(e);
                    throw e;
                }
            }

            private string ReadMessageFromPipe() => Encoding.ASCII.GetString(ReadNext());

            private void SendMessageToPipe(string m)
            {
                LogOutgoingMessageToFilesystem(m);
                WriteBytes(Encoding.ASCII.GetBytes(m));
            }

            public void SendOncePossible (string msg)
            {
                EventHandler action = (s, e) => SendMessageToPipe(msg);
                if (onReady == null) onReady += action;
                else onReadyQueue.Enqueue(action);
            }

        }

        public override void Send(string message)
        {
            // Before a release-worthy Windows build of _anything_ can happen, this will need to be less stupid.
            // Right now it will never time out, which is fine for a dev app (start the wallet manually)
            // but obviously not great for end users.
            IOEngine.Instance.SendOncePossible(message);
        }

        public static bool TryReceive(out string receivedMsg)
        {
            bool r = IOEngine.exportedResponse != null;
            receivedMsg = IOEngine.exportedResponse;
            IOEngine.exportedResponse = null;
            return r;
        }
    }
}
#endif