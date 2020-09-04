using Newtonsoft.Json;
using PylonsSDK.Tx;

namespace PylonsSDK.Ipc.Internal
{
    public readonly struct ItemResponse
    {
        [JsonProperty("items")]
        public readonly Item[] Items;
    }
}
