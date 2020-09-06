using Newtonsoft.Json;

namespace PylonsSdk.Tx.Msg
{
    public readonly struct EnableTrade
    {
        [JsonProperty("TradeID")]
        public readonly string TradeId;
        [JsonProperty("Sender")]
        public readonly string Sender;

        public EnableTrade(string sender, string tradeId)
        {
            Sender = sender;
            TradeId = tradeId;
        }
    }
}