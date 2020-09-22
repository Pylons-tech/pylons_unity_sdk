namespace PylonsSdk.Internal.Ipc.Messages
{
    public sealed class GetRecipes: IpcMessage
    {
        public GetRecipes() : base(ResponseType.RECIPE_RESPONSE) { }
    }
}