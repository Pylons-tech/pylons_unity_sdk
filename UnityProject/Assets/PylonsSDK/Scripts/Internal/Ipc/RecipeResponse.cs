using Newtonsoft.Json;
using PylonsSdk.Tx;

namespace PylonsSdk.Internal.Ipc
{
    public readonly struct RecipeResponse
    {
        [JsonProperty("recipes")]
        public readonly Recipe[] Recipes;

        public RecipeResponse(Recipe[] recipes)
        {
            Recipes = recipes;
        }
    }
}