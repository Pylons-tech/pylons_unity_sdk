using System;
using System.Collections.ObjectModel;

namespace PylonsSdk.Tx
{
    public readonly struct Recipe
    {
        public readonly string Id;
        public readonly string Sender;
        public readonly string CookbookId;
        public readonly string Name;
        public readonly string Description;
        public readonly long BlockInterval;
        public readonly bool Disabled;
        public readonly ReadOnlyDictionary<string, long> CoinInputs;
        public readonly ItemInput[] ItemInputs;
        public readonly EntriesList Entries;
        public readonly WeightedOutput[] Outputs;

        public Recipe(string id, string sender, string cookbookId, string name, string description, long blockInterval, bool disabled, ReadOnlyDictionary<string, long> coinInputs, ItemInput[] itemInputs, EntriesList entries, WeightedOutput[] outputs)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Sender = sender ?? throw new ArgumentNullException(nameof(sender));
            CookbookId = cookbookId ?? throw new ArgumentNullException(nameof(cookbookId));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            BlockInterval = blockInterval;
            Disabled = disabled;
            CoinInputs = coinInputs ?? throw new ArgumentNullException(nameof(coinInputs));
            ItemInputs = itemInputs ?? throw new ArgumentNullException(nameof(itemInputs));
            Entries = entries;
            Outputs = outputs ?? throw new ArgumentNullException(nameof(outputs));
        }
    }
}