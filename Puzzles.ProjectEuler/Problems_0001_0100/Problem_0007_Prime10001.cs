using System;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, we can see that the 6th prime is 13.
    ///
    /// What is the 10 001st prime number?
    /// </summary>
    [TestFixture]
    public class Problem_0007_Prime10001
    {
        [Test]
        public void FindSixthPrime()
        {
            var prime = PrimeAtPosition(6);

            Assert.AreEqual(13, prime);
        }

        /// <summary>
        /// 104743
        /// </summary>
        [Test, Explicit]
        public void FindSpecifiedPrime()
        {
            var prime = PrimeAtPosition(10001);

            Console.WriteLine(prime);

            prime.Should().Be(104743);
        }

        [Test]
        [TestCase(1, 2)]
        [TestCase(2, 3)]
        [TestCase(3, 5)]
        [TestCase(4, 7)]
        [TestCase(5, 11)]
        [TestCase(6, 13)]
        [TestCase(7, 17)]
        [TestCase(8, 19)]
        [TestCase(9, 23)]
        [TestCase(10, 29)]
        public void FindPrimeAt(int position, long expectedPrime)
        {
            var prime = PrimeAtPosition(position);

            Assert.AreEqual(expectedPrime, prime);

        }

        private static long PrimeAtPosition(int requiredPosition)
        {
            if (requiredPosition == 1) return 2;
            if (requiredPosition == 2) return 3;

            long position = 2;
            var candidatePrime = 5;

            while (position < requiredPosition)
            {
                var isPrime = PrimeHelper.IsPrime(candidatePrime);

                if (isPrime)
                    position++;

                if (position >= requiredPosition) break;

                candidatePrime += 2;
            }

            return candidatePrime;
        }
    }
}
