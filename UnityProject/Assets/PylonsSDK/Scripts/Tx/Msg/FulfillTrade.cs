using Newtonsoft.Json;

namespace PylonsSDK.Tx.Msg
{
    public readonly struct FulfillTrade
    {
        [JsonProperty("TradeID")]
        public readonly string TradeId;
        [JsonProperty("Sender")]
        public readonly string Sender;
        [JsonProperty("ItemIDs")]
        public readonly string[] ItemIds;

        public FulfillTrade(string sender, string tradeId, string[] itemIds)
        {
            Sender = sender;
            TradeId = tradeId;
            ItemIds = itemIds;
        }
    }
}