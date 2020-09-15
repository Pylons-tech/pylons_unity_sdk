using System;
using PylonsIpc;

namespace PylonsSdk.Internal.Ipc
{
    public abstract class IpcMessage
    {
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

        private string Serialize() => Newtonsoft.Json.JsonConvert.SerializeObject(this);

        public void Broadcast(IpcEvent[] evts)
        {
            foreach (var evt in evts) onResolution += evt;
            var msg = Serialize();
            msg = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(msg));
            var json = $"{{ \"type\":\"{GetType().Name}\", \"msg\":\"{msg}\" }}";
            var interaction = new IpcInteraction(json);
            interaction.OnResolution += HandleResponse;
            interaction.Resolve();
        }

        private static T DeserializeResponse<T>(string json) => Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);

        private object ParseResponse(string responseJson)
        {
            switch (responseType)
            {
                case ResponseType.TEST_RESPONSE:
                    return DeserializeResponse<TestResponse>(responseJson);
                case ResponseType.TX_RESPONSE:
                    return DeserializeResponse<TxResponse>(responseJson);
                case ResponseType.PROFILE_RESPONSE:
                    return DeserializeResponse<ProfileResponse>(responseJson);
                case ResponseType.ITEM_RESPONSE:
                    return DeserializeResponse<ItemResponse>(responseJson);
                case ResponseType.RECIPE_RESPONSE:
                    return DeserializeResponse<RecipeResponse>(responseJson);
                case ResponseType.COOKBOOK_RESPONSE:
                    return DeserializeResponse<CookbookResponse>(responseJson);
                case ResponseType.TRADE_RESPONSE:
                    return DeserializeResponse<TradeResponse>(responseJson);
                case ResponseType.EXECUTION_RESPONSE:
                    throw new NotImplementedException();
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