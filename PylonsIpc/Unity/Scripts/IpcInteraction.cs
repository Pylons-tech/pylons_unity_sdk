using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace PylonsIpc
{
    public class IpcInteraction
    {
        public static void Stage (Func<IBroadcastable> action, params IpcEvent[] events)
        {
            actionQueue.Enqueue(action);
            argsQueue.Enqueue(events);
        }

        private static Queue<Func<IBroadcastable>> actionQueue = new Queue<Func<IBroadcastable>>();
        private static Queue<IpcEvent[]> argsQueue = new Queue<IpcEvent[]>();

        public class IpcInteractionEventArgs : EventArgs
        {
            public readonly IpcInteraction interaction;

            public IpcInteractionEventArgs(IpcInteraction _interaction) => interaction = _interaction;
        }

        public event EventHandler<IpcInteractionEventArgs> OnSubmit;
        public event EventHandler<IpcInteractionEventArgs> OnResolution;
        protected dynamic outgoingMessage = null;
        public string receivedMessage { get; private set; } = null;
        protected PassedException receivedError = null;
        protected IpcChannel encoder = IpcChannel.Create();
        public static bool awaitingResponseToSubmittedMessage = false;


        public IpcInteraction (dynamic msg)
        {
            outgoingMessage = msg;
        }

        private void Submit ()
        {
            OnSubmit?.Invoke(this, new IpcInteractionEventArgs(this));
            var json = JsonConvert.SerializeObject(outgoingMessage);
            encoder.Send(json);
            awaitingResponseToSubmittedMessage = true;
            IpcManager.PrepareToReceiveMessage((sender, args) => {
                receivedMessage = args.message;
                if (receivedMessage != null) UnityEngine.Debug.Log("Got response to originating message");
                var evtArgs = new IpcInteractionEventArgs(this);
                UnityEngine.Debug.Log(json);
                OnResolution += (s, e) =>
                {
                    awaitingResponseToSubmittedMessage = false;
                    IpcChannel.Semaphore.Release();
                };
                OnResolution.Invoke(this, evtArgs);
            });
        }

        /// <summary>
        /// Stages the IPCInteraction to be submitted. (It will be submitted immediately,
        /// unless awaiting a response from a prior IPCInteraction.)
        /// Modifying the state of an IPCInteraction externally after calling Resolve may 
        /// result in erratic behavior, so, like, don't do it?
        /// </summary>
        public void Resolve ()
        {
            if (!awaitingResponseToSubmittedMessage) Submit();
            else throw new Exception("Tried to resolve an IpcInteraction while still awaiting response to last one!");
        }

        public static void ProcessQueue()
        {
            if (actionQueue.Count > 0 && IpcChannel.Semaphore.WaitOne())
            {
                UnityEngine.Debug.Log($"{actionQueue.Count} messages remaining in queue; dispatching next");
                actionQueue.Dequeue()().Broadcast(argsQueue.Dequeue());
            }
        }
    }
}

