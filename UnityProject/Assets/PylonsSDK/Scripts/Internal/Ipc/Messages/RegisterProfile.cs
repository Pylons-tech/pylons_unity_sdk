using Newtonsoft.Json;
using PylonsSdk.Internal.Ipc;
using PylonsSdk.Internal.Ipc.Messages;
using System;

namespace PylonsSdk
{
    public static partial class Service
    {
        public static void RegisterProfile(string name, params IpcEvent[] evts) => new RegisterProfile(name).Broadcast(evts);
    }
}

namespace PylonsSdk.Internal.Ipc.Messages
{
    public sealed class RegisterProfile : IpcMessage
    {
        [JsonProperty("name")]
        public readonly string Name;

        public RegisterProfile(string name) : base(ResponseType.TX_RESPONSE)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}