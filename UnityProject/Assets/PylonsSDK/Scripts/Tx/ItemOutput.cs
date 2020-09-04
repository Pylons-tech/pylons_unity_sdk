using System;

namespace PylonsSDK.Tx
{
    public readonly struct ItemOutput
    {
        public readonly DoubleOutputParam[] Doubles;
        public readonly LongOutputParam[] Longs;
        public readonly StringOutputParam[] Strings;
        public readonly ItemUpgradeParams ItemUpgradeParams;

        public ItemOutput(DoubleOutputParam[] doubles, LongOutputParam[] longs, StringOutputParam[] strings, ItemUpgradeParams itemUpgradeParams)
        {
            Doubles = doubles ?? throw new ArgumentNullException(nameof(doubles));
            Longs = longs ?? throw new ArgumentNullException(nameof(longs));
            Strings = strings ?? throw new ArgumentNullException(nameof(strings));
            ItemUpgradeParams = itemUpgradeParams;
        }
    }
}