using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// The number 145 is well known for the property that the sum of the factorial of its digits is equal to 145:
    ///
    /// 1! + 4! + 5! = 1 + 24 + 120 = 145
    ///
    /// Perhaps less well known is 169, in that it produces the longest chain of numbers that link back to 169; 
    /// it turns out that there are only three such loops that exist:
    ///
    /// 169 → 363601 → 1454 → 169
    /// 871 → 45361 → 871
    /// 872 → 45362 → 872
    ///
    /// It is not difficult to prove that EVERY starting number will eventually get stuck in a loop. For example,
    ///
    /// 69 → 363600 → 1454 → 169 → 363601 (→ 1454)
    /// 78 → 45360 → 871 → 45361 (→ 871)
    /// 540 → 145 (→ 145)
    ///
    /// Starting with 69 produces a chain of five non-repeating terms, 
    /// but the longest non-repeating chain with a starting number below one million is sixty terms.
    ///
    /// How many chains, with a starting number below one million, contain exactly sixty non-repeating terms?
    /// </summary>
    [TestFixture]
    public class Problem_0074_DigitFactorialChain
    {
        private readonly Dictionary<int, long> factorials = new Dictionary<int, long>();

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            for (var i = 0; i < 10; ++i)
            {
                var fact = MathHelper.GetFactorial(i);
                factorials.Add(i, fact);
            }
        }

        [Test]
        [TestCase(69, 5)]
        [TestCase(78, 4)]
        [TestCase(169, 3)]
        [TestCase(540, 2)]
        [TestCase(871, 2)]
        [TestCase(872, 2)]
        [TestCase(145, 1)]
        public void ConfirmExamples(long startingNumber, int expectedChainLength)
        {
            var chainLength = GetChainLength(startingNumber);
            Assert.AreEqual(expectedChainLength, chainLength);
        }

        /// <summary>
        /// 402
        /// </summary>
        [Test, Explicit]
        public void FindCountSixtyChainsBelowAMillion()
        {
            var count = 0;

            for (var number = 1; number < 1000000; ++number)
            {
                var chainLength = GetChainLength(number);
                if (chainLength == 60)
                    count++;
            }

            Console.WriteLine("Sixty chain: {0}", count);
            count.Should().Be(402);
        }

        private int GetChainLength(long startingNumber)
        {
            var chainLength = 1;
            var links = new List<long> { startingNumber };

            var nextLink = GetFactorialResult(startingNumber);
            if (nextLink == startingNumber) return 1;

            do
            {
                links.Add(nextLink);
                chainLength++;
                nextLink = GetFactorialResult(nextLink);
            } while (!links.Contains(nextLink));

            return chainLength;
        }

        private long GetFactorialResult(long startingNumber)
        {
            var digits = DigitHelper.GetDigits(startingNumber);

            long value = 0;
            foreach (var digit in digits)
            {
                var fact = factorials[digit];
                value += fact;
            }

            return value;
        }
    }
}
