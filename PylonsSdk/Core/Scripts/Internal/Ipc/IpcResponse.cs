using Newtonsoft.Json;

namespace PylonsSdk.Internal.Ipc
{
    public readonly struct IpcResponse
    {
        [JsonProperty("messageId")]
        public readonly string MessageId;
        [JsonProperty("statusBlock")]
        public readonly StatusBlock StatusBlock;
        [JsonProperty("responseData")]
        public readonly string ResponseData;

        public IpcResponse(string messageId, StatusBlock statusBlock, string responseData)
        {
            MessageId = messageId;
            StatusBlock = statusBlock;
            ResponseData = responseData;
        }
    }
}