using Newtonsoft.Json;

namespace PylonsSdk.Internal.Ipc.Messages
{
    /// <summary>
    /// IPC: Registers a new account using the linked wallet.
    /// 
    /// Returns a TxResponse containing the created RegisterAccount message.
    /// </summary>
    public sealed class RegisterProfile : IpcMessage
    {
        /// <summary>
        /// The name of the profile to register.
        /// NOTE: name still doesn't actually exist on backend...
        /// </summary>
        [JsonProperty("name")]
        public readonly string Name;
        /// <summary>
        /// If true, generate new keys; else, try to register new account w/ current keypair.
        /// If true and multi-key is enabled for linked wallet, a new WalletCore instance will
        /// be brought online and the active keypair will automatically switch to the new one.
        /// </summary>
        [JsonProperty("makeKeys")]
        public readonly bool MakeKeys;

        public RegisterProfile(string name, bool makeKeys) : base(ResponseType.TX_RESPONSE)
        {
            Name = name;
            MakeKeys = makeKeys;
        }
    }
}