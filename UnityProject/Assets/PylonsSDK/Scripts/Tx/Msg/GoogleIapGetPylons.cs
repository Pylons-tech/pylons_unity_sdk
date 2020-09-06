using Newtonsoft.Json;

namespace PylonsSdk.Tx.Msg
{
    public readonly struct GoogleIapGetPylons
    {
        [JsonProperty("ProductID")]
        public readonly string ProductId;
        [JsonProperty("PurchaseToken")]
        public readonly string PurchaseToken;
        [JsonProperty("ReceiptDataBase64")]
        public readonly string ReceiptDataBase64;
        [JsonProperty("Signature")]
        public readonly string Signature;
        [JsonProperty("Requester")]
        public readonly string Sender;

        public GoogleIapGetPylons(string sender, string signature, string productId, string purchaseToken, string receiptDataBase64)
        {
            Sender = sender;
            Signature = signature;
            ProductId = productId;
            PurchaseToken = purchaseToken;
            ReceiptDataBase64 = receiptDataBase64;
        }
    }
}