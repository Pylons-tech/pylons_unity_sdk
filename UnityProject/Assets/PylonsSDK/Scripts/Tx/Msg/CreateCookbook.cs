using Newtonsoft.Json;

namespace PylonsSDK.Tx.Msg
{
    public readonly struct CreateCookbook
    {
        [JsonProperty("CookbookID")]
        public readonly string CookbookId;
        [JsonProperty("Name")]
        public readonly string Name;
        [JsonProperty("Description")]
        public readonly string Description;
        [JsonProperty("Version")]
        public readonly string Version;
        [JsonProperty("Developer")]
        public readonly string Developer;
        [JsonProperty("SupportEmail")]
        public readonly string SupportEmail;
        [JsonProperty("Level")]
        public readonly long Level;
        [JsonProperty("Sender")]
        public readonly string Sender;
        [JsonProperty("CostPerBlock")]
        public readonly long CostPerBlock;

        public CreateCookbook(string sender, string cookbookId, string name, string description, string version, string developer, string supportEmail, long level, long costPerBlock)
        {
            Sender = sender;
            CookbookId = cookbookId;
            Name = name;
            Description = description;
            Version = version;
            Developer = developer;
            SupportEmail = supportEmail;
            Level = level;
            CostPerBlock = costPerBlock;
        }
    }
}