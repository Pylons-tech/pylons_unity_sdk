using System;
using UnityEngine;

namespace CrossPlatformIPC
{
    public abstract class MessageEncoder
    {
        public abstract void Send(string message);

        public static MessageEncoder Create ()
        {
#if UNITY_EDITOR
                    return new EditorMessageEncoder();
#elif UNITY_ANDROID
                    return AndroidMessageEncoder.Prepare();
#else
                    throw new NotImplementedException();
#endif
        }
    }
}