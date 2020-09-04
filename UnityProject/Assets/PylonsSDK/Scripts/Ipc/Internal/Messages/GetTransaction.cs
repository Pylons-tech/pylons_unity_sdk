using Newtonsoft.Json;
using PylonsSDK.Ipc;
using PylonsSDK.Ipc.Internal.Messages;
using System;

namespace PylonsSDK
{
    public static partial class Service
    {
        public static void GetTransaction(string txHash, params IpcEvent[] evts) => new GetTransaction(txHash).Broadcast(evts);
    }
}

namespace PylonsSDK.Ipc.Internal.Messages
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