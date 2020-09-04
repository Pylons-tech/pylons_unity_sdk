using PylonsSDK.Ipc;
using PylonsSDK.Ipc.Internal.Messages;

namespace PylonsSDK
{
    public static partial class Service
    {
        public static void GetCookbooks(params IpcEvent[] evts) => new GetCookbooks().Broadcast(evts);
    }
}

namespace PylonsSDK.Ipc.Internal.Messages
{
    public sealed class GetCookbooks : IpcMessage
    {
        public GetCookbooks() : base(ResponseType.COOKBOOK_RESPONSE) { }
    }
}