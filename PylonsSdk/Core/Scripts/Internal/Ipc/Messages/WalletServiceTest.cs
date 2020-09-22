using Newtonsoft.Json;
using System;

namespace PylonsSdk.Internal.Ipc.Messages
{
    public sealed class WalletServiceTest : IpcMessage
    {
        [JsonProperty("input")]
        public readonly string Input;

        public WalletServiceTest(string input) : base(ResponseType.TEST_RESPONSE)
        {
            UnityEngine.Debug.Log("constructor hit");
            Input = input ?? throw new ArgumentNullException(nameof(input));
        }
    }
}