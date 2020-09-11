using Newtonsoft.Json;
using PylonsSdk.Internal.Ipc;
using PylonsSdk.Internal.Ipc.Messages;

namespace PylonsSdk
{
    public static partial class Service
    {
        public static void GoogleIapGetPylons(string productId, string purchaseToken, string receiptData, string signature, params IpcEvent[] evts) => 
            new GoogleIapGetPylons(productId, purchaseToken, receiptData, signature).Broadcast(evts);
    }
}

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