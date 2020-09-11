using Newtonsoft.Json;

namespace PylonsSdk.Internal.Ipc.Messages
{
    public sealed class FulfillTrade : IpcMessage
    {
        [JsonProperty("tradeId")]
        public readonly string TradeId;
        [JsonProperty("itemIds")]
        public readonly string[] ItemIds;

        public FulfillTrade(string tradeId, string[] itemIds) : base(ResponseType.TX_RESPONSE)
        {
            TradeId = tradeId;
            ItemIds = itemIds;
        }
    }
}