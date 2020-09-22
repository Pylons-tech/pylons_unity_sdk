using Newtonsoft.Json;
using System;

namespace PylonsSdk.Internal.Ipc.Messages
{
    public sealed class GetProfile : IpcMessage
    {
        [JsonProperty("address")]
        public readonly string Address;

        public GetProfile(string address) : base(ResponseType.PROFILE_RESPONSE) // todo: this can actually be one of two things...
        {
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }
    }
}