using Newtonsoft.Json;

namespace PylonsSdk.Tx
{
    public readonly struct Execution
    {
        [JsonProperty("nodeVersion")]
        public readonly string NodeVersion;
        [JsonProperty("id")]
        public readonly string Id;
        [JsonProperty("recipeId")]
        public readonly string RecipeId;
        [JsonProperty("cookbookId")]
        public readonly string CookbookId;
        [JsonProperty("coinInputs")]
        public readonly Coin[] CoinInputs;
        [JsonProperty("itemInputs")]
        public readonly string[] ItemInputs;
        [JsonProperty("blockHeight")]
        public readonly long BlockHeight;
        [JsonProperty("sender")]
        public readonly string Sender;
        [JsonProperty("completed")]
        public readonly bool Completed;

        public Execution(string nodeVersion, string id, string recipeId, string cookbookId, Coin[] coinInputs, string[] itemInputs, long blockHeight, string sender, bool completed)
        {
            NodeVersion = nodeVersion;
            Id = id;
            RecipeId = recipeId;
            CookbookId = cookbookId;
            CoinInputs = coinInputs;
            ItemInputs = itemInputs;
            BlockHeight = blockHeight;
            Sender = sender;
            Completed = completed;
        }
    }
}