using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// The prime 41, can be written as the sum of six consecutive primes:
    ///
    /// 41 = 2 + 3 + 5 + 7 + 11 + 13
    /// This is the longest sum of consecutive primes that adds to a prime below one-hundred.
    ///
    /// The longest sum of consecutive primes below one-thousand that adds to a prime, contains 21 terms, and is equal to 953.
    ///
    /// Which prime, below one-million, can be written as the sum of the most consecutive primes?
    [TestFixture]
    public class Problem_0050_ConsecutivePrimeSum
    {
        private readonly List<int> primes = PrimeHelper.GetPrimesUpTo(1000000);

        [Test]
        [TestCase(100, 41, 6)]
        [TestCase(1000, 953, 21)]
        public void ConfirmLongestSequenceToLimit(int limit, int sum, int terms)
        {
            int termCount;

            var foundPrime = FindPrime(limit, out termCount);
            Assert.AreEqual(sum, foundPrime, "Found prime sum");
            Assert.AreEqual(terms, termCount, "Term count");
        }

        /// <summary>
        /// 997651 
        /// </summary>
        [Test, Explicit]
        public void FindLongestSequenceBelowAMillion()
        {
            int termCount;

            var foundPrime = FindPrime(1000000, out termCount);
            Console.WriteLine("{0} with {1} terms", foundPrime, termCount);

            foundPrime.Should().Be(997651);
        }

        private int FindPrime(int limit, out int termCount)
        {
            int longestPrimeSum = 0;
            termCount = 0;

            for (var idx = 0; idx < primes.Count; ++idx)
            {
                var primeSum = primes[idx];
                var offset = 0;
                while (primeSum < limit)
                {
                    offset++;
                    if (idx + offset > primes.Count - 1) break;

                    primeSum += primes[idx + offset];
                    if (primeSum > limit) break;

                    if (offset > termCount)
                    {
                        if (primes.Contains(primeSum))
                        {
                            longestPrimeSum = primeSum;
                            termCount = offset + 1;
                        }
                    }
                }
            }

            return longestPrimeSum;
        }
    }
}
