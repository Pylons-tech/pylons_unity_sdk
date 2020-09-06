using Newtonsoft.Json;

namespace PylonsSdk.Tx.Msg
{
    public readonly struct DisableRecipe
    {
        [JsonProperty("RecipeID")]
        public readonly string RecipeId;
        [JsonProperty("Sender")]
        public readonly string Sender;

        public DisableRecipe(string sender, string recipeId)
        {
            Sender = sender;
            RecipeId = recipeId;
        }
    }
}