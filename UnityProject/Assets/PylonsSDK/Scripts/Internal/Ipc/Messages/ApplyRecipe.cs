using PylonsSdk.Internal.Ipc;
using PylonsSdk.Internal.Ipc.Messages;
using System;
using Newtonsoft.Json;

namespace PylonsSdk
{
    public static partial class Service
    {
        public static void ApplyRecipe (string recipe, string cookbook, string[] itemInputs, params IpcEvent[] evts) =>
            new ApplyRecipe(recipe, cookbook, itemInputs).Broadcast(evts);
    }
}

namespace PylonsSdk.Internal.Ipc.Messages
{
    public sealed class ApplyRecipe : IpcMessage
    {
        [JsonProperty("recipe")]
        public readonly string Recipe;
        [JsonProperty("cookbook")]
        public readonly string Cookbook;
        [JsonProperty("itemInputs")]
        public readonly string[] ItemInputs;

        public ApplyRecipe(string recipe, string cookbook, string[] itemInputs) : base(ResponseType.TX_RESPONSE)
        {
            Recipe = recipe ?? throw new ArgumentNullException(nameof(recipe));
            Cookbook = cookbook ?? throw new ArgumentNullException(nameof(cookbook));
            ItemInputs = itemInputs ?? throw new ArgumentNullException(nameof(itemInputs));
        }
    }
}