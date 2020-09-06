using PylonsSdk.Internal.Ipc;
using PylonsSdk.Internal.Ipc.Messages;

namespace PylonsSdk
{
    public static partial class Service
    {
        public static void GetCookbooks(params IpcEvent[] evts) => new GetCookbooks().Broadcast(evts);
    }
}

namespace PylonsSdk.Internal.Ipc.Messages
{
    public sealed class GetCookbooks : IpcMessage
    {
        public GetCookbooks() : base(ResponseType.COOKBOOK_RESPONSE) { }
    }
}