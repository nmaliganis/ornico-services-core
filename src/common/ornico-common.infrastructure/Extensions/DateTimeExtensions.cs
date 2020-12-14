using System;

namespace ornico.common.infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToUrlFriendlyDate(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }
    }
}