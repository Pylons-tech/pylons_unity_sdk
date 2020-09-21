using Newtonsoft.Json;

namespace PylonsSdk
{
    public readonly struct StatusBlock
    {
        public static StatusBlock Last { get; private set; }

        [JsonProperty("height")]
        public readonly long BlockHeight;
        [JsonProperty("walletCoreVersion")]
        public readonly string WalletCoreVersion;
        [JsonProperty("blockTime")]
        public readonly double BlockTime;

        public StatusBlock(long blockHeight, string walletCoreVersion, double blockTime)
        {
            BlockHeight = blockHeight;
            WalletCoreVersion = walletCoreVersion;
            BlockTime = blockTime;
            Last = this;
        }
    }
}