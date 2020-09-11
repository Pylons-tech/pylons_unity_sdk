using Newtonsoft.Json;

namespace PylonsSdk.Tx
{
    public readonly struct DoubleInputParam
    {
        [JsonProperty("Key")]
        public readonly string Key;
        [JsonProperty("MinValue")]
        public readonly double MinValue;
        [JsonProperty("MaxValue")]
        public readonly double MaxValue;

        public DoubleInputParam(string key, double minValue, double maxValue)
        {
            Key = key;
            MinValue = minValue;
            MaxValue = maxValue;
        }
    }
}