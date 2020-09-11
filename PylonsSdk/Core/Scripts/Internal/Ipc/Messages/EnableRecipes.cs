using Newtonsoft.Json;
using PylonsSdk.Internal.Ipc;
using PylonsSdk.Internal.Ipc.Messages;
using System;

namespace PylonsSdk
{
    public static partial class Service
    {
        public static void EnableRecipes(string[] recipes, params IpcEvent[] evts) =>
            new EnableRecipes(recipes).Broadcast(evts);
    }
}

namespace PylonsSdk.Internal.Ipc.Messages
{
    public sealed class EnableRecipes : IpcMessage
    {
        [JsonProperty("recipes")]
        public readonly string[] Recipes;

        public EnableRecipes(string[] recipes) : base(ResponseType.TX_RESPONSE)
        {
            Recipes = recipes ?? throw new ArgumentNullException(nameof(recipes));
        }
    }
}