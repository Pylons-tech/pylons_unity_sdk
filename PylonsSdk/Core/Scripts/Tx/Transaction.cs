using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PylonsSdk.Tx
{
    public static class TransactionExtensions
    {
        public static bool Success(this Transaction[] txs)
        {
            foreach (var tx in txs) if (tx.Code != ResponseCode.OK) return false;
            return true;
        }
    }

    public readonly struct Transaction
    {
        [JsonProperty("txData", NullValueHandling = NullValueHandling.Ignore)]
        public readonly TxData[] TxData;
        [JsonProperty("stdTx", NullValueHandling = NullValueHandling.Ignore)]
        public readonly StdTx StdTx;
        [JsonProperty("code")]
        public readonly ResponseCode Code;
        [JsonProperty("raw_log")]
        public readonly string RawLog;

        public Transaction(TxData[] txData, StdTx stdTx, ResponseCode code, string rawLog)
        {
            TxData = txData;
            StdTx = stdTx;
            Code = code;
            RawLog = rawLog;
        }
    }
}