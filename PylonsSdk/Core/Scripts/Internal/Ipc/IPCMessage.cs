using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PylonsIpc;

namespace PylonsSdk.Internal.Ipc
{
    public abstract class IpcMessage
    {
        private readonly int MessageId = new Random().Next();

        protected enum ResponseType
        {
            NONE = 0,
            TEST_RESPONSE = 1,
            TX_RESPONSE = 2,
            PROFILE_RESPONSE = 3,
            ITEM_RESPONSE = 4,
            RECIPE_RESPONSE = 5,
            COOKBOOK_RESPONSE = 6,
            TRADE_RESPONSE = 7,
            EXECUTION_RESPONSE = 8
        }

        private readonly ResponseType responseType;

        public event IpcEvent onResolution;

        protected IpcMessage(ResponseType rt) => responseType = rt;

        private string Serialize() => JsonConvert.SerializeObject(this);

        public void Broadcast(IpcEvent[] evts)
        {
            if (DebugMessageEncoder.WalletId == 0) throw new Exception("Handshake hasn't been done yet. Wait for connection state to become sane before sending messages.");
            foreach (var evt in evts) onResolution += evt;
            var interaction = new IpcInteraction(new
            {
                type = GetType().Name,
                msg = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(Serialize())),
                messageId = MessageId,
                clientId = DebugMessageEncoder.ClientId,
                walletId = DebugMessageEncoder.WalletId
            });
            interaction.OnResolution += HandleResponse;
            interaction.Resolve();
        }

        private object ParseResponse(string response)
        {
            var obj = JsonConvert.DeserializeObject(response) as dynamic;
            // TODO: validate the response
            var responseData = (JObject)obj.responseData;
            if (obj.clientId != DebugMessageEncoder.ClientId || obj.messageId != MessageId || obj.walletId != DebugMessageEncoder.WalletId)
                throw new Exception("Client/Message/Wallet ID mismatch");

            switch (responseType)
            {
                case ResponseType.TEST_RESPONSE:
                    return responseData.ToObject<TestResponse>();
                case ResponseType.TX_RESPONSE:
                    return responseData.ToObject<TxResponse>();
                case ResponseType.PROFILE_RESPONSE:
                    return responseData.ToObject<ProfileResponse>();
                case ResponseType.ITEM_RESPONSE:
                    return responseData.ToObject<ItemResponse>();
                case ResponseType.RECIPE_RESPONSE:
                    return responseData.ToObject<RecipeResponse>();
                case ResponseType.COOKBOOK_RESPONSE:
                    return responseData.ToObject<CookbookResponse>();
                case ResponseType.TRADE_RESPONSE:
                    return responseData.ToObject<TradeResponse>();
                case ResponseType.EXECUTION_RESPONSE:
                    return responseData.ToObject<ExecutionResponse>();
                default:
                    throw new ArgumentOutOfRangeException(nameof(responseType));
            }
        }

        private void HandleResponse(object caller, IpcInteraction.IpcInteractionEventArgs args)
        {
            var response = ParseResponse(args.interaction.receivedMessage);
            onResolution.Invoke(this, response);
        }

    }
}