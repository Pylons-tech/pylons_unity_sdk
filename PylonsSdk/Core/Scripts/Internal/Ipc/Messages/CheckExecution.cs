using Newtonsoft.Json;
using System;

namespace PylonsSdk.Internal.Ipc.Messages
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