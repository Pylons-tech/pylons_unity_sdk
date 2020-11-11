using Newtonsoft.Json;

namespace PylonsSdk.Internal.Ipc.Messages
{
    /// <summary>
    /// IPC: Batch operation. Create one or more cookbooks (sequentially, not simultaneously) using the linked wallet.
    /// Linked wallet must have a keypair. Note that CreateCookbooks should never be called in production code; end-user
    /// wallets are not able to send the cookbook/recipe creation/maintenance messages.
    /// 
    /// In general, CreateCookbooks is designed to be called by the automated, human-friendly recipe builder provided with the
    /// Pylons SDK, which takes advantage of batching capabilities to create/update the project's cookbooks en masse.
    /// It is advised that you do not use the low-level IPC functions unless absolutely required.
    /// 
    /// Each argument is an array. All of these arrays are expected to be the same length; the wallet iterates over the IDs
    /// given and uses the entries in the other arrays w/ the corresponding indices to build the message.
    /// (Note that we don't have a non-batch CreateCookbook message, to cut down on redundant code. If you want to create
    /// a single cookbook, just use arrays of length 1. Contractually speaking, creating *zero* cookbooks should work, though
    /// obviously that should never actually be done.)
    /// 
    /// Gets a TxResponse containing all of the created CreateCookbook transactions.
    /// </summary>
    public sealed class CreateCookbooks : IpcMessage
    {
        /// <summary>
        /// An array containing the unique IDs of the cookbooks to be created. It is the responsibility of the caller to ensure
        /// that these IDs actually *are* unique; if a cookbook with that ID already exists, the attempt to create it will fail.
        /// (As a usability concern, the dev tools provided w/ the Pylons SDK should avoid collisions on users' behalves.)
        /// </summary>
        [JsonProperty("ids")]
        public readonly string[] Ids;
        /// <summary>
        /// An array containing the human-friendly names of the cookbooks to be created. These do not need to be unique.
        /// </summary>
        [JsonProperty("names")]
        public readonly string[] Names;
        /// <summary>
        /// An array containing the developer strings of the cookbooks to be created. In real-world use these are almost always
        /// going to be the same string, of course.
        /// </summary>
        [JsonProperty("developers")]
        public readonly string[] Developers;
        /// <summary>
        /// An array containing the description strings of the cookbooks to be created.
        /// </summary>
        [JsonProperty("descriptions")]
        public readonly string[] Descriptions;
        /// <summary>
        /// An array containing the (initial) SemVer version numbers of the cookbooks to be created.
        /// </summary>
        [JsonProperty("versions")]
        public readonly string[] Versions;
        /// <summary>
        /// An array containing the support email addresses for the cookbooks to be created.
        /// </summary>
        [JsonProperty("supportEmails")]
        public readonly string[] SupportEmails;
        /// <summary>
        /// Deprecated. Same length as Ids, each entry is always 0. TODO: remove this
        /// </summary>
        [JsonProperty("levels")]
        public readonly long[] Levels;
        /// <summary>
        /// Array containing the number of Pylons (per block-time interval) to be charged to end users for 
        /// completion of delayed recipes for each cookbook to be created.
        /// Note that this is cookbook-global. (TODO: this field may be eliminated in future.)
        /// </summary>
        [JsonProperty("costsPerBlock")]
        public readonly long[] CostsPerBlock;

        public CreateCookbooks(string[] ids, string[] names, string[] developers, string[] descriptions, string[] versions, string[] supportEmails, long[] levels, long[] costsPerBlock) : base(ResponseType.TX_RESPONSE)
        {
            Ids = ids;
            Names = names;
            Developers = developers;
            Descriptions = descriptions;
            Versions = versions;
            SupportEmails = supportEmails;
            Levels = levels;
            CostsPerBlock = costsPerBlock;
        }
    }
}