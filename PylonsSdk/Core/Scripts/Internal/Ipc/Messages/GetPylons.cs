using Newtonsoft.Json;

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