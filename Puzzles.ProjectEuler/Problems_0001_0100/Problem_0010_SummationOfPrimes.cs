using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// The sum of the primes below 10 is 2 + 3 + 5 + 7 = 17.
    ///
    /// Find the sum of all the primes below two million.
    /// </summary>
    [TestFixture]
    public class Problem_0010_SummationOfPrimes
    {
        [Test]
        public void CalculateSumOfPrimesBelow10()
        {
            var primes = PrimeHelper.GetPrimesUpTo(10);

            Assert.AreEqual(17, primes.Sum());
        }

        /// <summary>
        /// 142913828922
        /// </summary>
        [Test, Explicit]
        public void CalculateSumOfPrimesBelowTwoMillion()
        {
            var primes = PrimeHelper.GetPrimesUpTo(2000000);

            long total = primes.Aggregate<int, long>(0, (current, prime) => current + prime);

            Console.WriteLine(total);

            total.Should().Be(142913828922);
        }

    }
}
