using System;

namespace PylonsSdk.Tx
{
    public readonly struct PubKey
    {
        public readonly string Type;
        public readonly string Value;

        public PubKey(string type, string value)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}