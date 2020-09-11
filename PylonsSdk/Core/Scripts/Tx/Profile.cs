using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PylonsSdk.Tx
{
    public static class ProfileExtensions
    {
        public static Profile GetFirst(this Profile[] profiles)
        {
            if (profiles != null && profiles.Length > 0) return profiles[0];
            else return default;
        }

        public static bool ArrayEquals(this Coin[] coins, Coin[] other)
        {
            if (coins.Length != other.Length) return false;
            for (int i = 0; i < coins.Length; i++) if (coins[i].Amount != other[i].Amount || coins[i].Denom != other[i].Denom) return false;
            return true;
        }
    }

    public readonly struct Profile : IEquatable<Profile>
    {
        [JsonProperty("address")]
        public readonly string Address;
        [JsonProperty("name")]
        public readonly string Name;
        [JsonProperty("coins")]
        public readonly Coin[] Coins;
        [JsonProperty("items")]
        public readonly Item[] Items;

        public Profile(string address, string name, Coin[] coins, Item[] items)
        {
            Address = address;
            Name = name;
            Coins = coins;
            Items = items;
        }  

        public static bool operator ==(Profile prf, Profile other) => prf.Address == other.Address && prf.Name == other.Name && prf.Coins.Compare(other.Coins) && prf.Items.Compare(other.Items);

        public static bool operator !=(Profile prf, Profile other) => prf.Address != other.Address || prf.Name != other.Name || !prf.Coins.Compare(other.Coins) || !prf.Items.Compare(other.Items);

        public bool Default => this == default;

        public override bool Equals(object obj) => obj is Profile && Equals((Profile)obj);

        public bool Equals(Profile other) => other == this;

        public override int GetHashCode()
        {
            var hashCode = 396661560;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Address);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<Coin[]>.Default.GetHashCode(Coins);
            hashCode = hashCode * -1521134295 + EqualityComparer<Item[]>.Default.GetHashCode(Items);
            hashCode = hashCode * -1521134295 + Default.GetHashCode();
            return hashCode;
        }
    }
}