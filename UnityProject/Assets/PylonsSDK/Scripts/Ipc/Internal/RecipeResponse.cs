using Newtonsoft.Json;
using PylonsSDK.Tx;

namespace PylonsSDK.Ipc.Internal
{
    public readonly struct RecipeResponse
    {
        [JsonProperty("recipes")]
        public readonly Recipe[] Recipes;
    }
}