using System;
using System.Threading;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PylonsIpc
{

    public class MainThreadDispatchManager : MonoBehaviour
    {
        private static event EventHandler recurring;
        private static event EventHandler oneShot;
        private static Mutex recurringDispatchEventMutex = new Mutex(false);
        private static Mutex oneShotDispatchEventMutex = new Mutex(false);

        void Update() => PumpEvents();

        private static void PumpEvents()
        {
            recurring?.Invoke(null, EventArgs.Empty);
            oneShot?.Invoke(null, EventArgs.Empty);
            oneShot = null;
        }

#if UNITY_EDITOR
    private static bool hooked = false;
    private static Thread unityThread;

    [UnityEditor.Callbacks.DidReloadScripts]
    private static void Hook()
    {
        if (!hooked)
        {
            unityThread = Thread.CurrentThread;
            EditorApplication.update += PumpEvents;
            hooked = true;
        }
    }
#endif

        public static void DispatchRecurring(EventHandler eventHandler)
        {
            recurringDispatchEventMutex.WaitOne();
            recurring += eventHandler;
            recurringDispatchEventMutex.ReleaseMutex();
        }

        public static void Dispatch(EventHandler eventHandler)
        {
            oneShotDispatchEventMutex.WaitOne();
            oneShot += eventHandler;
            oneShotDispatchEventMutex.ReleaseMutex();
        }
    }
}