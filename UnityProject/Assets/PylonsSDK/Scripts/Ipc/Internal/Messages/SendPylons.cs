using Newtonsoft.Json;
using PylonsSDK.Ipc;
using PylonsSDK.Ipc.Internal.Messages;
using System;

namespace PylonsSDK
{
    public static partial class Service
    {
        public static void SendPylons(string coins, string receiver, params IpcEvent[] evts) => new SendCoins(coins, receiver).Broadcast(evts);
    }
}

namespace PylonsSDK.Ipc.Internal.Messages
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