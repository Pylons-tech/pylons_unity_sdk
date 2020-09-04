using Newtonsoft.Json;

public readonly struct Coin
{
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