using System;
using Newtonsoft.Json;
using PylonsSDK.Ipc;
using PylonsSDK.Ipc.Internal.Messages;

namespace PylonsSDK
{
    public static partial class Service
    {
        public static void FulfillTrade(string tradeId, params IpcEvent[] evts) =>
            new FulfillTrade(tradeId).Broadcast(evts);
    }
}

namespace PylonsSDK.Ipc.Internal.Messages
{
    public sealed class FulfillTrade : IpcMessage
    {
        [JsonProperty("tradeId")]
        public readonly string TradeId;
        [JsonProperty("itemIds")]
        public readonly string[] ItemIds;

        public FulfillTrade(string tradeId) : base(ResponseType.TX_RESPONSE)
        {
            TradeId = tradeId ?? throw new ArgumentNullException(nameof(tradeId));
        }
    }
}