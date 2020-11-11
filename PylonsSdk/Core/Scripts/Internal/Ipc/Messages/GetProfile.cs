using Newtonsoft.Json;

namespace PylonsSdk.Internal.Ipc.Messages
{
    /// <summary>
    /// IPC: Gets a single profile using the linked wallet. Linked wallet must have a keypair.
    /// Depending on whether or not an address is provided (see address argument) this will retrieve either the
    /// account at the given address or the account corresponding to the active keypair.
    /// 
    /// Gets a ProfileResponse containing the current state of the retrieved profile.
    /// </summary>
    public sealed class GetProfile : IpcMessage
    {
        /// <summary>
        /// Address of the profile to retrieve. If left null or empty, just retrieves profile corresponding to current keypair.
        /// </summary>
        [JsonProperty("address")]
        public readonly string Address;

        public GetProfile(string address) : base(ResponseType.PROFILE_RESPONSE)
        {
            Address = address;
        }
    }
}