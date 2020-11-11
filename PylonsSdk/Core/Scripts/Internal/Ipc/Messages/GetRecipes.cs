namespace PylonsSdk.Internal.Ipc.Messages
{
    /// <summary>
    /// IPC: Fetch recipes owned by current keypair cookbooks using the linked wallet.
    /// Linked wallet must have a keypair.
    /// 
    /// Gets a RecipeResponse containing those recipes.
    /// </summary>
    public sealed class GetRecipes: IpcMessage
    {
        public GetRecipes() : base(ResponseType.RECIPE_RESPONSE) { }
    }
}