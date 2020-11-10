using Newtonsoft.Json;
using System;

namespace PylonsSdk.Internal.Ipc.Messages
{
    /// <summary>
    /// IPC: Batch operation. Creates one or more recipes (sequentially, not simultaneously) using the linked wallet.
    /// Linked wallet must have a keypair. Note that CreateRecipes should never be called in production code; end-user
    /// wallets are not able to send the cookbook/recipe creation/maintenance messages.
    /// 
    /// In general, CreateRecipes is designed to be called by the automated, human-friendly recipe builder provided with the
    /// Pylons SDK, which takes advantage of batching capabilities to create/update the project's recipes en masse.
    /// It is advised that you do not use the low-level IPC functions unless absolutely required.
    /// 
    /// Each argument is an array. All of these arrays are expected to be the same length; the wallet iterates over the names
    /// given and uses the entries in the other arrays w/ the corresponding indices to build the message.
    /// (Note that we don't have a non-batch CreateRecipe message, to cut down on redundant code. If you want to create
    /// a single recipe, just use arrays of length 1. Contractually speaking, creating *zero* recipes should work, though
    /// obviously that should never actually be done.)
    /// 
    /// Gets a TxResponse containing all of the created CreateRecipe transactions.
    /// </summary>
    public sealed class CreateRecipes : IpcMessage
    {
        /// <summary>
        /// An array containing the human-friendly names of the recipes to be created. These do not need to be unique.
        /// </summary>
        [JsonProperty("names")]
        public readonly string[] Names;
        /// <summary>
        /// An array containing the UUIDs of the cookbooks each recipe belongs to. The active keypair must own the cookbook
        /// in question.
        /// </summary>
        [JsonProperty("cookbooks")]
        public readonly string[] Cookbooks;
        /// <summary>
        /// An array containing the description strings for the recipes to be created.
        /// </summary>
        [JsonProperty("descriptions")]
        public readonly string[] Decriptions;
        /// <summary>
        /// An array containing the amount of block-time intervals that must elapse before the recipe is completed.
        /// If greater than 0, the recipe is considered to possess an execution delay, and (if the cookbook's costsPerBlock are > 0)
        /// completion must be pair for w/ the CheckExecution message before it will finish.
        /// </summary>
        [JsonProperty("blockIntervals")]
        public readonly long[] BlockIntervals;
        /// <summary>
        /// An array containing serialized JSON arrays of CoinInput structs. This is an array-of-arrays; the JSON arrays are just serialized before
        /// processing. PylonsSdk.Tx.CoinInput[] will serialize correctly.
        /// </summary>
        [JsonProperty("coinInputs")]
        public readonly string[] CoinInputs;
        /// <summary>
        /// An array containing serialized JSON arrays of ItemInput structs. This is an array-of-arrays; the JSON arrays are just serialized before
        /// processing. PylonsSdk.Tx.ItemInput[] will serialize correctly.
        /// </summary>
        [JsonProperty("itemInputs")]
        public readonly string[] ItemInputs;
        /// <summary>
        /// An array of JSON-serialized EntriesList structs. PylonsSdk.Tx.EntriesList will serialize correctly.
        /// </summary>
        [JsonProperty("outputTables")]
        public readonly string[] OutputTables;
        /// <summary>
        /// An array containing serialized JSON arrays of WeightedOutput structs. This is an array-of-arrays; the JSON arrays are just serialized before
        /// processing. PylonsSdk.Tx.WeightedOutput[] will serialize correctly.
        /// </summary>
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