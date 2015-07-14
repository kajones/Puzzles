using System;
using System.Globalization;
using NUnit.Framework;
using Puzzles.Core;

namespace Puzzles.ProjectEuler.Problems_0001_0100.Problems_0001_0010
{
    /// <summary>
    /// A palindromic number reads the same both ways. 
    /// The largest palindrome made from the product of two 2-digit numbers is 9009 = 91 × 99.
    ///
    /// Find the largest palindrome made from the product of two 3-digit numbers.
    /// </summary>
    [TestFixture]
    public class Problem_0004_LargestPalindromeProduct
    {
        [Test, Explicit]
        public void FindTheLargestPalindromeFromTwoDigitNumbers()
        {
            FindLargestPalindrome(9, 99);
        }

        /// <summary>
        /// Largest palindrome: 906609 from 913 and 993
        /// </summary>
        [Test, Explicit]
        public void FindTheLargestPalindromeFromThreeDigitNumbers()
        {
            FindLargestPalindrome(99, 999);
        }

        [Test, Explicit]
        [TestCase(1, true)]
        [TestCase(11, true)]
        [TestCase(12, false)]
        [TestCase(121, true)]
        [TestCase(221, false)]
        [TestCase(1221, true)]
        [TestCase(12321, true)]
        public void ConfirmPalindrome(int candidatePalindrome, bool isPalindrome)
        {
            Assert.AreEqual(isPalindrome, PalindromeHelper.IsPalindrome(candidatePalindrome), candidatePalindrome.ToString(CultureInfo.InvariantCulture));
        }

        private static void FindLargestPalindrome(int lowerLimit, int upperLimit)
        {
            var factor1 = 0;
            var factor2 = 0;
            var product = 0;

            for (var number1 = upperLimit; number1 > lowerLimit; --number1)
            {
                for (var number2 = upperLimit; number2 > lowerLimit; --number2)
                {
                    var candidateProduct = number1 * number2;
                    if (candidateProduct < product) continue;

                    var isPalindrome = PalindromeHelper.IsPalindrome(candidateProduct);
                    if (!isPalindrome) continue;

                    factor1 = number1;
                    factor2 = number2;
                    product = candidateProduct;
                    break;
                }
            }

            Console.WriteLine("Largest palindrome: {0} from {1} and {2}", product, factor1, factor2);
        }
    }
}
