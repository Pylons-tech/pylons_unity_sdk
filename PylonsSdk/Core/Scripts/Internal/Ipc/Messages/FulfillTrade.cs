using Newtonsoft.Json;

namespace PylonsSdk.Internal.Ipc.Messages
{
    /// <summary>
    /// IPC: Uses the linked wallet to fulfill the given trade.
    /// 
    /// Gets a TxResponse containing the created FulfillTrade transaction.
    /// </summary>
    public sealed class FulfillTrade : IpcMessage
    {
        /// <summary>
        /// UUID of the trade to be fulfilled.
        /// </summary>
        [JsonProperty("tradeId")]
        public readonly string TradeId;
        /// <summary>
        /// An array of item UUIDs. These items must satisfy all of the trade's item input requirements.
        /// These do not needto be in any specific order; the node will handle matching them to the trade's 
        /// inputs on its own.
        /// </summary>
        [JsonProperty("itemIds")]
        public readonly string[] ItemIds;

        public FulfillTrade(string tradeId, string[] itemIds) : base(ResponseType.TX_RESPONSE)
        {
            TradeId = tradeId;
            ItemIds = itemIds;
        }
    }
}