using System;

namespace PylonsSdk.Tx
{
    public readonly struct LongOutputParam
    {
        public readonly string Program;
        public readonly double Rate;
        public readonly double Key;
        public readonly LongWeightRange[] Ranges;

        public LongOutputParam(string program, double rate, double key, LongWeightRange[] ranges)
        {
            Program = program ?? throw new ArgumentNullException(nameof(program));
            Rate = rate;
            Key = key;
            Ranges = ranges ?? throw new ArgumentNullException(nameof(ranges));
        }
    }
}