using Newtonsoft.Json;
using PylonsSdk.Internal.Ipc;
using PylonsSdk.Internal.Ipc.Messages;
using System;

namespace PylonsSdk
{
    public static partial class Service
    {
        public static void CancelTrade(string tradeId, params IpcEvent[] evts) =>
            new CancelTrade(tradeId).Broadcast(evts);
    }
}

namespace PylonsSdk.Internal.Ipc.Messages
{
    public sealed class CancelTrade : IpcMessage
    {
        [JsonProperty("tradeId")]
        public readonly string TradeId;

        public CancelTrade(string tradeId) : base(ResponseType.TX_RESPONSE)
        {
            TradeId = tradeId ?? throw new ArgumentNullException(nameof(tradeId));
        }
    }
}