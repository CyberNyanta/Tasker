using System;

namespace Tasker.Core.AL.Utils
{
    public static class DateConverterExtensions
    {
        public static DateTime UnixTimeToDateTime(this long date)
        {                
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddMilliseconds(date);
        }

        public static long ToUnixTime(this DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(new TimeSpan(date.Ticks - epoch.Ticks)).TotalMilliseconds;
        }
    }
}