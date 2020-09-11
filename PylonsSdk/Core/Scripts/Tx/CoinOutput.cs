using Newtonsoft.Json;

public readonly struct CoinOutput
{
    [JsonProperty("ID")]
    public readonly string Id;
    [JsonProperty("Coin")]
    public readonly string Coin;
    [JsonProperty("Count")]
    public readonly string Count;

    public CoinOutput(string id, string coin, string count)
    {
        Id = id;
        Coin = coin;
        Count = count;
    }
}