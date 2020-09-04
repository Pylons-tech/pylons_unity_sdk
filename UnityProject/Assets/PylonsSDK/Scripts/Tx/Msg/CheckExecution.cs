using Newtonsoft.Json;

namespace PylonsSDK.Tx.Msg
{
    public readonly struct CheckExecution
    {
        [JsonProperty("ExecID")]
        public readonly string ExecId;
        [JsonProperty("Sender")]
        public readonly string Sender;
        [JsonProperty("PayToComplete")]
        public readonly bool PayToComplete;

        public CheckExecution(string sender, string execId, bool payToComplete)
        {
            Sender = sender;
            ExecId = execId;
            PayToComplete = payToComplete;
        }
    }
}