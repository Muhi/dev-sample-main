using System;

namespace DeveloperSample.Algorithms
{
    public static class Algorithms
    {
        public static int GetFactorial(int n) {

            if (n < 0)
            throw new ArgumentException("Factorial non-negative numbers.");

            int result = 1;
            for (int i = 2; i<=n; i++){
                result *=i;
            }
            return result;
        }

        public static string FormatSeparators(params string[] items)
        {
            if (items == null || items.Length == 0)
            {
                return string.Empty;
            }
            else if (items.Length == 1)
            {
                return items[0];
            }
            else if (items.Length == 2)
            {
                return $"{items[0]} and {items[1]}";
            }
            else
            {
                string allButLast = string.Join(", ", items, 0, items.Length - 1);
                //https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-8.0/ranges
                return $"{allButLast} and {items[^1]}";
            }
        }
    }
}