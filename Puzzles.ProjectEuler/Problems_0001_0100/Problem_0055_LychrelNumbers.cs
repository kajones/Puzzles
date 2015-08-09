using System;
using System.Linq;
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// If we take 47, reverse and add, 47 + 74 = 121, which is palindromic.
    ///
    /// Not all numbers produce palindromes so quickly. For example,
    ///
    /// 349 + 943 = 1292,
    /// 1292 + 2921 = 4213
    /// 4213 + 3124 = 7337
    ///
    /// That is, 349 took three iterations to arrive at a palindrome.
    ///
    /// Although no one has proved it yet, it is thought that some numbers, like 196, never produce a palindrome. 
    /// A number that never forms a palindrome through the reverse and add process is called a Lychrel number. 
    /// Due to the theoretical nature of these numbers, and for the purpose of this problem, 
    /// we shall assume that a number is Lychrel until proven otherwise. In addition you are given that for every number below ten-thousand, 
    /// it will either (i) become a palindrome in less than fifty iterations, or, (ii) no one, with all the computing power that exists, 
    /// has managed so far to map it to a palindrome. In fact, 10677 is the first number to be shown to require over fifty iterations 
    /// before producing a palindrome: 4668731596684224866951378664 (53 iterations, 28-digits).
    ///
    /// Surprisingly, there are palindromic numbers that are themselves Lychrel numbers; the first example is 4994.
    ///
    /// How many Lychrel numbers are there below ten-thousand?
    ///
    /// NOTE: Wording was modified slightly on 24 April 2007 to emphasise the theoretical nature of Lychrel numbers.
    [TestFixture]
    public class Problem_0055_LychrelNumbers
    {
        [Test]
        [TestCase(47, false)]
        [TestCase(349, false)]
        [TestCase(4994, true)]
        public void ConfirmIfLychrel(long candidate, bool expectedResult)
        {
            var result = IsLychrel(candidate);
            Assert.AreEqual(expectedResult, result);
        }

        /// <summary>
        /// 249
        /// </summary>
        [Test, Explicit]
        public void FindCountOfLychrelBelowTenThousand()
        {
            var count = 0;

            for (long candidate = 1; candidate < 10000; ++candidate)
            {
                var result = IsLychrel(candidate);
                if (result)
                    count++;
            }

            Console.WriteLine("Number of Lychrel: {0}", count);

            count.Should().Be(249);
        }

        private bool IsLychrel(long candidate)
        {
            BigInteger number = candidate;
            for (var i = 0; i < 50; ++i)
            {
                var reverse = GetReverse(number);
                var sum = number + reverse;
                if (PalindromeHelper.IsPalindrome(sum))
                    return false;
                number = sum;
            }

            return true;
        }

        private BigInteger GetReverse(BigInteger number)
        {
            var digits = DigitHelper.GetDigits(number).ToList();
            digits.Reverse();

            BigInteger result;
            var canParse = BigInteger.TryParse(string.Concat(digits), out result);

            return (canParse) ? result : -1;
        }

    }
}
