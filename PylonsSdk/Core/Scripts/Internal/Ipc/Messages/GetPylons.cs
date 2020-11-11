using Newtonsoft.Json;

namespace PylonsSdk.Internal.Ipc.Messages
{
    /// <summary>
    /// IPC: Gets Pylons currency from the appropriate platform-specific endpoint using the linked wallet.
    /// Linked wallet must have a keypair. In most cases, this message will need to seize UI focus from a
    /// client application
    /// 
    /// Gets a TxResponse containing the created transaction. Note that the message type of the created
    /// transaction in this case is *not* fixed. Depending on the platform we're running on and the
    /// environment (local devnet vs. global testnet vs. live) we can resolve GetPylons with
    /// one of several different transaction message types.
    /// </summary>
    public sealed class GetPylons : IpcMessage
    {
        /// <summary>
        /// The number of Pylons to be gotten. Note that GetPylons isn't necessarily this granular in all cases.
        /// If the transaction layer for the current platform only enables users to obtain Pylons in a limited
        /// number of SKUs, we'll resolve this number to the smallest possible sku that gets us at least this
        /// many Pylons. If that's not possible (that is, we want more than the largest SKU) the operation
        /// will fail.
        /// </summary>
        [JsonProperty("count")]
        public readonly long Count;

        public GetPylons(long count) : base(ResponseType.TX_RESPONSE)
        {
            Count = count;
        }
    }
}