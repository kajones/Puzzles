using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// Let d(n) be defined as the sum of proper divisors of n (numbers less than n which divide evenly into n).
    /// If d(a) = b and d(b) = a, where a ≠ b, then a and b are an amicable pair and each of a and b are called amicable numbers.
    ///
    /// For example, the proper divisors of 220 are 1, 2, 4, 5, 10, 11, 20, 22, 44, 55 and 110; 
    /// therefore d(220) = 284. The proper divisors of 284 are 1, 2, 4, 71 and 142; so d(284) = 220.
    ///
    /// Evaluate the sum of all the amicable numbers under 10000.
    /// </summary>
    [TestFixture]
    public class Problem_0021_AmicableNumbers
    {
        [Test]
        public void Confirm220And284Pair()
        {
            var pair220 = GetAmicablePair(220);
            Assert.AreEqual(284, pair220, "284 is pair of 220");

            var pair284 = GetAmicablePair(284);
            Assert.AreEqual(220, pair284, "220 is pair of 284");
        }

        /// <summary>
        ///  31626
        /// </summary>
        [Test, Explicit]
        public void FindAmicableNumbersUnderTenThousand()
        {
            const long limit = 10000;

            var list = new List<long>();

            for (long l = 1; l < limit; ++l)
            {
                if (list.Contains(l)) continue;

                var amicable = GetAmicablePair(l);
                if (amicable != 0 && amicable < limit)
                {
                    list.Add(l);

                    if (!list.Contains(amicable)) list.Add(amicable);
                }
            }

            foreach (var l in list)
            {
                Console.WriteLine(l);
            }

            var result = list.Sum();
            Console.WriteLine("Sum of amicable numbers is: {0}", result);

            result.Should().Be(31626);
        }

        private long GetAmicablePair(long number)
        {
            var factors = FactorHelper.GetProperFactorsOf(number);
            var sumOfFactors = factors.Sum();

            if (sumOfFactors == number) return 0;

            var possAmicable = FactorHelper.GetProperFactorsOf(sumOfFactors);
            var sumOfAmicableFactors = possAmicable.Sum();

            if (sumOfAmicableFactors == number) return sumOfFactors;

            return 0;
        }

    }
}
