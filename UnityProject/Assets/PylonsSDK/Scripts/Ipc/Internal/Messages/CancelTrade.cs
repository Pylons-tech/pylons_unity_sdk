using Newtonsoft.Json;
using PylonsSDK.Ipc;
using PylonsSDK.Ipc.Internal.Messages;
using System;

namespace PylonsSDK
{
    public static partial class Service
    {
        public static void CancelTrade(string tradeId, params IpcEvent[] evts) =>
            new CancelTrade(tradeId).Broadcast(evts);
    }
}

namespace PylonsSDK.Ipc.Internal.Messages
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