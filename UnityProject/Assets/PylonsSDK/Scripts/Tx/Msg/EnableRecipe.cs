using Newtonsoft.Json;

namespace PylonsSDK.Tx.Msg
{
    public readonly struct EnableRecipe
    {
        [JsonProperty("RecipeID")]
        public readonly string RecipeId;
        [JsonProperty("Sender")]
        public readonly string Sender;

        public EnableRecipe(string sender, string recipeId)
        {
            Sender = sender;
            RecipeId = recipeId;
        }
    }
}