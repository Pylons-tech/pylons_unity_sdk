namespace PylonsSDK.Tx
{
    public readonly struct LongWeightRange
    {
        public readonly long Upper;
        public readonly long Lower;
        public readonly double Weight;

        public LongWeightRange(long upper, long lower, double weight)
        {
            Upper = upper;
            Lower = lower;
            Weight = weight;
        }
    }
}