using Newtonsoft.Json;
using System.Collections.Generic;

namespace PylonsSdk.Tx.Msg
{
    public readonly struct FiatItem
    {
        [JsonProperty("CookbookId")]
        public readonly string CookbookId;
        [JsonProperty("Doubles")]
        public readonly KeyValuePair<string, string>[] Doubles;
        [JsonProperty("Longs")]
        public readonly KeyValuePair<string, long>[] Longs;
        [JsonProperty("Strings")]
        public readonly KeyValuePair<string, string>[] Strings;
        [JsonProperty("Sender")]
        public readonly string Sender;
        [JsonProperty("TransferFee")]
        public readonly long TransferFee;

        public FiatItem(string sender, string cookbookId, KeyValuePair<string, string>[] doubles, KeyValuePair<string, long>[] longs, KeyValuePair<string, string>[] strings, long transferFee)
        {
            Sender = sender;
            CookbookId = cookbookId;
            Doubles = doubles;
            Longs = longs;
            Strings = strings;
            TransferFee = transferFee;
        }
    }
}