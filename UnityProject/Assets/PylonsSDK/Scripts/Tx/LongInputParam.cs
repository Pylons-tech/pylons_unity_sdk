using Newtonsoft.Json;

namespace PylonsSdk.Tx
{
    public readonly struct LongInputParam
    {
        [JsonProperty("Key")]
        public readonly string Key;
        [JsonProperty("MinValue")]
        public readonly long MinValue;
        [JsonProperty("MaxValue")]
        public readonly long MaxValue;

        public LongInputParam(string key, long minValue, long maxValue)
        {
            Key = key;
            MinValue = minValue;
            MaxValue = maxValue;
        }
    }
}