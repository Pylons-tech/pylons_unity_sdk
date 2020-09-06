using Newtonsoft.Json;

namespace PylonsSdk.Tx.Msg
{
    public readonly struct CreateTrade
    {
        [JsonProperty("CoinInputs")]
        public readonly CoinInput[] CoinInputs;
        [JsonProperty("ItemInputs")]
        public readonly TradeItemInput[] ItemInputs;
        [JsonProperty("CoinInputs")]
        public readonly CoinOutput[] CoinOutputs;
        [JsonProperty("ItemOutputs")]
        public readonly Item[] ItemOutputs;
        [JsonProperty("ExtraInfo")]
        public readonly string ExtraInfo;
        [JsonProperty("Sender")]
        public readonly string Sender;

        public CreateTrade(string sender, string extraInfo, CoinInput[] coinInputs, TradeItemInput[] itemInputs, CoinOutput[] coinOutputs, Item[] itemOutputs)
        {
            Sender = sender;
            ExtraInfo = extraInfo;
            CoinInputs = coinInputs;
            ItemInputs = itemInputs;
            CoinOutputs = coinOutputs;
            ItemOutputs = itemOutputs;
        }
    }
}