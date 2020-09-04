using System;
using System.Collections.Generic;

namespace CrossPlatformIPC
{
    public class IPCInteraction
    {
        private static Queue<IPCInteraction> ipcInteractionDispatchQueue = new Queue<IPCInteraction>();

        public class IPCInteractionEventArgs : EventArgs
        {
            public readonly IPCInteraction interaction;

            public IPCInteractionEventArgs(IPCInteraction _interaction) => interaction = _interaction;
        }

        public event EventHandler<IPCInteractionEventArgs> OnSubmit;
        public event EventHandler<IPCInteractionEventArgs> OnResolution;
        protected string outgoingMessage = null;
        public string receivedMessage { get; private set; } = null;
        protected PassedException receivedError = null;
        protected virtual void PreSubmit() { }
        protected virtual void Resolution() { }
        protected virtual void Success() { }
        protected virtual void Failure() { }
        protected MessageEncoder encoder = MessageEncoder.Create();
        private bool awaitingResponseToSubmittedMessage = false;


        public IPCInteraction (string msg)
        {
            outgoingMessage = msg;
        }


        private void Submit ()
        {
            OnSubmit?.Invoke(this, new IPCInteractionEventArgs(this));
            PreSubmit();
            UnityEngine.Debug.Log($"Firing {GetType().Name}");
            encoder.Send(outgoingMessage);
            awaitingResponseToSubmittedMessage = true;
            IPCManager.PrepareToReceiveMessage((sender, args) => {
                UnityEngine.Debug.Log("Submit-time callback firing");
                awaitingResponseToSubmittedMessage = false;
                receivedMessage = args.message;
                if (receivedMessage != null) UnityEngine.Debug.Log("Received message");
                var evtArgs = new IPCInteractionEventArgs(this);
                Resolution();
                OnResolution?.Invoke(this, evtArgs);
                if (ipcInteractionDispatchQueue.Count > 0)
                {
                    UnityEngine.Debug.Log($"{ipcInteractionDispatchQueue.Count} messages remaining in queue; dispatching next");
                    ipcInteractionDispatchQueue.Dequeue().Submit();
                }
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
            else ipcInteractionDispatchQueue.Enqueue(this);
        }
    }
}

