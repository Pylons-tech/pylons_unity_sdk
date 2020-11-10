using Newtonsoft.Json;

namespace PylonsSdk.Internal.Ipc.Messages
{
    /// <summary>
    /// IPC: Batch operation. Disables execution of one or more recipes (sequentially, not simultaneously) using the linked wallet.
    /// The linked wallet must have a keypair, and the active keypair must own the recipes in question.
    /// 
    /// Gets a TxResponse containing all created DisableRecipe transactions.
    /// </summary>
    public sealed class DisableRecipes : IpcMessage
    {
        /// <summary>
        /// Array containing the UUIDs of the recipes to be disabled.
        /// </summary>
        [JsonProperty("recipes")]
        public readonly string[] Recipes;

        public DisableRecipes(string[] recipes) : base(ResponseType.TX_RESPONSE)
        {
            Recipes = recipes;
        }
    }
}