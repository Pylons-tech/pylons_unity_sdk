using System;

namespace PylonsSdk.Tx
{
    public readonly struct Cookbook
    {
        public readonly string Id;
        public readonly string Name;
        public readonly string Description;
        public readonly string Version;
        public readonly string Developer;
        public readonly string Sender;
        public readonly string SupportEmail;
        public readonly long CostPerBlock;
        public readonly long Level;

        public Cookbook(string id, string name, string description, string version, string developer, string sender, string supportEmail, long costPerBlock, long level)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Version = version ?? throw new ArgumentNullException(nameof(version));
            Developer = developer ?? throw new ArgumentNullException(nameof(developer));
            Sender = sender ?? throw new ArgumentNullException(nameof(sender));
            SupportEmail = supportEmail ?? throw new ArgumentNullException(nameof(supportEmail));
            CostPerBlock = costPerBlock;
            Level = level;
        }
    }
}