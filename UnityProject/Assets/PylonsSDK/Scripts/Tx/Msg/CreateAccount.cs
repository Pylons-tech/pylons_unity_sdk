using Newtonsoft.Json;

namespace PylonsSDK.Tx.Msg
{
    public readonly struct CreateAccount
    {
        [JsonProperty("Requester")]
        public readonly string Sender;

        public CreateAccount(string sender)
        {
            Sender = sender;
        }
    }
}