using System;
using System.Collections.ObjectModel;

namespace PylonsSdk.Tx
{
    public readonly struct StdFee
    {
        public readonly ReadOnlyDictionary<string, long> Amount;
        public readonly long Gas;

        public StdFee(ReadOnlyDictionary<string, long> amount, long gas)
        {
            Amount = amount ?? throw new ArgumentNullException(nameof(amount));
            Gas = gas;
        }
    }
}