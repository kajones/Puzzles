using System;

namespace Puzzles.Core.Helpers
{
    public static class LetterHelper
    {
        private static readonly long AValue = Convert.ToInt32('A');

        public static long GetValue(string text)
        {
            long total = 0;

            for (var idx = 0; idx < text.Length; ++idx)
            {
                var c = text[idx];
                var cValue = Convert.ToInt32(c) - AValue + 1;
                total += cValue;
            }

            return total;
        }
    }
}
