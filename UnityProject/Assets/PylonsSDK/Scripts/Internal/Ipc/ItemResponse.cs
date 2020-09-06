using Newtonsoft.Json;
using PylonsSdk.Tx;

namespace PylonsSdk.Internal.Ipc
{
    public readonly struct ItemResponse
    {
        [JsonProperty("items")]
        public readonly Item[] Items;

        public ItemResponse(Item[] items)
        {
            Items = items;
        }
    }
}
