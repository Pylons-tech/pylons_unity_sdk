using Newtonsoft.Json;

namespace PylonsSdk.Tx.Msg
{
    public readonly struct DisableTrade
    {
        [JsonProperty("TradeID")]
        public readonly string TradeId;
        [JsonProperty("Sender")]
        public readonly string Sender;

        public DisableTrade(string sender, string tradeId)
        {
            Sender = sender;
            TradeId = tradeId;
        }
    }
}