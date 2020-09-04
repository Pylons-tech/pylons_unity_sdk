using PylonsSDK.Ipc;
using PylonsSDK.Ipc.Internal.Messages;

namespace PylonsSDK
{
    public static partial class Service
    {
        public static void GetRecipes(params IpcEvent[] evts) => new GetRecipes().Broadcast(evts);
    }
}

namespace PylonsSDK.Ipc.Internal.Messages
{
    public sealed class GetRecipes: IpcMessage
    {
        public GetRecipes() : base(ResponseType.RECIPE_RESPONSE) { }
    }
}