using Newtonsoft.Json;
using System;

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