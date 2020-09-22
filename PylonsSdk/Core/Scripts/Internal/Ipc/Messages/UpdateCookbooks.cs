using Newtonsoft.Json;
using System;

namespace PylonsSdk.Internal.Ipc.Messages
{
    public sealed class UpdateCookbooks : IpcMessage
    {
        [JsonProperty("ids")]
        public readonly string[] Ids;
        [JsonProperty("names")]
        public readonly string[] Names;
        [JsonProperty("developers")]
        public readonly string[] Developers;
        [JsonProperty("descriptions")]
        public readonly string[] Descriptions;
        [JsonProperty("versions")]
        public readonly string[] Versions;
        [JsonProperty("supportEmails")]
        public readonly string[] SupportEmails;

        public UpdateCookbooks(string[] ids, string[] names, string[] developers, string[] descriptions, string[] versions, string[] supportEmails) : base(ResponseType.TX_RESPONSE)
        {
            Ids = ids ?? throw new ArgumentNullException(nameof(ids));
            Names = names ?? throw new ArgumentNullException(nameof(names));
            Developers = developers ?? throw new ArgumentNullException(nameof(developers));
            Descriptions = descriptions ?? throw new ArgumentNullException(nameof(descriptions));
            Versions = versions ?? throw new ArgumentNullException(nameof(versions));
            SupportEmails = supportEmails ?? throw new ArgumentNullException(nameof(supportEmails));
        }
    }
}