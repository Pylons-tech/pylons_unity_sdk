using System;
using UnityEngine;
using CrossPlatformIPC;

public class IPCManager : MonoBehaviour
{
    public static IPCManager current { get; private set; }
    public static IPCTarget target { get; private set; }
    private bool anticipatingMessage;
    private MessageEncoder outgoingMessage;
    private event EventHandler<IncomingMessageEventArgs> onOutgoingMessageGet;
    private static event EventHandler onCurrentExists;

    public class IncomingMessageEventArgs : EventArgs
    {
        public readonly string message;

        public IncomingMessageEventArgs (string m)
        {
            message = m;
        }
    }

    void Awake ()
    {
#if !UNITY_WEBGL
        current = this;
        SetIpcTarget(IPCTarget.instance);
        onCurrentExists?.Invoke(this, EventArgs.Empty);
#endif
    }

    public static void SetIpcTarget (IPCTarget _target)
    {
        target = _target;
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidMessageEncoder.ConformAndroidSideToIpcTarget(target);
#endif
    }

    void OnDestroy()
    {
        if (current == this) current = null;
    }

    void Update ()
    {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        if (EditorMessageEncoder.IOEngine.exceptions.Count > 0)
        {
            Exception e = EditorMessageEncoder.IOEngine.exceptions.First.Value;
            EditorMessageEncoder.IOEngine.exceptions.RemoveFirst();
            if (e.GetType() != typeof(System.Threading.ThreadAbortException)) // ThreadAbortException is thrown to kill the thread, but it's not actually anything to worry about
            {
                Debug.LogError(e.StackTrace);
                throw e;
            }
        }
#endif
        if (anticipatingMessage) CheckIfIncomingMessageObtained();
    }

    private void CheckIfIncomingMessageObtained ()
    {
#if !UNITY_WEBGL
        string receivedMsg;
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        if (EditorMessageEncoder.TryReceive(out receivedMsg))
#elif UNITY_ANDROID
        if (AndroidMessageEncoder.TryReceive(out receivedMsg))
#endif

        {
            anticipatingMessage = false;
            onOutgoingMessageGet?.Invoke(this, new IncomingMessageEventArgs(receivedMsg));
            onOutgoingMessageGet = null;
        }
#endif
    }

    public static void PrepareToReceiveMessage (EventHandler<IncomingMessageEventArgs> msgCallback)
    {
#if !UNITY_WEBGL
        void action ()
        {
            current.onOutgoingMessageGet += msgCallback;
            current.anticipatingMessage = true;
        }
        Debug.Log($"have current ipc manager: {current != null}");
        if (current != null) action();
        else onCurrentExists += (s, e) => action();
#endif
    }
}
