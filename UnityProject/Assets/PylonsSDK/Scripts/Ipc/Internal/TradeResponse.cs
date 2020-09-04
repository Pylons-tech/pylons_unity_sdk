using Newtonsoft.Json;
using PylonsSDK.Tx;

namespace PylonsSDK.Ipc.Internal
{
    public readonly struct TradeResponse
    {
        [JsonProperty("trades")]
        public readonly Trade[] Trades;
    }
}