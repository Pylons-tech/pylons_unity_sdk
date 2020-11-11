namespace PylonsSdk.Internal.Ipc.Messages
{
    /// <summary>
    /// IPC: Retrieve pending executions belonging to current keypair using the linked wallet.
    /// Linked wallet must have a keypair.
    /// 
    /// Gets an ExecutionResponse containing those executions.
    /// </summary>
    public sealed class GetPendingExecutions : IpcMessage
    {
        public GetPendingExecutions() : base(ResponseType.EXECUTION_RESPONSE) { }
    }
}