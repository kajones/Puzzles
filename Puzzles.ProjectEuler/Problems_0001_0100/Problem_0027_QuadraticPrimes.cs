using System;
using System.Collections.Generic;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// Euler discovered the remarkable quadratic formula:
    ///
    /// n² + n + 41
    ///
    /// It turns out that the formula will produce 40 primes for the consecutive values n = 0 to 39. 
    /// However, when n = 40, 402 + 40 + 41 = 40(40 + 1) + 41 is divisible by 41, 
    /// and certainly when n = 41, 41² + 41 + 41 is clearly divisible by 41.
    ///
    /// The incredible formula  n² − 79n + 1601 was discovered, which produces 80 primes for the consecutive values n = 0 to 79. 
    /// The product of the coefficients, −79 and 1601, is −126479.
    ///
    /// Considering quadratics of the form:
    ///
    /// n² + an + b, where |a| less than 1000 and |b| less than 1000
    ///
    /// where |n| is the modulus/absolute value of n
    /// e.g. |11| = 11 and |−4| = 4
    /// Find the product of the coefficients, a and b, for the quadratic expression that produces 
    /// the maximum number of primes for consecutive values of n, starting with n = 0.
    /// </summary>
    [TestFixture]
    public class Problem_0027_QuadraticPrimes
    {
        private List<int> primes;

        [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
            primes = PrimeHelper.GetPrimesUpTo(87400);
        }

        [Test]
        [TestCase(1, 41, 40)]
        [TestCase(-79, 1601, 80)]
        [TestCase(1, 17, 16)]
        public void ConfirmQuadraticPrimes(int a, int b, int numberOfPrimesGenerated)
        {
            var numPrimes = CountOfPrimes(a, b);
            Assert.AreEqual(numberOfPrimesGenerated, numPrimes, "{0} {1}", a, b);
        }

        /// <summary>
        /// a:-61 b:971  primes: 71; product of a and b: -59231
        /// </summary>
        [Test, Explicit]
        public void FindMaximumPrimes()
        {
            var bPrimes = PrimeHelper.GetPrimesUpTo(1000);

            int bestA = 0;
            int bestB = 0;
            int mostPrimes = 0;

            // Ignoring the b=2 option (a would have to be even then)
            for (var a = -999; a <= 1000; a += 2)
            {
                foreach (var b in bPrimes)
                {
                    // Count starts with n=0 so quadratic equation means b must be a prime
                    var countOfPrimes = CountOfPrimes(a, b);

                    if (countOfPrimes >= mostPrimes)
                    {
                        bestA = a;
                        bestB = b;
                        mostPrimes = countOfPrimes;
                    }
                }
            }

            Console.WriteLine("a:{0} b:{1}  primes: {2}; product of a and b: {3}", bestA, bestB, mostPrimes, (bestA * bestB));
        }

        private int CountOfPrimes(int a, int b)
        {
            var isPrime = true;
            var n = 0;
            while (isPrime)
            {
                int candidate = (n * n) + (a * n) + b;
                isPrime = primes.Contains(candidate);
                if (!isPrime) break;

                n++;
            }

            return n;
        }

    }
}
