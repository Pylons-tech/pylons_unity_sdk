using Newtonsoft.Json;
using System;

namespace PylonsSdk.Internal.Ipc.Messages
{
    public sealed class CreateRecipes : IpcMessage
    {
        [JsonProperty("names")]
        public readonly string[] Names;
        [JsonProperty("cookbooks")]
        public readonly string[] Cookbooks;
        [JsonProperty("descriptions")]
        public readonly string[] Decriptions;
        [JsonProperty("blockIntervals")]
        public readonly long[] BlockIntervals;
        [JsonProperty("coinInputs")]
        public readonly string[] CoinInputs;
        [JsonProperty("itemInputs")]
        public readonly string[] ItemInputs;
        [JsonProperty("outputTables")]
        public readonly string[] OutputTables;
        [JsonProperty("outputs")]
        public readonly string[] Outputs;

        public CreateRecipes(string[] names, string[] cookbooks, string[] decriptions, long[] blockIntervals, 
            string[] coinInputs, string[] itemInputs, string[] outputTables, string[] outputs) : base(ResponseType.TX_RESPONSE)
        {
            Names = names ?? throw new ArgumentNullException(nameof(names));
            Cookbooks = cookbooks ?? throw new ArgumentNullException(nameof(cookbooks));
            Decriptions = decriptions ?? throw new ArgumentNullException(nameof(decriptions));
            BlockIntervals = blockIntervals ?? throw new ArgumentNullException(nameof(blockIntervals));
            CoinInputs = coinInputs ?? throw new ArgumentNullException(nameof(coinInputs));
            ItemInputs = itemInputs ?? throw new ArgumentNullException(nameof(itemInputs));
            OutputTables = outputTables ?? throw new ArgumentNullException(nameof(outputTables));
            Outputs = outputs ?? throw new ArgumentNullException(nameof(outputs));
        }
    }
}