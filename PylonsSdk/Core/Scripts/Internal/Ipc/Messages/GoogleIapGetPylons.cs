using Newtonsoft.Json;

namespace PylonsSdk.Internal.Ipc.Messages
{
    public sealed class GoogleIapGetPylons : IpcMessage
    {
        [JsonProperty("productId")]
        public readonly string ProductId;
        [JsonProperty("purchaseToken")]
        public readonly string PurchaseToken;
        [JsonProperty("receiptData")]
        public readonly string ReceiptData;
        [JsonProperty("signature")]
        public readonly string Signature;

        public GoogleIapGetPylons(string productId, string purchaseToken, string receiptData, string signature) : base(ResponseType.TX_RESPONSE)
        {
            ProductId = productId;
            PurchaseToken = purchaseToken;
            ReceiptData = receiptData;
            Signature = signature;
        }
    }
}