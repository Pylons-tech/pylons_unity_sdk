using System;
using UnityEngine;
using Debug = UnityEngine.Debug;
using System.Diagnostics;

namespace PylonsIpc
{
    [ExecuteAlways]
    public class IpcManager : MonoBehaviour
    {
        public static IpcManager instance { get; private set; }
        public static IpcTarget target { get; private set; }
        private bool anticipatingMessage;
        private IpcChannel outgoingMessage;
        private event EventHandler<IncomingMessageEventArgs> onOutgoingMessageGet;
        private static event EventHandler onCurrentExists;

        public class IncomingMessageEventArgs : EventArgs
        {
            public readonly string message;

            public IncomingMessageEventArgs(string m)
            {
                message = m;
            }
        }

        void Initialize()
        {
            if (instance != null)
            {
                if (instance == this) return;
                else throw new Exception("can't initialize a new ipcmanager when one already exists");
            }
            instance = this;
            SetIpcTarget(IpcTarget.instance);
            if (Application.isPlaying) DontDestroyOnLoad(this);
            onCurrentExists?.Invoke(this, EventArgs.Empty);
        }

        void Awake()
        {
            if (instance == null) Initialize();
        }

        public static void SetIpcTarget(IpcTarget _target)
        {
            target = _target;
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidMessageEncoder.ConformAndroidSideToIpcTarget(target);
#endif
        }

        void OnEnable()
        {
            if (instance == null) Initialize();
        }

        void OnDestroy()
        {
            if (instance == this) instance = null;
        }

        void Update()
        {
#if UNITY_EDITOR && (UNITY_STANDALONE && DEBUG)
        if (instance == null) Awake(); // we have to call Awake() manually bc unity doesn't call it consistently in editor
        if (IpcChannelDebugHttp.IOEngine.exceptions.Count > 0)
        {
            Exception e = IpcChannelDebugHttp.IOEngine.exceptions.First.Value;
            IpcChannelDebugHttp.IOEngine.exceptions.RemoveFirst();
            if (e.GetType() != typeof(System.Threading.ThreadAbortException)) // ThreadAbortException is thrown to kill the thread, but it's not actually anything to worry about
            {
                Debug.LogError(e.StackTrace);
                throw e;
            }
        }
#endif
            if (anticipatingMessage) CheckIfIncomingMessageObtained();
        }

        private void CheckIfIncomingMessageObtained()
        {
#if !UNITY_WEBGL
            string receivedMsg;
#if UNITY_EDITOR || (UNITY_STANDALONE && DEBUG)
        if (IpcChannelDebugHttp.TryReceive(out receivedMsg))
#elif UNITY_ANDROID
        if (AndroidMessageEncoder.TryReceive(out receivedMsg))
#endif

            {
                anticipatingMessage = false;
                if (onOutgoingMessageGet != null) UnityEngine.Debug.Log(new StackTrace());
                Debug.Log(receivedMsg.GetType());
                onOutgoingMessageGet?.Invoke(this, new IncomingMessageEventArgs(receivedMsg));
                onOutgoingMessageGet = null;
            }
#endif
        }

        public static void PrepareToReceiveMessage(EventHandler<IncomingMessageEventArgs> msgCallback)
        {
#if !UNITY_WEBGL
            void action()
            {
                instance.onOutgoingMessageGet += msgCallback;
                instance.anticipatingMessage = true;
            }
            if (instance != null) action();
            else onCurrentExists += (s, e) => action();
#endif
        }
    }
}