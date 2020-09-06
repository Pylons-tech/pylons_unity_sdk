using Newtonsoft.Json;

namespace PylonsSdk.Tx
{
    public readonly struct StringInputParam
    {
        [JsonProperty("Key")]
        public readonly string Key;
        [JsonProperty("Value")]
        public readonly string Value;

        public StringInputParam(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}