using Newtonsoft.Json;

namespace PylonsSdk.Tx
{
    public readonly struct WeightedOutput
    {
        [JsonProperty("EntryIDs")]
        public readonly string[] EntryIds;
        [JsonProperty("Weight")]
        public readonly string Weight;

        public WeightedOutput(string[] entryIds, string weight)
        {
            EntryIds = entryIds; 
            Weight = weight;
        }
    }
}