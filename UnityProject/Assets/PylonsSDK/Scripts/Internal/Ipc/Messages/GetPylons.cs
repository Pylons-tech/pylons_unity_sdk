using Newtonsoft.Json;
using PylonsSdk.Internal.Ipc;
using PylonsSdk.Internal.Ipc.Messages;

namespace PylonsSdk
{
    public static partial class Service
    {
        public static void GetPylons(long count, params IpcEvent[] evts) => new GetPylons(count).Broadcast(evts);
    }
}

namespace PylonsSdk.Internal.Ipc.Messages
{
    public sealed class GetPylons : IpcMessage
    {
        [JsonProperty("count")]
        public readonly long Count;

        public GetPylons(long count) : base(ResponseType.TX_RESPONSE)
        {
            Count = count;
        }
    }
}