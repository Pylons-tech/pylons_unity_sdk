using Newtonsoft.Json;

namespace PylonsSdk.Internal.Ipc
{
    public readonly struct TestResponse
    {
        [JsonProperty("output")]
        public readonly string Output;

        public TestResponse(string output)
        {
            Output = output;
        }
    }
}