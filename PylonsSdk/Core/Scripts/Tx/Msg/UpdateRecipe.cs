using Newtonsoft.Json;

namespace PylonsSdk.Tx.Msg
{
    public readonly struct UpdateRecipe
    {
        [JsonProperty("Name")]
        public readonly string Name;
        [JsonProperty("CookbookID")]
        public readonly string CookbookId;
        [JsonProperty("ID")]
        public readonly string RecipeId;
        [JsonProperty("CoinInputs")]
        public readonly CoinInput[] CoinInputs;
        [JsonProperty("ItemInputs")]
        public readonly ItemInput[] ItemInputs;
        [JsonProperty("Entries")]
        public readonly EntriesList Entries;
        [JsonProperty("Outputs")]
        public readonly WeightedOutput[] Outputs;
        [JsonProperty("BlockInterval")]
        public readonly long BlockInterval;
        [JsonProperty("Sender")]
        public readonly string Sender;
        [JsonProperty("Description")]
        public readonly string Description;


        public UpdateRecipe(string sender, long blockInterval, string recipeId, string cookbookId, string name, string description, CoinInput[] coinInputs, ItemInput[] itemInputs, EntriesList entries, WeightedOutput[] outputs)
        {
            Sender = sender;
            BlockInterval = blockInterval;
            RecipeId = recipeId;
            CookbookId = cookbookId;
            Name = name;
            Description = description;
            CoinInputs = coinInputs;
            ItemInputs = itemInputs;
            Entries = entries;
            Outputs = outputs;
        }
    }
}