using Newtonsoft.Json;
using PylonsSdk.Tx;

namespace PylonsSdk.Internal.Ipc
{
    public readonly struct TxResponse 
    {
        [JsonProperty("transactions", NullValueHandling = NullValueHandling.Include)]
        public readonly Transaction[] Transactions;

        public TxResponse(Transaction[] transactions)
        {
            Transactions = transactions;
        }

        public bool Succeeded()
        {
            foreach (var tx in Transactions) { if (tx.Code != ResponseCode.OK) return false; }
            return true;        
        }
    }
}