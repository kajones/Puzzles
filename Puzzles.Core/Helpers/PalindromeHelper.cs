using System.Globalization;
using System.Numerics;

namespace Puzzles.Core.Helpers
{
    public static class PalindromeHelper
    {
        public static bool IsPalindrome(long candidatePalindrome)
        {
            var candidateText = candidatePalindrome.ToString(CultureInfo.InvariantCulture);

            return IsPalindrome(candidateText);
        }

        public static bool IsPalindrome(int candidatePalindrome)
        {
            var candidateText = candidatePalindrome.ToString(CultureInfo.InvariantCulture);

            return IsPalindrome(candidateText);
        }

        public static bool IsPalindrome(BigInteger candidatePalindrome)
        {
            var candidateText = candidatePalindrome.ToString();

            return IsPalindrome(candidateText);
        }

        public static bool IsPalindrome(string candidateText)
        {
            var length = candidateText.Length;

            for (var idx = 0; idx < length/2; ++idx)
            {
                var leftChar = candidateText[idx];
                var rightChar = candidateText[length - 1 - idx];

                if (leftChar != rightChar)
                    return false;
            }

            return true;
        }
    }
}
