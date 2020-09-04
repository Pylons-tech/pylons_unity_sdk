using Newtonsoft.Json;
using PylonsSDK.Tx;

namespace PylonsSDK.Ipc.Internal
{
    public readonly struct ProfileResponse
    {
        [JsonProperty("profiles")]
        public readonly Profile[] Profiles;
    }
}