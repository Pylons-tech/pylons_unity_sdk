namespace PylonsSdk.Internal.Ipc.Messages
{
    public sealed class GetCookbooks : IpcMessage
    {
        public GetCookbooks() : base(ResponseType.COOKBOOK_RESPONSE) { }
    }
}