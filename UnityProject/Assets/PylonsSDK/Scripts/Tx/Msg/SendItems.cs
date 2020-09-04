using Newtonsoft.Json;

namespace PylonsSDK.Tx.Msg
{
    public readonly struct SendItems
    {
        [JsonProperty("ItemIDs")]
        public readonly string[] ItemIds;
        [JsonProperty("Sender")]
        public readonly string Sender;
        [JsonProperty("Receiver")]
        public readonly string Receiver;


        public SendItems(string sender, string receiver, string[] itemIds)
        {
            Sender = sender;
            Receiver = receiver;
            ItemIds = itemIds;
        }
    }
}