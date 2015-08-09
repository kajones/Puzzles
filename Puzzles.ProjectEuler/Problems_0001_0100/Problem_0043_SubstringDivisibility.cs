using System;
using System.Collections.Generic;
using System.Globalization;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// The number, 1406357289, is a 0 to 9 pandigital number because it is made up of each of the digits 0 to 9 in some order, 
    /// but it also has a rather interesting sub-string divisibility property.
    ///
    /// Let d1 be the 1st digit, d2 be the 2nd digit, and so on. In this way, we note the following:
    ///
    /// d2d3d4=406 is divisible by 2
    /// d3d4d5=063 is divisible by 3
    /// d4d5d6=635 is divisible by 5
    /// d5d6d7=357 is divisible by 7
    /// d6d7d8=572 is divisible by 11
    /// d7d8d9=728 is divisible by 13
    /// d8d9d10=289 is divisible by 17
    /// 
    /// Find the sum of all 0 to 9 pandigital numbers with this property.
    /// </summary>
    [TestFixture]
    public class Problem_0043_SubstringDivisibility
    {
        private readonly List<int> divisors = new List<int> { 2, 3, 5, 7, 11, 13, 17 };

        [Test]
        public void ConfirmSubstringDivisibility()
        {
            var canDivide = CanDivideSubstrings(1406357289);
            Assert.IsTrue(canDivide);
        }

        /// <summary>
        /// 16695334890
        /// </summary>
        [Test, Explicit]
        public void FindTenDigitPandigitalsThatHaveDivisibleSubstrings()
        {
            var tenDigitPandigitals = PermutationHelper.GetLongPermutations(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            long total = 0;
            foreach (long tenDigitPandigital in tenDigitPandigitals)
            {
                if (DigitHelper.GetNumberLength(tenDigitPandigital) < 10) continue;

                var canDivide = CanDivideSubstrings(tenDigitPandigital);
                if (canDivide)
                {
                    total += tenDigitPandigital;
                }
            }

            Console.WriteLine("Total: {0}", total);

            total.Should().Be(16695334890);
        }

        private bool CanDivideSubstrings(long candidate)
        {
            var candidateText = candidate.ToString(CultureInfo.InvariantCulture);
            for (var idx = 1; idx < candidateText.Length - 2; idx++)
            {
                var subText = candidateText.Substring(idx, 3);
                var subNumber = Convert.ToInt32(subText);
                //if (primes.Contains(subNumber)) return false;

                if (subNumber % divisors[idx - 1] != 0) return false;
            }

            return true;
        }
    }
}
