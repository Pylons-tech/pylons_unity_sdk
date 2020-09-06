using System;

namespace PylonsSdk.Tx
{
    public readonly struct Transaction
    {
        public readonly string TxHash;
        public readonly StdTx StdTx;
        public readonly TxState State;
        public readonly TxError Error;

        public Transaction(string txHash, StdTx stdTx, TxState state, TxError error)
        {
            TxHash = txHash ?? throw new ArgumentNullException(nameof(txHash));
            StdTx = stdTx;
            State = state;
            Error = error;
        }
    }
}