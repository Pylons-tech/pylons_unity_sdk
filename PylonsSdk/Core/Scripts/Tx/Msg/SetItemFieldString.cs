using Newtonsoft.Json;

namespace PylonsSdk.Tx.Msg
{
    public readonly struct SetItemFieldString
    {
        [JsonProperty("Field")]
        public readonly string Field;
        [JsonProperty("Value")]
        public readonly string Value;
        [JsonProperty("Sender")]
        public readonly string Sender;
        [JsonProperty("ItemID")]
        public readonly string ItemId;

        public SetItemFieldString(string sender, string itemId, string field, string value)
        {
            Sender = sender;
            ItemId = itemId;
            Field = field;
            Value = value;
        }
    }
}