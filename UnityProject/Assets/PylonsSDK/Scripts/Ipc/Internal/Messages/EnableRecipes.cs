using Newtonsoft.Json;
using PylonsSDK.Ipc;
using PylonsSDK.Ipc.Internal.Messages;
using System;

namespace PylonsSDK
{
    public static partial class Service
    {
        public static void EnableRecipes(string[] recipes, params IpcEvent[] evts) =>
            new EnableRecipes(recipes).Broadcast(evts);
    }
}

namespace PylonsSDK.Ipc.Internal.Messages
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