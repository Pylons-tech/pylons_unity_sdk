using System;

namespace PylonsSdk
{
    public static partial class Util
    {
        public readonly static DateTime UnixEpoch = new DateTime(1970, 1, 1);

        public static DateTime GetDateTimeFromUnixTimestamp(long timestamp) => UnixEpoch.AddMilliseconds(timestamp);

        // TODO: this needs tests, i don't trust it
        public static long GetUnixTimestampForDateTime(DateTime dt) => (long)dt.Subtract(UnixEpoch).TotalMilliseconds;
    }
}