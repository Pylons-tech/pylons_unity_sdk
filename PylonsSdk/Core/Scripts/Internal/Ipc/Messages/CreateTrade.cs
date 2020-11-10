using Newtonsoft.Json;

namespace PylonsSdk.Internal.Ipc.Messages
{
    /// <summary>
    /// IPC: Creates a trade using the linked wallet.
    /// Linked wallet must have a keypair.
    /// 
    /// Note that we're expected to actually have the coins and/or items we're putting up for trade when we send the message.
    /// If you don't, the message will be rejected.
    /// 
    /// Gets a TxResponse containing the created CreateTrade transaction.
    /// </summary>
    public sealed class CreateTrade : IpcMessage
    {
        /// <summary>
        /// An array of JSON strings corresponding to serialized CoinInput structs. PylonsSdk.Tx.CoinInput will serialize correctly.
        /// </summary>
        [JsonProperty("coinInputs")]
        public readonly string[] CoinInputs;
        /// <summary>
        /// An array of JSON strings corresponding to serialized TradeItemInput structs. PylonsSdk.Tx.TradeItemInput will serialize correctly.
        /// </summary>
        [JsonProperty("itemInputs")]
        public readonly string[] ItemInputs;
        /// <summary>
        /// An array of JSON strings corresponding to serialized CoinOutput structs. PylonsSdk.Tx.CoinOutput will serialize correctly.
        /// </summary>
        [JsonProperty("coinOutputs")]
        public readonly string[] CoinOutputs;
        /// <summary>
        /// An array of JSON strings corresponding to serialized Item structs. PylonsSdk.Tx.Item will serialize correctly.
        /// </summary>
        [JsonProperty("itemOutputs")]
        public readonly string[] ItemOutputs;
        /// <summary>
        /// A human-readable string containing a (generally end-user supplied) description of the trade in question.
        /// (TODO: this will be changed to 'description' once that happens on the node)
        /// </summary>
        [JsonProperty("extraInfo")]
        public readonly string ExtraInfo;

        public CreateTrade(string[] coinInputs, string[] itemInputs, string[] coinOutputs, string[] itemOutputs, string extraInfo) : base(ResponseType.TX_RESPONSE)
        {
            CoinInputs = coinInputs;
            ItemInputs = itemInputs;
            CoinOutputs = coinOutputs;
            ItemOutputs = itemOutputs;
            ExtraInfo = extraInfo;
        }
    }
}