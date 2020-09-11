using Newtonsoft.Json;

namespace PylonsSdk.Tx
{
    public readonly struct EntriesList
    {
        [JsonProperty("CoinOutputs")]
        public readonly CoinOutput[] CoinOutputs;
        [JsonProperty("ItemModifyOutputs")]
        public readonly ItemModifyOutput[] ItemModifyOutputs;
        [JsonProperty("ItemOutputs")]
        public readonly ItemOutput[] ItemOutputs;

        public EntriesList(CoinOutput[] coinOutputs, ItemModifyOutput[] itemModifyOutputs, ItemOutput[] itemOutputs)
        {
            CoinOutputs = coinOutputs;
            ItemModifyOutputs = itemModifyOutputs;
            ItemOutputs = itemOutputs;
        }
    }
}