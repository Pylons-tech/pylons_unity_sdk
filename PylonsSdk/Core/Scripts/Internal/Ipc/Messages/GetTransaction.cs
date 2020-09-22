using Newtonsoft.Json;
using System;

namespace PylonsSdk.Internal.Ipc.Messages
{
    public sealed class GetTransaction: IpcMessage
    {
        [JsonProperty("txHash")]
        public readonly string TxHash;

        public GetTransaction(string txHash) : base(ResponseType.TX_RESPONSE)
        {
            TxHash = txHash ?? throw new ArgumentNullException(nameof(txHash));
        }
    }
}