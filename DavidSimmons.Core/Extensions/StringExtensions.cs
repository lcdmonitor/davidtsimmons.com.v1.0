using System;

using System.Text.RegularExpressions;

namespace DavidSimmons.Core.Extensions
{
    public static class StringExtensions
    {
        static Regex notAlphaNumExpression = new Regex("[^a-zA-Z\\d]");

        public static string ToStorageKey(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return String.Empty;
            }

            return
                notAlphaNumExpression.Replace(
                    input
                    .Replace(" ", "-")
                    .Replace("/", "-")
                    .Replace("\\", " ")
                    .Replace("#", "-")
                    .Replace("?", "-")
                    .Replace("\t", "-")
                    .Replace("\n", "-")
                    .Replace("\n", "-")
                    .Replace(Environment.NewLine, "-"),
                    "-"
                );

        }
    }
}
