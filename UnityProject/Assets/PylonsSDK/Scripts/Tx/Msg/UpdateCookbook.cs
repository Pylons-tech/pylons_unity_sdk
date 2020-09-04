using Newtonsoft.Json;

namespace PylonsSDK.Tx.Msg
{
    public readonly struct UpdateCookbook
    {
        [JsonProperty("ID")]
        public readonly string CookbookId;
        [JsonProperty("Description")]
        public readonly string Description;
        [JsonProperty("Version")]
        public readonly string Version;
        [JsonProperty("Developer")]
        public readonly string Developer;
        [JsonProperty("SupportEmail")]
        public readonly string SupportEmail;
        [JsonProperty("Sender")]
        public readonly string Sender;

        public UpdateCookbook(string sender, string cookbookId, string description, string version, string developer, string supportEmail)
        {
            Sender = sender;
            CookbookId = cookbookId;
            Description = description;
            Version = version;
            Developer = developer;
            SupportEmail = supportEmail;
        }
    }
}