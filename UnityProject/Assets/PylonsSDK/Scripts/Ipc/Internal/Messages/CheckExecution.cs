using Newtonsoft.Json;
using PylonsSDK.Ipc;
using PylonsSDK.Ipc.Internal.Messages;
using System;

namespace PylonsSDK
{
    public static partial class Service
    {
        public static void CheckExecution(string id, bool payForCompletion, params IpcEvent[] evts) =>
            new CheckExecution(id, payForCompletion).Broadcast(evts);
    }
}

namespace PylonsSDK.Ipc.Internal.Messages
{
    public sealed class CheckExecution : IpcMessage
    {
        [JsonProperty("id")]
        public readonly string Id;
        [JsonProperty("payForCompletion")]
        public readonly bool PayForCompletion;

        public CheckExecution(string id, bool payForCompletion) : base(ResponseType.TX_RESPONSE)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            PayForCompletion = payForCompletion;
        }
    }
}