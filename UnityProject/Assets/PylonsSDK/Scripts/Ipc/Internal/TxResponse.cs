using Newtonsoft.Json;
using PylonsSDK.Tx;

namespace PylonsSDK.Ipc.Internal
{
    public readonly struct TxResponse
    {
        [JsonProperty("transactions")]
        public readonly Transaction[] Transactions;
    }
}