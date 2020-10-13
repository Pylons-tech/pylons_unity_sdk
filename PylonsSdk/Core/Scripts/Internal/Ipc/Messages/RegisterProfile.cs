using Newtonsoft.Json;
using System;

namespace PylonsSdk.Internal.Ipc.Messages
{
    public sealed class RegisterProfile : IpcMessage
    {
        [JsonProperty("name")]
        public readonly string Name;
        [JsonProperty("makeKeys")]
        public readonly bool MakeKeys;

        public RegisterProfile(string name, bool makeKeys) : base(ResponseType.TX_RESPONSE)
        {
            Name = name;
            MakeKeys = makeKeys;
        }
    }
}