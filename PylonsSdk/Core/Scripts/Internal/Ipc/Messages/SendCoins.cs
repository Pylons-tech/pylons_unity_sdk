using Newtonsoft.Json;

namespace PylonsSdk.Internal.Ipc.Messages
{
    /// <summary>
    /// IPC: Sends coins to another account using the linked wallet.
    /// Linked wallet must have a keypair. If we don't have all the keys we want to send
    /// or an account doesn't exist at the given address, the operation will fail.
    /// 
    /// Gets a TxResponse contaaining the created SendCoins message.
    /// </summary>
    public sealed class SendCoins : IpcMessage
    {
        /// <summary>
        /// Serialized JSON array of Coin structs corresponding to the coins to be sent. Tx.Coin[] serializes correctly. 
        /// </summary>
        [JsonProperty("coins")]
        public readonly string Coins;
        /// <summary>
        /// Address of destination account.
        /// </summary>
        [JsonProperty("receiver")]
        public readonly string Receiver;

        public SendCoins(string coins, string receiver) : base(ResponseType.TX_RESPONSE)
        {
            Coins = coins;
            Receiver = receiver;
        }
    }
}