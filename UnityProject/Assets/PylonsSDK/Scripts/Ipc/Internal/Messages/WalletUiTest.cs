using Newtonsoft.Json;
using PylonsSDK.Ipc;
using PylonsSDK.Ipc.Internal.Messages;
using System;

namespace PylonsSDK
{
    public static partial class Service
    {
        public static void WalletUiTest(string input, params IpcEvent[] evts) => new WalletUiTest(input).Broadcast(evts);
    }
}

namespace PylonsSDK.Ipc.Internal.Messages
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