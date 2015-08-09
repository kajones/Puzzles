using System;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// Euler's Totient function, φ(n) [sometimes called the phi function], 
    /// is used to determine the number of positive numbers less than or equal to n which are relatively prime to n. 
    /// For example, as 1, 2, 4, 5, 7, and 8, are all less than nine and relatively prime to nine, φ(9)=6.
    /// 
    /// The number 1 is considered to be relatively prime to every positive number, so φ(1)=1.
    ///
    /// Interestingly, φ(87109)=79180, and it can be seen that 87109 is a permutation of 79180.
    ///
    /// Find the value of n, 1 less than n less than 10^7, for which φ(n) is a permutation of n and the ratio n/φ(n) produces a minimum.
    /// </summary>
    [TestFixture]
    public class Problem_0070_TotientPermutation
    {
        [Test]
        public void ConfirmExample()
        {
            var phi = PhiAsPermutation(87109);
            Assert.AreEqual(79180, phi);
        }

        /// <summary>
        /// 8319823 
        /// </summary>
        [Test, Explicit]
        public void FindMinimumRatioForPermutation()
        {
            var limit = Math.Pow(10, 7);

            var numberForMinRatio = 0;
            var minRatio = limit;

            for (var number = 2; number <= limit; ++number)
            {
                var phi = PhiAsPermutation(number);
                if (phi == 0) continue;

                double ratio = (double)(1.0 * number) / (1.0 * phi);
                if (ratio < minRatio)
                {
                    minRatio = ratio;
                    numberForMinRatio = number;
                }
            }

            Console.WriteLine("Number: {0} ratio:{1}", numberForMinRatio, minRatio);

            numberForMinRatio.Should().Be(8319823);
        }

        private static long PhiAsPermutation(long number)
        {
            var phi = EulerTotientHelper.CalculatePhi(number);

            var arePermutations = PermutationHelper.ArePermutations(number, phi);
            return (arePermutations ? phi : 0);
        }
    }
}
