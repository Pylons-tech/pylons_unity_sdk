using Newtonsoft.Json;

namespace PylonsSDK.Ipc.Internal
{
    public readonly struct TestResponse
    {
        [JsonProperty("output")]
        public readonly string Output;
    }
}