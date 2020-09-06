using System;

namespace PylonsSdk.Tx
{
    public readonly struct StdTx
    {
        public readonly string Memo;
        public readonly StdFee Fee;
        public readonly StdSignature[] Signatures;
        public readonly object[] Msgs;

        public StdTx(string memo, StdFee fee, StdSignature[] signatures, object[] msgs)
        {
            Memo = memo ?? throw new ArgumentNullException(nameof(memo));
            Fee = fee;
            Signatures = signatures ?? throw new ArgumentNullException(nameof(signatures));
            Msgs = msgs ?? throw new ArgumentNullException(nameof(msgs));
        }
    }
}