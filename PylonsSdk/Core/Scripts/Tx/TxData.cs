using Newtonsoft.Json;

namespace PylonsSdk.Tx
{
    public readonly struct TxData
    {
        [JsonProperty("msg")]
        public readonly string Msg;
        [JsonProperty("status")]
        public readonly string Status;
        [JsonProperty("output")]
        public readonly TxDataOutput[] Output;
    }
}