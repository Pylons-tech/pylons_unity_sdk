using PylonsSdk.Internal.Ipc;
using PylonsSdk.Internal.Ipc.Messages;

namespace PylonsSdk
{
    public static partial class Service
    {
        public static void GetPendingExecutions(params IpcEvent[] evts) => new GetPendingExecutions().Broadcast(evts);
    }
}

namespace PylonsSdk.Internal.Ipc.Messages
{
    public sealed class GetPendingExecutions : IpcMessage
    {
        public GetPendingExecutions() : base(ResponseType.EXECUTION_RESPONSE) { }
    }
}