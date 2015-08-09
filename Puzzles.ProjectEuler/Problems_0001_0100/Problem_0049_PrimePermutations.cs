using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// The arithmetic sequence, 1487, 4817, 8147, in which each of the terms increases by 3330, is unusual in two ways: 
    /// (i) each of the three terms are prime, and, 
    /// (ii) each of the 4-digit numbers are permutations of one another.
    ///
    /// There are no arithmetic sequences made up of three 1-, 2-, or 3-digit primes, exhibiting this property, 
    /// but there is one other 4-digit increasing sequence.
    ///
    /// What 12-digit number do you form by concatenating the three terms in this sequence?
    [TestFixture]
    public class Problem_0049_PrimePermutations
    {
        [Test]
        public void ConfirmExample()
        {
            var result = PassesTest(1487);
            Assert.IsTrue(result);
        }

        [Test, Explicit]
        public void CheckAnswer()
        {
            PassesTest(2969);
        }

        /// <summary>
        /// 2969 6299 9629
        /// </summary>
        [Test, Explicit]
        public void FindAlternateSet()
        {
            for (var number = 1000; number < 3000; ++number)
            {
                if (number == 1487) continue;

                var result = PassesTest(number);
                if (result)
                {
                    Console.WriteLine(number);
                    break;
                }
            }
        }

        private static bool PassesTest(int original)
        {
            var second = original + 3330;
            var third = second + 3330;
            if (second > 9999 || third > 9999) return false;

            var originalDigits = DigitHelper.GetDigits(original).ToList();
            var match = SameDigits(originalDigits, second);
            if (!match) return false;
            match = SameDigits(originalDigits, third);
            if (!match) return false;

            if (!PrimeHelper.IsPrime(original)) return false;
            if (!PrimeHelper.IsPrime(second)) return false;
            if (!PrimeHelper.IsPrime(third)) return false;

            return true;
        }

        private static bool SameDigits(ICollection<int> originalDigits, int toCompare)
        {
            var otherDigits = DigitHelper.GetDigits(toCompare);

            return otherDigits.All(originalDigits.Contains);
        }
    }
}
