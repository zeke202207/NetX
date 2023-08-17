namespace NetX.Common
{
    public static class DatetimeExtensions
    {
        /// <summary>
        /// 获取时间戳(UTC 1970-01-01 00:00:00)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long ToTimeStamp(this DateTime date)
        {
            return date.ToTimeStamp(new DateTime(1970, 1, 1, 0, 0, 0, 0));
        }

        /// <summary>
        /// 获取时间戳(UTC 1970-01-01 00:00:00)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long ToTimeStamp(this DateTime date, DateTime baseTime)
        {
            TimeSpan ts = date - baseTime;
            return Convert.ToInt64(ts.TotalMilliseconds);
        }
    }
}
