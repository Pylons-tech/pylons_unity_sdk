using Newtonsoft.Json;

namespace PylonsSdk.Internal.Ipc.Messages
{
    /// <summary>
    /// IPC: Uses the linked wallet to cancel the trade with the given UUID.
    /// Linked wallet must have a keypair.
    /// The trade must, of course, be active, uncompleted,
    /// and created by the current active keypair.
    /// 
    /// Gets a TxResponse containing the created CancelTrade traansaction.
    /// </summary>
    public sealed class CancelTrade : IpcMessage
    {
        /// <summary>
        /// UUID of the trade to be canceled.
        /// </summary>
        [JsonProperty("tradeId")]
        public readonly string TradeId;

        public CancelTrade(string tradeId) : base(ResponseType.TX_RESPONSE)
        {
            TradeId = tradeId;
        }
    }
}