using Newtonsoft.Json;

namespace PylonsSdk.Internal.Ipc.Messages
{
    /// <summary>
    /// IFC message to switch active keypairs (to the one w/ the appropriate address) on
    /// a wallet that supports multi-key.
    /// The keypair must have already been loaded with AddKeypair.
    /// </summary>
    public sealed class SwitchKeys : IpcMessage
    {
        [JsonProperty("address")]
        public readonly string Address;

        public SwitchKeys(string address) : base(ResponseType.PROFILE_RESPONSE)
        {
            Address = address;
        }
    }
}