using Newtonsoft.Json;
using System;

namespace PylonsSdk.Internal.Ipc.Messages
{
    /// <summary>
    /// IPC: Fetch specified transaction using the linked wallet.
    /// 
    /// Gets a TxResponse containing that transaction.
    /// </summary>
    public sealed class GetTransaction: IpcMessage
    {
        /// <summary>
        /// The hash of the transaction to retrieve.
        /// </summary>
        [JsonProperty("txHash")]
        public readonly string TxHash;

        public GetTransaction(string txHash) : base(ResponseType.TX_RESPONSE)
        {
            TxHash = txHash ?? throw new ArgumentNullException(nameof(txHash));
        }
    }
}