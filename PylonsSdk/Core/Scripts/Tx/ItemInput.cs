﻿using Newtonsoft.Json;

namespace PylonsSdk.Tx
{
    public readonly struct ItemInput
    {
        [JsonProperty("Conditions")]
        public readonly ConditionList Conditions;
        [JsonProperty("Doubles")]
        public readonly DoubleInputParam[] Doubles;
        [JsonProperty("Longs")]
        public readonly LongInputParam[] Longs;
        [JsonProperty("Strings")]
        public readonly StringInputParam[] Strings;

        public ItemInput(ConditionList conditions, DoubleInputParam[] doubles, LongInputParam[] longs, StringInputParam[] strings)
        {
            Conditions = conditions;
            Doubles = doubles;
            Longs = longs;
            Strings = strings;
        }
    }
}