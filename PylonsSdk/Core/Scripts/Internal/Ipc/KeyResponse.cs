using Newtonsoft.Json;

namespace PylonsSdk.Internal.Ipc
{
    public readonly struct KeyResponse
    {
        [JsonProperty("privateKey")]
        public readonly string PrivateKey;
        [JsonProperty("publicKey")]
        public readonly string PublicKey;
        [JsonProperty("address")]
        public readonly string Address;
        [JsonProperty("name")]
        public readonly string Name;

        public KeyResponse(string privateKey, string publicKey, string address, string name)
        {
            PrivateKey = privateKey;
            PublicKey = publicKey;
            Address = address;
            Name = name;
        }
    }
}