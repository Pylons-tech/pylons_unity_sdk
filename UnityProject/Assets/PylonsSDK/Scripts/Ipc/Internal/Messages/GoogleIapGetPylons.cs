using Newtonsoft.Json;
using PylonsSDK.Ipc;
using PylonsSDK.Ipc.Internal.Messages;

namespace PylonsSDK
{
    public static partial class Service
    {
        public static void GoogleIapGetPylons(string productId, string purchaseToken, string receiptData, string signature, params IpcEvent[] evts) => 
            new GoogleIapGetPylons(productId, purchaseToken, receiptData, signature).Broadcast(evts);
    }
}

namespace PylonsSDK.Ipc.Internal.Messages
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