using System;
using System.Threading;

namespace PylonsIpc
{
    public abstract class IpcChannel
    {
        public readonly static int ClientId = new Random().Next();
        public static int HostId { get; protected set; }

        public static readonly Semaphore Semaphore = new Semaphore(1, 1);

        public abstract void Send(string message);

        public static IpcChannel Create ()
        {
#if UNITY_EDITOR || (UNITY_STANDALONE && DEBUG)
                    return new IpcChannelDebugHttp();
#elif UNITY_ANDROID
                    return new IpcChannelAndroid();
#else
                    throw new NotImplementedException();
#endif
        }
    }
}