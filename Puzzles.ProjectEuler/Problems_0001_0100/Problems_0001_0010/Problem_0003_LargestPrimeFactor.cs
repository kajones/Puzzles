using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core;

namespace Puzzles.ProjectEuler.Problems_0001_0100.Problems_0001_0010
{
    /// <summary>
    /// The prime factors of 13195 are 5, 7, 13 and 29.
    ///
    /// What is the largest prime factor of the number 600851475143 ?
    /// </summary>
    [TestFixture]
    public class Problem_0003_LargestPrimeFactor
    {
        [Test, Explicit]
        public void FindThePrimeFactorsOf20()
        {
            var primeFactors = PrimeHelper.FindPrimeFactors(20);

            foreach (var primeFactor in primeFactors)
            {
                Console.WriteLine("{0}, ", primeFactor);
            }
        }

        [Test]
        public void FindThePrimeFactorsOf13195()
        {
            var primeFactors = PrimeHelper.FindPrimeFactors(13195);

            Assert.AreEqual(4, primeFactors.Count, "Four prime factors");

            Assert.IsTrue(primeFactors.Contains(5), "5");
            Assert.IsTrue(primeFactors.Contains(7), "7");
            Assert.IsTrue(primeFactors.Contains(13), "13");
            Assert.IsTrue(primeFactors.Contains(29), "29");
        }

        /// <summary>
        /// 6857
        /// </summary>
        [Test, Explicit]
        public void FindTheLargestPrimeFactorOfSpecifiedNumber()
        {
            var primeFactors = PrimeHelper.FindPrimeFactors(600851475143);

            var largestPrimeFactor = primeFactors.Max();

            Console.WriteLine(largestPrimeFactor);

            largestPrimeFactor.Should().Be(6857);
        }
    }
}
