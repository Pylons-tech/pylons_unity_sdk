using Newtonsoft.Json;

namespace PylonsSdk.Tx.Msg
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