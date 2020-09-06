using System;
using System.Collections.ObjectModel;

namespace PylonsSdk.Tx
{
    public readonly struct Profile
    {
        public readonly string Id;
        public readonly string Name;
        public readonly ReadOnlyDictionary<string, long> Coins;
        public readonly Item[] Items;

        public Profile(string id, string name, ReadOnlyDictionary<string, long> coins, Item[] items)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Coins = coins ?? throw new ArgumentNullException(nameof(coins));
            Items = items ?? throw new ArgumentNullException(nameof(items));
        }
    }
}