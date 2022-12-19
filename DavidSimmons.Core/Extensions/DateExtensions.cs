using System;
using System.Globalization;

namespace DavidSimmons.Core.Extensions
{
    public static class DateExtensions
    {
        public static string ToStorageKey(this DateTime input)
        {
            return input.ToString("MMM-dd-yyyy", CultureInfo.InvariantCulture);
        }

        public static string ToMonthAndYearStorageKey(this DateTime input)
        {
            return input.ToString("MM-yyyy", CultureInfo.InvariantCulture);
        }
    }
}
