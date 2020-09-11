using Newtonsoft.Json;

namespace PylonsSdk.Tx
{
    public readonly struct TxDataOutput
    {
        [JsonProperty("type")]
        public readonly string Type;
        [JsonProperty("coin")]
        public readonly string Coin;
        [JsonProperty("amount")]
        public readonly long Amount;
        [JsonProperty("itemId")]
        public readonly string ItemId;
    }
}