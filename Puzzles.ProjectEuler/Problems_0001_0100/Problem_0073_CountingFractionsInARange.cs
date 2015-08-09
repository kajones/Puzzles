using System;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// Consider the fraction, n/d, where n and d are positive integers. 
    /// If n less than d and HCF(n,d)=1, it is called a reduced proper fraction.
    ///
    /// If we list the set of reduced proper fractions for d ≤ 8 in ascending order of size, we get:
    ///
    /// 1/8, 1/7, 1/6, 1/5, 1/4, 2/7, 1/3, 3/8, 2/5, 3/7, 1/2, 4/7, 3/5, 5/8, 2/3, 5/7, 3/4, 4/5, 5/6, 6/7, 7/8
    ///
    /// It can be seen that there are 3 fractions between 1/3 and 1/2.
    ///
    /// How many fractions lie between 1/3 and 1/2 in the sorted set of reduced proper fractions for d ≤ 12,000?
    /// </summary>
    [TestFixture]
    public class Problem_0073_CountingFractionsInARange
    {
        [Test]
        public void ConfirmExample()
        {
            var count = 0;
            double lowerThreshold = 1.0 / 3.0;
            double upperThreshold = 0.5;
            for (var denominator = 2; denominator <= 8; ++denominator)
            {
                var calcDenom = (double)denominator;
                var low = (long)Math.Floor(calcDenom / 3);
                var high = (long)Math.Ceiling(calcDenom / 2);

                for (var numerator = low; numerator < high; ++numerator)
                {
                    var fraction = (1.0 * numerator) / (1.0 * denominator);
                    if ((fraction <= lowerThreshold) || (fraction >= upperThreshold)) continue;

                    var areCoPrime = FactorHelper.AreCoprime(numerator, denominator);
                    if (!areCoPrime) continue;

                    Console.WriteLine("{0}/{1}", numerator, denominator);
                    count++;
                }
            }

            Assert.AreEqual(3, count);
        }

        /// <summary>
        /// 7295372
        /// </summary>
        [Test, Explicit]
        public void FindFractionsInRangeUpToTwelveThousand()
        {
            var count = 0;
            double lowerThreshold = 1.0 / 3.0;
            double upperThreshold = 0.5;
            for (var denominator = 2; denominator <= 12000; ++denominator)
            {
                var calcDenom = (double)denominator;
                var low = (long)Math.Floor(calcDenom / 3);
                var high = (long)Math.Ceiling(calcDenom / 2);

                for (var numerator = low; numerator < high; ++numerator)
                {
                    var fraction = (1.0 * numerator) / (1.0 * denominator);
                    if ((fraction <= lowerThreshold) || (fraction >= upperThreshold)) continue;

                    var areCoPrime = FactorHelper.AreCoprime(numerator, denominator);
                    if (!areCoPrime) continue;

                    //Console.WriteLine("{0}/{1}", numerator, denominator);
                    count++;
                }
            }

            Console.WriteLine("Number: {0}", count);
            count.Should().Be(7295372);
        }
    }
}
