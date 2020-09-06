using Newtonsoft.Json;
using PylonsSdk.Internal.Ipc;
using PylonsSdk.Internal.Ipc.Messages;
using System;

namespace PylonsSdk
{
    public static partial class Service
    {
        public static void GetProfile(string id, params IpcEvent[] evts) => new GetProfile(id).Broadcast(evts);
    }
}

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