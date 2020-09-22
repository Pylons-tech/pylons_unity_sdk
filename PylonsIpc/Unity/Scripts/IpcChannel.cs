using System;
using UnityEngine;

namespace PylonsIpc
{
    public abstract class IpcChannel
    {
        public static bool Ready { get; private set; }

        public abstract void Send(string message);

        public static IpcChannel Create ()
        {
#if UNITY_EDITOR || (UNITY_STANDALONE && DEBUG)
                    return new IpcChannelDebugHttp();
#elif UNITY_ANDROID
                    return AndroidMessageEncoder.Prepare();
#else
                    throw new NotImplementedException();
#endif
        }
    }
}