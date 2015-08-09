using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// The decimal number, 585 = 10010010012 (binary), is palindromic in both bases.
    ///
    /// Find the sum of all numbers, less than one million, which are palindromic in base 10 and base 2.
    ///
    /// (Please note that the palindromic number, in either base, may not include leading zeros.)
    [TestFixture]
    public class Problem_0036_DoubleBasePalindrome
    {
        [Test]
        [TestCase(585, true)]
        [TestCase(123, false)]
        public void ConfirmPalindrome(int candidate, bool expectedResult)
        {
            var result = IsDoubleBasePalindrome(candidate);
            Assert.AreEqual(expectedResult, result);
        }

        /// <summary>
        /// 872187
        /// </summary>
        [Test, Explicit]
        public void FindAllDoubleBasePalindromesBelowAMillion()
        {
            var list = new List<int>();

            for (var number = 1; number < 1000000; ++number)
            {
                var result = IsDoubleBasePalindrome(number);
                if (result)
                    list.Add(number);
            }

            var sumOfList = list.Sum();
            Console.WriteLine("Sum: {0}", sumOfList);

            foreach (var number in list)
            {
                Console.WriteLine("  {0}", number);
            }

            sumOfList.Should().Be(872187);
        }

        private static bool IsDoubleBasePalindrome(int decimalNumber)
        {
            var decimalIsPalindrome = PalindromeHelper.IsPalindrome(decimalNumber);
            if (!decimalIsPalindrome) return false;

            var binaryNumber = BinaryHelper.GetBinaryAsText(decimalNumber);
            return PalindromeHelper.IsPalindrome(binaryNumber);
        }


    }
}
