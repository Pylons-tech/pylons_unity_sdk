using System;

namespace PylonsSDK.Tx
{
    public readonly struct ItemUpgradeParams
    {
        public readonly DoubleInputParam[] Doubles;
        public readonly LongInputParam[] Longs;
        public readonly StringInputParam[] Strings;

        public ItemUpgradeParams(DoubleInputParam[] doubles, LongInputParam[] longs, StringInputParam[] strings)
        {
            Doubles = doubles ?? throw new ArgumentNullException(nameof(doubles));
            Longs = longs ?? throw new ArgumentNullException(nameof(longs));
            Strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }
    }
}