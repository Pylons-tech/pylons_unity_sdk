using Newtonsoft.Json;

namespace PylonsSdk.Internal.Ipc.Messages
{
    /// <summary>
    /// IPC: Batch operation. Enables execution of one or more recipes (sequentially, not simultaneously) using the linked wallet.
    /// The linked wallet must have a keypair, and the active keypair must own the recipes in question.
    /// 
    /// Gets a TxResponse containing all created EnableRecipe transactions.
    /// </summary>
    public sealed class EnableRecipes : IpcMessage
    {
        /// <summary>
        /// Array containing the UUIDs of the recipes to be enabled.
        /// </summary>
        [JsonProperty("recipes")]
        public readonly string[] Recipes;

        public EnableRecipes(string[] recipes) : base(ResponseType.TX_RESPONSE)
        {
            Recipes = recipes;
        }
    }
}