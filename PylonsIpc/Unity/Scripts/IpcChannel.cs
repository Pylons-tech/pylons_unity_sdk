using System;

namespace PylonsIpc
{
    public abstract class IpcChannel
    {
        public readonly static int ClientId = new System.Random().Next();
        public static int HostId { get; protected set; }

        public static bool Ready { get; private set; }

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