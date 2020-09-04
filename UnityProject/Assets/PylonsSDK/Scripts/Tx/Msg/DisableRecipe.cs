using Newtonsoft.Json;

namespace PylonsSDK.Tx.Msg
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