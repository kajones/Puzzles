using System.Collections.Generic;
using System.Linq;

namespace Puzzles.Core.Helpers
{
    public static class PandigitalHelper
    {
        public static bool IsPandigital(int candidate)
        {
            var digits = DigitHelper.GetDigits(candidate);
            return IsPandigital(digits);
        }

        public static bool IsPandigital(IEnumerable<int> digits)
        {
            return IsPandigital(digits.ToList());
        }

        public static bool IsPandigital(List<int> digits)
        {
            if (digits.Count != 9) return false;

            if (!digits.Contains(1)) return false;
            if (!digits.Contains(2)) return false;
            if (!digits.Contains(3)) return false;
            if (!digits.Contains(4)) return false;
            if (!digits.Contains(5)) return false;
            if (!digits.Contains(6)) return false;
            if (!digits.Contains(7)) return false;
            if (!digits.Contains(8)) return false;
            if (!digits.Contains(9)) return false;

            return true;
        }

        public static bool IsPandigitalToNDigits(int candidate)
        {
            var digits = DigitHelper.GetDigits(candidate).ToList();

            for (var n = 1; n <= digits.Count; ++n)
            {
                if (!digits.Contains(n)) return false;
            }

            return true;
        }
    }
}