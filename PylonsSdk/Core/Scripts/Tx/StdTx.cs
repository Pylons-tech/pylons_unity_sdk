using Newtonsoft.Json;

namespace PylonsSdk.Tx
{
    public readonly struct StdTx
    {
        [JsonProperty("memo")]
        public readonly string Memo;
        [JsonProperty("fee")]
        public readonly StdFee Fee;
        [JsonProperty("signatures")]
        public readonly StdSignature[] Signatures;
        [JsonProperty("msgs")]
        //TODO: need to write smth to intelligently parse msgs (this isn't urgent tho b/c we have other criteria for assessing success/failure)
        public readonly object[] Msgs;

        public StdTx(string memo, StdFee fee, StdSignature[] signatures, object[] msgs)
        {
            Memo = memo;
            Fee = fee;
            Signatures = signatures;
            Msgs = msgs;
        }
    }
}