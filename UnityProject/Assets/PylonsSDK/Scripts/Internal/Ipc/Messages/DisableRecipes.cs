using Newtonsoft.Json;
using PylonsSdk.Internal.Ipc;
using PylonsSdk.Internal.Ipc.Messages;
using System;

namespace PylonsSdk
{
    public static partial class Service
    {
        public static void DisableRecipes(string[] recipes, params IpcEvent[] evts) =>
            new DisableRecipes(recipes).Broadcast(evts);
    }
}

namespace PylonsSdk.Internal.Ipc.Messages
{
    public sealed class DisableRecipes : IpcMessage
    {
        [JsonProperty("recipes")]
        public readonly string[] Recipes;

        public DisableRecipes(string[] recipes) : base(ResponseType.TX_RESPONSE)
        {
            Recipes = recipes ?? throw new ArgumentNullException(nameof(recipes));
        }
    }
}