namespace PylonsSdk.Internal.Ipc.Messages
{
    public sealed class GetPendingExecutions : IpcMessage
    {
        public GetPendingExecutions() : base(ResponseType.EXECUTION_RESPONSE) { }
    }
}