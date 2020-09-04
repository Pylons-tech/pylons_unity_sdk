using Newtonsoft.Json;
using PylonsSDK.Ipc;
using PylonsSDK.Ipc.Internal.Messages;

namespace PylonsSDK
{
    public static partial class Service
    {
        public static void GetPylons(long count, params IpcEvent[] evts) => new GetPylons(count).Broadcast(evts);
    }
}

namespace PylonsSDK.Ipc.Internal.Messages
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