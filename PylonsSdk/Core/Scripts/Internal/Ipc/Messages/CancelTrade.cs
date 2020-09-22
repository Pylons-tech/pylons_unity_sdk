using Newtonsoft.Json;
using System;

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