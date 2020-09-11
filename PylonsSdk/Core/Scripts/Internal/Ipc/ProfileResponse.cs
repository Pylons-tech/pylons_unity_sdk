using Newtonsoft.Json;
using PylonsSdk.Tx;

namespace PylonsSdk.Internal.Ipc
{
    public readonly struct ProfileResponse
    {
        [JsonProperty("profiles")]
        public readonly Profile[] Profiles;

        public ProfileResponse(Profile[] profiles)
        {
            Profiles = profiles;
        }
    }
}