using Newtonsoft.Json;

namespace PylonsSdk.Tx
{
    public readonly struct TradeItemInput
    {
        [JsonProperty("ItemInput")]
        public readonly ItemInput ItemInput;
        [JsonProperty("CookbookID")]
        public readonly string CookbookId;

        public TradeItemInput(ItemInput itemInput, string cookbookId)
        {
            ItemInput = itemInput;
            CookbookId = cookbookId;
        }
    }
}