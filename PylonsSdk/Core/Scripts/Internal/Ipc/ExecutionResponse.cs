using Newtonsoft.Json;
using PylonsSdk.Tx;

namespace PylonsSdk.Internal.Ipc
{
    public readonly struct ExecutionResponse
    {
        [JsonProperty("executions")]
        public readonly Execution[] Executions;

        public ExecutionResponse(Execution[] executions)
        {
            Executions = executions;
        }
    }
}