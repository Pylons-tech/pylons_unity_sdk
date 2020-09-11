using Newtonsoft.Json;
using PylonsSdk.Tx;

namespace PylonsSdk.Internal.Ipc
{
    public readonly struct TradeResponse
    {
        [JsonProperty("trades")]
        public readonly Trade[] Trades;

        public TradeResponse(Trade[] trades)
        {
            Trades = trades;
        }
    }
}