namespace PylonsSdk.Internal.Ipc.Messages
{
    public sealed class ExportKeys : IpcMessage
    {
        public ExportKeys() : base(ResponseType.KEY_RESPONSE)
        {
            // empty class body
        }
    }
}