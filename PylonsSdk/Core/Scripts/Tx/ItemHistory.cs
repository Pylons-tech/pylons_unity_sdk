using Newtonsoft.Json;

namespace PylonsSdk.Tx
{
    public readonly struct ItemHistory
    {
        [JsonProperty("ID")]
        public readonly string Id;
        [JsonProperty("Owner")]
        public readonly string Owner;
        [JsonProperty("ItemID")]
        public readonly string ItemId;
        [JsonProperty("RecipeID")]
        public readonly string RecipeId;
        [JsonProperty("TradeID")]
        public readonly string TradeId;

        public ItemHistory(string id, string owner, string itemId, string recipeId, string tradeId)
        {
            Id = id;
            Owner = owner;
            ItemId = itemId;
            RecipeId = recipeId;
            TradeId = tradeId;
        }
    }
}