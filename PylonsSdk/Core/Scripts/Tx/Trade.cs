using System;
using System.Collections.ObjectModel;

namespace PylonsSdk.Tx
{
    public readonly struct Trade
    {
        public readonly string Id;
        public readonly string Sender;
        public readonly string Fulfiller;
        public readonly string ExtraInfo;
        public readonly bool Disabled;
        public readonly bool Completed;
        public readonly ReadOnlyDictionary<string, long> CoinInputs;
        public readonly ReadOnlyDictionary<string, long> CoinOutputs;
        public readonly ItemInput[] ItemInputs;
        public readonly ItemOutput[] ItemOutputs;

        public Trade(string id, string sender, string fulfiller, string extraInfo, bool disabled, bool completed, ReadOnlyDictionary<string, long> coinInputs, ReadOnlyDictionary<string, long> coinOutputs, ItemInput[] itemInputs, ItemOutput[] itemOutputs)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Sender = sender ?? throw new ArgumentNullException(nameof(sender));
            Fulfiller = fulfiller ?? throw new ArgumentNullException(nameof(fulfiller));
            ExtraInfo = extraInfo ?? throw new ArgumentNullException(nameof(extraInfo));
            Disabled = disabled;
            Completed = completed;
            CoinInputs = coinInputs ?? throw new ArgumentNullException(nameof(coinInputs));
            CoinOutputs = coinOutputs ?? throw new ArgumentNullException(nameof(coinOutputs));
            ItemInputs = itemInputs ?? throw new ArgumentNullException(nameof(itemInputs));
            ItemOutputs = itemOutputs ?? throw new ArgumentNullException(nameof(itemOutputs));
        }
    }
}