using PylonsSdk.Internal.Ipc;
using PylonsSdk.Internal.Ipc.Messages;

namespace PylonsSdk
{
    public static partial class Service
    {
        public static void GetRecipes(params IpcEvent[] evts) => new GetRecipes().Broadcast(evts);
    }
}

namespace PylonsSdk.Internal.Ipc.Messages
{
    public sealed class GetRecipes: IpcMessage
    {
        public GetRecipes() : base(ResponseType.RECIPE_RESPONSE) { }
    }
}