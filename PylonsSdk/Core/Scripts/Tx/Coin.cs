using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PylonsSdk.Tx
{
    public static class CoinExtensions
    {
        public static bool Compare(this Coin[] coins, Coin[] other)
        {
            if (coins == null && other == null) return true;
            if (coins == null || other == null) return false;
            if (coins.Length != other.Length) return false;
            for (int i = 0; i < coins.Length; i++) if (coins[i] != other[i]) return false;
            return true;
        }
    }

    public readonly struct Coin : IEquatable<Coin>
    {
        public static bool operator ==(Coin coin, Coin other) => coin.Denom == other.Denom && coin.Amount == other.Amount;
        public static bool operator !=(Coin coin, Coin other) => coin.Denom != other.Denom || coin.Amount != other.Amount;
        public override bool Equals(object obj) => obj is Coin c && this == c;

        public bool Equals(Coin other)
        {
            return Denom == other.Denom &&
                   Amount == other.Amount;
        }

        public override int GetHashCode()
        {
            var hashCode = -2080394403;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Denom);
            hashCode = hashCode * -1521134295 + Amount.GetHashCode();
            return hashCode;
        }

        [JsonProperty("denom")]
        public readonly string Denom;
        [JsonProperty("amount")]
        public readonly long Amount;

        public Coin(string denom, long amount)
        {
            Denom = denom;
            Amount = amount;
        }
    }
}