using Newtonsoft.Json;

namespace PylonsSdk.Internal.Ipc.Messages
{
    /// <summary>
    /// IPC: Basic test of communication with the linked wallet and the UI control passing mechanism.
    /// Returns a TestResponse w/ a response string containing the input string.
    /// </summary>
    public sealed class WalletUiTest : IpcMessage
    {
        /// <summary>
        /// The input string.
        /// </summary>
        [JsonProperty("input")]
        public readonly string Input;

        public WalletUiTest(string input) : base(ResponseType.TEST_RESPONSE)
        {
            Input = input;
        }
    }
}