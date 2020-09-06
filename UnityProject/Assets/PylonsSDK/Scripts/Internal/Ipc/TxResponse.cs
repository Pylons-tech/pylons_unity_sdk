using Newtonsoft.Json;
using PylonsSdk.Tx;

namespace PylonsSdk.Internal.Ipc
{
    public readonly struct TxResponse
    {
        [JsonProperty("transactions")]
        public readonly Transaction[] Transactions;

        public TxResponse(Transaction[] transactions)
        {
            Transactions = transactions;
        }
    }
}