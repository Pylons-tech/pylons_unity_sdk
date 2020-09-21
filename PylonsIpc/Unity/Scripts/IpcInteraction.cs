using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PylonsIpc
{
    public class IpcInteraction
    {
        private static Queue<IpcInteraction> ipcInteractionDispatchQueue = new Queue<IpcInteraction>();

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
        protected virtual void PreSubmit() { }
        protected virtual void Resolution() { }
        protected virtual void Success() { }
        protected virtual void Failure() { }
        protected MessageEncoder encoder = MessageEncoder.Create();
        private bool awaitingResponseToSubmittedMessage = false;


        public IpcInteraction (dynamic msg)
        {
            outgoingMessage = msg;
        }


        private void Submit ()
        {
            OnSubmit?.Invoke(this, new IpcInteractionEventArgs(this));
            PreSubmit();
            var json = JsonConvert.SerializeObject(outgoingMessage);
            UnityEngine.Debug.Log($"Firing: {json}");
            encoder.Send(json);
            awaitingResponseToSubmittedMessage = true;
            IpcManager.PrepareToReceiveMessage((sender, args) => {
                UnityEngine.Debug.Log("Submit-time callback firing");
                awaitingResponseToSubmittedMessage = false;
                receivedMessage = args.message;
                if (receivedMessage != null) UnityEngine.Debug.Log("Received message");
                var evtArgs = new IpcInteractionEventArgs(this);
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

