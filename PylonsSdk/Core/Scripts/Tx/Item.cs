using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PylonsSdk.Tx
{
    public static class ItemExtensions
    {
        public static bool Compare(this Item[] items, Item[] other)
        {
            if (items == null && other == null) return true;
            if (items == null || other == null) return false;
            if (items.Length != other.Length) return false;
            for (int i = 0; i < items.Length; i++)
                if (items[i] != other[i]) return false;
            return true;
        }

        public static TValue? GetV<TKey, TValue> (this KeyValuePair<TKey, TValue>[] kvp, TKey key) where TValue : struct
        {
            foreach (var pair in kvp)
            {
                if (EqualityComparer<TKey>.Default.Equals(key, pair.Key)) return (TValue?)pair.Value;
            }
            return null;
        }

        public static TValue GetO<TKey, TValue>(this KeyValuePair<TKey, TValue>[] kvp, TKey key) where TValue : class
        {
            foreach (var pair in kvp)
            {
                if (EqualityComparer<TKey>.Default.Equals(key, pair.Key)) return pair.Value;
            }
            return null;
        }

        public static bool Compare<TKey, TValue> (this KeyValuePair<TKey, TValue>[] kvp, KeyValuePair<TKey, TValue>[] other)
        {
            if (kvp == null && other == null) return true;
            if (kvp == null || other == null) return false;
            if (kvp.Length != other.Length) return false;
            for (int i = 0; i < kvp.Length; i++)
                if (!EqualityComparer<TKey>.Default.Equals(kvp[i].Key, other[i].Key) || 
                    !EqualityComparer<TValue>.Default.Equals(kvp[i].Value, other[i].Value)) return false;
            return true;
        }
    }

    public readonly struct Item : IEquatable<Item>
    {
        public static bool operator ==(Item item, Item other) => item.NodeVersion == other.NodeVersion && item.Id == other.Id && item.CookbookId == other.CookbookId && item.Sender == other.Sender
            && item.OwnerRecipeId == other.OwnerRecipeId && item.OwnerTradeId == other.OwnerTradeId && item.Tradable == other.Tradable && item.LastUpdate == other.LastUpdate 
            && item.TransferFee == other.TransferFee && item.Doubles.Compare(other.Doubles) && item.Longs.Compare(other.Longs) && item.Strings.Compare(other.Strings);

        public static bool operator !=(Item item, Item other) => item.NodeVersion != other.NodeVersion || item.Id != other.Id || item.CookbookId != other.CookbookId || item.Sender != other.Sender 
            || item.OwnerRecipeId != other.OwnerRecipeId || item.OwnerTradeId != other.OwnerTradeId || item.Tradable != other.Tradable || item.LastUpdate != other.LastUpdate 
            || item.TransferFee != other.TransferFee || !item.Doubles.Compare(other.Doubles) || !item.Longs.Compare(other.Longs) || !item.Strings.Compare(other.Strings);

        [JsonProperty("NodeVersion")]
        public readonly string NodeVersion;
        [JsonProperty("ID")]
        public readonly string Id;
        [JsonProperty("Doubles")]
        public readonly KeyValuePair<string, string>[] Doubles;
        [JsonProperty("Longs")]
        public readonly KeyValuePair<string, long>[] Longs;
        [JsonProperty("Strings")]
        public readonly KeyValuePair<string, string>[] Strings;
        [JsonProperty("CookbookId")]
        public readonly string CookbookId;
        [JsonProperty("Sender")]
        public readonly string Sender;
        [JsonProperty("OwnerRecipeId")]
        public readonly string OwnerRecipeId;
        [JsonProperty("OwnerTradeId")]
        public readonly string OwnerTradeId;
        [JsonProperty("Tradable")]
        public readonly bool Tradable;
        [JsonProperty("LastUpdate")]
        public readonly long LastUpdate;
        [JsonProperty("TransferFee")]
        public readonly long TransferFee;

        public Item(string nodeVersion, string id, KeyValuePair<string, string>[] doubles, KeyValuePair<string, long>[] longs, 
            KeyValuePair<string, string>[] strings, string cookbookId, string sender, string ownerRecipeId, string ownerTradeId, 
            bool tradable, long lastUpdate, long transferFee)
        {
            NodeVersion = nodeVersion;
            Id = id;
            Doubles = doubles;
            Longs = longs;
            Strings = strings;
            CookbookId = cookbookId;
            Sender = sender;
            OwnerRecipeId = ownerRecipeId;
            OwnerTradeId = ownerTradeId;
            Tradable = tradable;
            LastUpdate = lastUpdate;
            TransferFee = transferFee;
        }

        public override bool Equals(object obj) => obj is Item && Equals((Item)obj);

        public bool Equals(Item other) => this == other;

        public override int GetHashCode()
        {
            var hashCode = -1781101708;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NodeVersion);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<KeyValuePair<string, string>[]>.Default.GetHashCode(Doubles);
            hashCode = hashCode * -1521134295 + EqualityComparer<KeyValuePair<string, long>[]>.Default.GetHashCode(Longs);
            hashCode = hashCode * -1521134295 + EqualityComparer<KeyValuePair<string, string>[]>.Default.GetHashCode(Strings);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CookbookId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Sender);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OwnerRecipeId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OwnerTradeId);
            hashCode = hashCode * -1521134295 + Tradable.GetHashCode();
            hashCode = hashCode * -1521134295 + LastUpdate.GetHashCode();
            hashCode = hashCode * -1521134295 + TransferFee.GetHashCode();
            return hashCode;
        }
    }
}