using Newtonsoft.Json;

namespace PylonsSdk.Internal.Ipc.Messages
{
    /// <summary>
    /// IPC: Uses the linked wallet to determine if the recipe execution with the given UUID is finished.
    /// If the recipe is pay-per-execution and has an execution delay, the PayForCompletion
    /// flag must be set in order to consent to the acccount state transformation.
    /// 
    /// Gets a TxResponse containing the created CheckExecution transaction.
    /// </summary>
    public sealed class CheckExecution : IpcMessage
    {
        /// <summary>
        /// UUID of the execution to check.
        /// </summary>
        [JsonProperty("id")]
        public readonly string Id;
        /// <summary>
        /// Set to true if we want to pay costs to finalize execution of a delayed recipe, else false
        /// </summary>
        [JsonProperty("payForCompletion")]
        public readonly bool PayForCompletion;

        public CheckExecution(string id, bool payForCompletion) : base(ResponseType.TX_RESPONSE)
        {
            Id = id;
            PayForCompletion = payForCompletion;
        }
    }
}