using Newtonsoft.Json;
using System;

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