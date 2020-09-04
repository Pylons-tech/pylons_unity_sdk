using Newtonsoft.Json;
using System.Collections.Generic;

namespace PylonsSDK.Tx
{
    public readonly struct Item
    {
        [JsonProperty("NodeVersion")]
        public readonly string NodeVersion;
        [JsonProperty("ID")]
        public readonly string Id;
        [JsonProperty("Doubles")]
        public readonly KeyValuePair<string, string>[] Doubles;
        [JsonProperty("Longs")]
        public readonly KeyValuePair<string, long>[] Longs;
        [JsonProperty("Strings")]
        public readonly KeyValuePair<string, string>[] Strings;
        [JsonProperty("CookbookId")]
        public readonly string CookbookId;
        [JsonProperty("Sender")]
        public readonly string Sender;
        [JsonProperty("OwnerRecipeId")]
        public readonly string OwnerRecipeId;
        [JsonProperty("OwnerTradeId")]
        public readonly string OwnerTradeId;
        [JsonProperty("Tradable")]
        public readonly bool Tradable;
        [JsonProperty("LastUpdate")]
        public readonly long LastUpdate;
        [JsonProperty("TransferFee")]
        public readonly long TransferFee;

        public Item(string nodeVersion, string id, KeyValuePair<string, string>[] doubles, KeyValuePair<string, long>[] longs, 
            KeyValuePair<string, string>[] strings, string cookbookId, string sender, string ownerRecipeId, string ownerTradeId, 
            bool tradable, long lastUpdate, long transferFee)
        {
            NodeVersion = nodeVersion;
            Id = id;
            Doubles = doubles;
            Longs = longs;
            Strings = strings;
            CookbookId = cookbookId;
            Sender = sender;
            OwnerRecipeId = ownerRecipeId;
            OwnerTradeId = ownerTradeId;
            Tradable = tradable;
            LastUpdate = lastUpdate;
            TransferFee = transferFee;
        }
    }
}