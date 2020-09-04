using Newtonsoft.Json;

namespace PylonsSDK.Tx.Msg
{
    public readonly struct ExecuteRecipe
    {
        [JsonProperty("RecipeId")]
        public readonly string RecipeId;
        [JsonProperty("Sender")]
        public readonly string Sender;
        [JsonProperty("ItemIDs")]
        public readonly string[] ItemIds;

        public ExecuteRecipe(string sender, string recipeId, string[] itemIds)
        {
            Sender = sender;
            RecipeId = recipeId;
            ItemIds = itemIds;
        }
    }
}