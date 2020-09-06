using Newtonsoft.Json;

namespace PylonsSdk.Tx.Msg
{
    public readonly struct GetPylons
    {
        [JsonProperty("Amount")]
        public readonly Coin[] Amount;
        [JsonProperty("Requester")]
        public readonly string Sender;

        public GetPylons(string Sender, Coin[] amount)
        {
            this.Sender = Sender;
            Amount = amount;
        }
    }
}