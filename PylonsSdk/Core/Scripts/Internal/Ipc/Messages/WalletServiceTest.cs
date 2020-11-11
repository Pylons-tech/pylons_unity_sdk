using Newtonsoft.Json;

namespace PylonsSdk.Internal.Ipc.Messages
{
    /// <summary>
    /// IPC: Basic test of communication with the linked wallet.
    /// Returns a TestResponse w/ a response string containing the input string.
    /// </summary>
    public sealed class WalletServiceTest : IpcMessage
    {
        /// <summary>
        /// The input string.
        /// </summary>
        [JsonProperty("input")]
        public readonly string Input;

        public WalletServiceTest(string input) : base(ResponseType.TEST_RESPONSE) => Input = input;
    }
}