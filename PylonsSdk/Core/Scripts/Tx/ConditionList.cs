using Newtonsoft.Json;

namespace PylonsSdk.Tx
{
    public readonly struct ConditionList
    {
        [JsonProperty("Doubles")]
        public readonly DoubleInputParam[] Doubles;
        [JsonProperty("Longs")]
        public readonly LongInputParam[] Longs;
        [JsonProperty("Strings")]
        public readonly StringInputParam[] Strings;

        public ConditionList(DoubleInputParam[] doubles, LongInputParam[] longs, StringInputParam[] strings)
        {
            Doubles = doubles;
            Longs = longs;
            Strings = strings;
        }
    }
}