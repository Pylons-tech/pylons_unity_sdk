using Newtonsoft.Json;
using PylonsSDK.Ipc;
using PylonsSDK.Ipc.Internal.Messages;
using System;

namespace PylonsSDK
{
    public static partial class Service
    {
        public static void SetItemString(string itemId, string field, string value, params IpcEvent[] evts) => new SetItemString(itemId, field, value).Broadcast(evts);
    }
}

namespace PylonsSDK.Ipc.Internal.Messages
{
    public sealed class SetItemString : IpcMessage
    {
        [JsonProperty("itemId")]
        public readonly string ItemId;
        [JsonProperty("field")]
        public readonly string Field;
        [JsonProperty("value")]
        public readonly string Value;

        public SetItemString(string itemId, string field, string value) : base(ResponseType.TX_RESPONSE)
        {
            ItemId = itemId ?? throw new ArgumentNullException(nameof(itemId));
            Field = field ?? throw new ArgumentNullException(nameof(field));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}