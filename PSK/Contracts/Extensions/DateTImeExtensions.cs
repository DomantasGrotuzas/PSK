using System;

namespace Contracts.Extensions
{
    public static class DateTImeExtensions
    {
        public static DateTime ClearHours(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        }
    }
}
