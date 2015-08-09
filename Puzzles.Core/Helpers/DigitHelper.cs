using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;

namespace Puzzles.Core.Helpers
{
    public static class DigitHelper
    {
        public static long GetDigitSum(string numberAsText)
        {
            long sum = 0;
            for (var idx = 0; idx < numberAsText.Length; ++idx)
            {
                sum += Convert.ToInt64(numberAsText.Substring(idx, 1));
            }
            return sum;
        }

        public static IEnumerable<int> GetDigits(string numberAsText)
        {
            return numberAsText.Select((t, idx) => Convert.ToInt32(numberAsText.Substring(idx, 1)));
        }

        public static IEnumerable<int> GetDigits(BigInteger number)
        {
            return GetDigits(number.ToString());
        }

        public static IEnumerable<int> GetDigits(decimal number)
        {
            return GetDigits(number.ToString(CultureInfo.InvariantCulture));
        }

        public static IEnumerable<int> GetDigits(long number)
        {
            var len = GetNumberLength(number);

            // Single digit number
            if (len == 0)
            {
                return new[] { (int)number };
            }

            var digits = new int[len];

            long parseNo = number;
            var dec = (long)Math.Pow(10, len - 1);

            for (var i = len - 1; i >= 0; --i)
            {
                digits[len - i - 1] = (int)(parseNo / dec);
                parseNo -= digits[len - i - 1] * dec;
                dec /= 10;
            }

            return digits;

        }

        public static IEnumerable<int> GetDigits(int number)
        {
            var len = GetNumberLength(number);

            // Single digit number
            if (len == 0)
            {
                return new[] { number };
            }

            var digits = new int[len];

            var parseNo = number;
            var dec = (int)Math.Pow(10, len - 1);

            for (var i = len - 1; i >= 0; --i)
            {
                digits[len - i - 1] = parseNo / dec;
                parseNo -= digits[len - i - 1] * dec;
                dec /= 10;
            }

            return digits;
        }

        public static int GetNumberLength(int number)
        {
            if (number == 0) return 1;

            return (int)Math.Ceiling(Math.Log10(number + 1));
        }

        public static int GetNumberLength(long number)
        {
            if (number == 0) return 1;

            return (int)Math.Ceiling(Math.Log10(number + 1));
        }

        public static int GetNumberLength(BigInteger number)
        {
            if (number == 0) return 1;

            var numberText = number.ToString();

            return numberText.Length;
        }
    }

}
