using Newtonsoft.Json;

namespace PylonsSdk.Internal.Ipc.Messages
{
    /// <summary>
    /// IPC: Set the value of a string property on an existing item owned by the current keypair using linked wallet.
    /// Requires linked wallet to have active keypair. If item not owned by current keypair, operation fails.
    /// </summary>
    public sealed class SetItemString : IpcMessage
    {
        /// <summary>
        /// The UUID of the item.
        /// </summary>
        [JsonProperty("itemId")]
        public readonly string ItemId;
        /// <summary>
        /// The name of the field to modify.
        /// </summary>
        [JsonProperty("field")]
        public readonly string Field;
        /// <summary>
        /// The new value of that field.
        /// </summary>
        [JsonProperty("value")]
        public readonly string Value;

        public SetItemString(string itemId, string field, string value) : base(ResponseType.TX_RESPONSE)
        {
            ItemId = itemId;
            Field = field;
            Value = value;
        }
    }
}