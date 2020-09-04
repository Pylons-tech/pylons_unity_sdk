using Newtonsoft.Json;
using PylonsSDK.Ipc;
using PylonsSDK.Ipc.Internal.Messages;
using System;

namespace PylonsSDK
{
    public static partial class Service
    {
        public static void RegisterProfile(string name, params IpcEvent[] evts) => new RegisterProfile(name).Broadcast(evts);
    }
}

namespace PylonsSDK.Ipc.Internal.Messages
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