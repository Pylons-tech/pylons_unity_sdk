using Newtonsoft.Json;
using PylonsSdk.Internal.Ipc;
using PylonsSdk.Internal.Ipc.Messages;
using System;

namespace PylonsSdk
{
    public static partial class Service
    {
        public static void GetTransaction(string txHash, params IpcEvent[] evts) => new GetTransaction(txHash).Broadcast(evts);
    }
}

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