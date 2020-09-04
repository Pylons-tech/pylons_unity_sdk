using System;

namespace PylonsSDK.Tx
{
    public readonly struct DoubleOutputParam
    {
        public readonly string Program;
        public readonly double Rate;
        public readonly double Key;
        public readonly DoubleWeightRange[] Ranges;

        public DoubleOutputParam(string program, double rate, double key, DoubleWeightRange[] ranges)
        {
            Program = program ?? throw new ArgumentNullException(nameof(program));
            Rate = rate;
            Key = key;
            Ranges = ranges ?? throw new ArgumentNullException(nameof(ranges));
        }
    }
}