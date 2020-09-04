﻿using Newtonsoft.Json;
using PylonsSDK.Ipc;
using PylonsSDK.Ipc.Internal.Messages;
using System;

namespace PylonsSDK
{
    public static partial class Service
    {
        public static void CreateTrade(string[] coinInputs, string[] itemInputs, string[] coinOutputs, string[] itemOutputs, string extraInfo, params IpcEvent[] evts) =>
            new CreateTrade(coinInputs, itemInputs, coinOutputs, itemOutputs, extraInfo).Broadcast(evts);
    }
}

namespace PylonsSDK.Ipc.Internal.Messages
{
    public sealed class CreateTrade : IpcMessage
    {
        [JsonProperty("coinInputs")]
        public readonly string[] CoinInputs;
        [JsonProperty("itemInputs")]
        public readonly string[] ItemInputs;
        [JsonProperty("coinOutputs")]
        public readonly string[] CoinOutputs;
        [JsonProperty("itemOutputs")]
        public readonly string[] ItemOutputs;
        [JsonProperty("extraInfo")]
        public readonly string ExtraInfo;

        public CreateTrade(string[] coinInputs, string[] itemInputs, string[] coinOutputs, string[] itemOutputs, string extraInfo) : base(ResponseType.TX_RESPONSE)
        {
            CoinInputs = coinInputs ?? throw new ArgumentNullException(nameof(coinInputs));
            ItemInputs = itemInputs ?? throw new ArgumentNullException(nameof(itemInputs));
            CoinOutputs = coinOutputs ?? throw new ArgumentNullException(nameof(coinOutputs));
            ItemOutputs = itemOutputs ?? throw new ArgumentNullException(nameof(itemOutputs));
            ExtraInfo = extraInfo ?? throw new ArgumentNullException(nameof(extraInfo));
        }
    }
}