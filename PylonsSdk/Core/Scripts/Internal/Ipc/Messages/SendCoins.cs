using Newtonsoft.Json;
using System;

namespace PylonsSdk.Internal.Ipc.Messages
{
    public sealed class SendCoins : IpcMessage
    {
        [JsonProperty("coins")]
        public readonly string Coins;
        [JsonProperty("receiver")]
        public readonly string Receiver;

        public SendCoins(string coins, string receiver) : base(ResponseType.TX_RESPONSE)
        {
            Coins = coins ?? throw new ArgumentNullException(nameof(coins));
            Receiver = receiver ?? throw new ArgumentNullException(nameof(receiver));
        }
    }
}