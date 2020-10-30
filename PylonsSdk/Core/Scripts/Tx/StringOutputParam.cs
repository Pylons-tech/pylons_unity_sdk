using System;

namespace PylonsSdk.Tx
{
    public readonly struct StringOutputParam
    {
        public readonly string Program;
        public readonly double Rate;
        public readonly string Key;
        public readonly string Value;

        public StringOutputParam(string program, double rate, string key, string value)
        {
            Program = program ?? throw new ArgumentNullException(nameof(program));
            Rate = rate;
            Key = key;
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}