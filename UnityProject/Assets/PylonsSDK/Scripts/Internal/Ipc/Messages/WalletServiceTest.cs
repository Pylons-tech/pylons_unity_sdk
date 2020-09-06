using Newtonsoft.Json;
using PylonsSdk.Internal.Ipc;
using PylonsSdk.Internal.Ipc.Messages;
using System;

namespace PylonsSdk
{
    public static partial class Service
    {
        public static void WalletServiceTest(string input, params IpcEvent[] evts) => new WalletServiceTest(input).Broadcast(evts);
    }
}

namespace PylonsSdk.Internal.Ipc.Messages
{
    public sealed class WalletServiceTest : IpcMessage
    {
        [JsonProperty("input")]
        public readonly string Input;

        public WalletServiceTest(string input) : base(ResponseType.TEST_RESPONSE)
        {
            Input = input ?? throw new ArgumentNullException(nameof(input));
        }
    }
}