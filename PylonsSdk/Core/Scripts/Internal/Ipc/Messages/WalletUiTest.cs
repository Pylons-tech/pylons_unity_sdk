using Newtonsoft.Json;
using PylonsSdk.Internal.Ipc;
using PylonsSdk.Internal.Ipc.Messages;
using System;

namespace PylonsSdk
{
    public static partial class Service
    {
        public static void WalletUiTest(string input, params IpcEvent[] evts) => new WalletUiTest(input).Broadcast(evts);
    }
}

namespace PylonsSdk.Internal.Ipc.Messages
{
    public sealed class WalletUiTest : IpcMessage
    {
        [JsonProperty("input")]
        public readonly string Input;

        public WalletUiTest(string input) : base(ResponseType.TEST_RESPONSE)
        {
            Input = input ?? throw new ArgumentNullException(nameof(input));
        }
    }
}