using Newtonsoft.Json;

namespace PylonsSDK.Tx
{
    public readonly struct ItemInput
    {
        [JsonProperty("Doubles")]
        public readonly DoubleInputParam[] Doubles;
        [JsonProperty("Longs")]
        public readonly LongInputParam[] Longs;
        [JsonProperty("Strings")]
        public readonly StringInputParam[] Strings;

        public ItemInput(DoubleInputParam[] doubles, LongInputParam[] longs, StringInputParam[] strings)
        {
            Doubles = doubles;
            Longs = longs;
            Strings = strings;
        }
    }
}