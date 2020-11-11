namespace PylonsSdk.Internal.Ipc.Messages
{
    /// <summary>
    /// IPC: Fetch cookbooks owned by current keypair using the linked wallet.
    /// Linked wallet must have a keypair.
    /// 
    /// Gets a CookbookResponse containing those cookbooks.
    /// </summary>
    public sealed class GetCookbooks : IpcMessage
    {
        public GetCookbooks() : base(ResponseType.COOKBOOK_RESPONSE) { }
    }
}