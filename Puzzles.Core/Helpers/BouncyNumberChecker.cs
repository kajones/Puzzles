using System.Linq;

namespace Puzzles.Core.Helpers
{
    public static class BouncyNumberChecker
    {
        public static bool IsBouncy(long candidate)
        {
            var digits = DigitHelper.GetDigits(candidate).ToArray();
            var isIncreasing = true;
            var isDecreasing = true;
            for (var idx = 0; idx < (digits.Length - 1); ++idx)
            {
                var currDigit = digits[idx];
                var nextDigit = digits[idx + 1];
                if (currDigit < nextDigit)
                {
                    isDecreasing = false;
                    if (!isIncreasing && !isDecreasing) return true;
                }
                else if (currDigit > nextDigit)
                {
                    isIncreasing = false;
                    if (!isIncreasing && !isDecreasing) return true;
                }
            }

            return false;
        }
    }
}
