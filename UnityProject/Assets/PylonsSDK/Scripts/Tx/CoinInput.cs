using Newtonsoft.Json;

public readonly struct CoinInput
{
    [JsonProperty("Coin")]
    public readonly string Coin;
    [JsonProperty("Count")]
    public readonly long Count;

    public CoinInput(string coin, long count)
    {
        Coin = coin;
        Count = count;
    }
}