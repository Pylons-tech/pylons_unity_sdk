using Newtonsoft.Json;

namespace PylonsSdk.Internal.Ipc.Messages
{
    /// <summary>
    /// IPC: Use the linked wallet to execute the given recipe (from the given cookbook.)
    /// The linked wallet must have a keypair.
    /// 
    /// Gets a TxResponse containing the created ExecuteRecipe transaction.
    /// </summary>
    public sealed class ExecuteRecipe : IpcMessage
    {
        /// <summary>
        /// The unique-within-cookbook ID of the recipe to be executed.
        /// </summary>
        [JsonProperty("recipe")]
        public readonly string Recipe;
        /// <summary>
        /// The unique ID of the cookbook the recipe is part of.
        /// </summary>
        [JsonProperty("cookbook")]
        public readonly string Cookbook;
        /// <summary>
        /// An array of item UUIDs. These items must satisfy all of the recipe's item input requirements.
        /// These do not needto be in any specific order; the node will handle matching them to the recipe's 
        /// inputs on its own.
        /// </summary>
        [JsonProperty("itemInputs")]
        public readonly string[] ItemInputs;

        public ExecuteRecipe(string recipe, string cookbook, string[] itemInputs) : base(ResponseType.TX_RESPONSE)
        {
            Recipe = recipe;
            Cookbook = cookbook;
            ItemInputs = itemInputs;
        }
    }
}