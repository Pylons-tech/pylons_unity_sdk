using Newtonsoft.Json;

namespace PylonsSdk.Internal.Ipc.Messages
{
    /// <summary>
    /// IFC: Add an existing keypair to a linked wallet with multi-keypair support.
    /// (We must add at least one keypair before we do anything else, if our target wallet supports multi-key!)
    /// Note that the AddKeypair message automatically switches active keypairs to the newly added one. This
    /// behavior is generally desirable in most real-world use cases, and lets us avoid making excessive IPC
    /// hits; if you don't want to immediately start using the new keys, though, you must immediately
    /// make a SwitchKeys message to switch back.
    /// This API may change in future to include an extra field to control this behavior, instead.
    /// (This should never be called by production code, because no end-user wallet will have multikey.)
    /// 
    /// Gets a ProfileResponse containing the current state of the profile associated w/ the given keys.
    /// </summary>
    public sealed class AddKeypair: IpcMessage
    {
        /// <summary>
        /// Raw uncompressed private key of a (noncompliant...) SECP256K1 keypair, as a hex string.
        /// (Public key and address are just derived from this value.)
        /// TODO: get a page that explains how/why our SECP256K1 implementation is noncompliant and link it.
        /// </summary>
        [JsonProperty("privkey")]
        public readonly string PrivKey;

        public AddKeypair(string privkey) : base(ResponseType.PROFILE_RESPONSE)
        {
            PrivKey = privkey;
        }
    }
}