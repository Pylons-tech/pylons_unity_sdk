namespace PylonsSDK.Tx
{
    public readonly struct DoubleWeightRange
    {
        public readonly double Upper;
        public readonly double Lower;
        public readonly double Weight;

        public DoubleWeightRange(double upper, double lower, double weight)
        {
            Upper = upper;
            Lower = lower;
            Weight = weight;
        }
    }
}