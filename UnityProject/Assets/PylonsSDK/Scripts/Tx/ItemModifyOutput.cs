using Newtonsoft.Json;

namespace PylonsSDK.Tx
{
    public readonly struct ItemModifyOutput
    {
        [JsonProperty("ID")]
        public readonly string Id;
        [JsonProperty("ItemInputRef")]
        public readonly string ItemInputRef;
        [JsonProperty("Doubles")]
        public readonly DoubleInputParam[] Doubles;
        [JsonProperty("Longs")]
        public readonly LongInputParam[] Longs;
        [JsonProperty("Strings")]
        public readonly StringInputParam[] Strings;
        [JsonProperty("TransferFee")]
        public readonly long TransferFee;

        public ItemModifyOutput(string id, string itemInputRef, DoubleInputParam[] doubles, LongInputParam[] longs, StringInputParam[] strings, long transferFee)
        {
            Id = id;
            ItemInputRef = itemInputRef;
            Doubles = doubles;
            Longs = longs;
            Strings = strings;
            TransferFee = transferFee;
        }
    }
}