using Newtonsoft.Json;

namespace PylonsSdk.Internal.Ipc.Messages
{
    /// <summary>
    /// IPC: Batch operation. Update one or more cookbooks (sequentially, not simultaneously) using the linked wallet.
    /// Linked wallet must have a keypair. Note that UpdateCookbooks should never be called in production code; end-user
    /// wallets are not able to send the cookbook/recipe creation/maintenance messages.
    /// 
    /// In general, UpdateCookbooks is designed to be called by the automated, human-friendly recipe builder provided with the
    /// Pylons SDK, which takes advantage of batching capabilities to create/update the project's cookbooks en masse.
    /// It is advised that you do not use the low-level IPC functions unless absolutely required.
    /// 
    /// Each argument is an array. All of these arrays are expected to be the same length; the wallet iterates over the IDs
    /// given and uses the entries in the other arrays w/ the corresponding indices to build the message.
    /// (Note that we don't have a non-batch UpdateCookbook message, to cut down on redundant code. If you want to create
    /// a single cookbook, just use arrays of length 1. Contractually speaking, updating *zero* cookbooks should work, though
    /// obviously that should never actually be done.)
    /// 
    /// Gets a TxResponse containing all of the created UpdateCookbook transactions.
    /// </summary>
    public sealed class UpdateCookbooks : IpcMessage
    {
        /// <summary>
        /// An array containing the unique IDs of the cookbooks to be updated. If we don't own these, the operation fails.
        /// </summary>
        [JsonProperty("ids")]
        public readonly string[] Ids;
        /// <summary>
        /// An array containing the human-friendly names of the cookbooks to be updated. These do not need to be unique.
        /// </summary>
        [JsonProperty("names")]
        public readonly string[] Names;
        /// <summary>
        /// An array containing the developer strings of the cookbooks to be updated. In real-world use these are almost always
        /// going to be the same string, of course.
        /// </summary>
        [JsonProperty("developers")]
        public readonly string[] Developers;
        /// <summary>
        /// An array containing the description strings of the cookbooks to be updated.
        /// </summary>
        [JsonProperty("descriptions")]
        public readonly string[] Descriptions;
        /// <summary>
        /// An array containing the SemVer version numbers of the cookbooks to be updated.
        /// </summary>
        [JsonProperty("versions")]
        public readonly string[] Versions;
        /// <summary>
        /// An array containing the support email addresses of the cookbooks to be updated.
        /// </summary>
        [JsonProperty("supportEmails")]
        public readonly string[] SupportEmails;

        public UpdateCookbooks(string[] ids, string[] names, string[] developers, string[] descriptions, string[] versions, string[] supportEmails) : base(ResponseType.TX_RESPONSE)
        {
            Ids = ids;
            Names = names;
            Developers = developers;
            Descriptions = descriptions;
            Versions = versions;
            SupportEmails = supportEmails;
        }
    }
}