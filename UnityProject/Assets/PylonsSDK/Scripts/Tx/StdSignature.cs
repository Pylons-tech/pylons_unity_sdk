using System;

namespace PylonsSDK.Tx
{
    public readonly struct StdSignature
    {
        public readonly string Signature;
        public readonly PubKey PubKey;

        public StdSignature(string signature, PubKey pubKey)
        {
            Signature = signature ?? throw new ArgumentNullException(nameof(signature));
            PubKey = pubKey;
        }
    }
}