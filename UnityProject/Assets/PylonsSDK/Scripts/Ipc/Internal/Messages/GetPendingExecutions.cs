using PylonsSDK.Ipc;
using PylonsSDK.Ipc.Internal.Messages;

namespace PylonsSDK
{
    public static partial class Service
    {
        public static void GetPendingExecutions(params IpcEvent[] evts) => new GetPendingExecutions().Broadcast(evts);
    }
}

namespace PylonsSDK.Ipc.Internal.Messages
{
    public sealed class GetPendingExecutions : IpcMessage
    {
        public GetPendingExecutions() : base(ResponseType.EXECUTION_RESPONSE) { }
    }
}