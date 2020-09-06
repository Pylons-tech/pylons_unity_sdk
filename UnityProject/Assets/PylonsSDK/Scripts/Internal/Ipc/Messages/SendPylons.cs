using Newtonsoft.Json;
using PylonsSdk.Internal.Ipc;
using PylonsSdk.Internal.Ipc.Messages;
using System;

namespace PylonsSdk
{
    public static partial class Service
    {
        public static void SendPylons(string coins, string receiver, params IpcEvent[] evts) => new SendCoins(coins, receiver).Broadcast(evts);
    }
}

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