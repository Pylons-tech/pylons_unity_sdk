using Newtonsoft.Json;

namespace PylonsSDK.Tx.Msg
{
    public readonly struct SendCoins
    {
        [JsonProperty("Amount")]
        public readonly Coin[] Amount;
        [JsonProperty("Sender")]
        public readonly string Sender;
        [JsonProperty("Receiver")]
        public readonly string Receiver;

        public SendCoins(string sender, string receiver, Coin[] amount)
        {
            Sender = sender;
            Receiver = receiver;
            Amount = amount;
        }
    }
}