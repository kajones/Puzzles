using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// The proper divisors of a number are all the divisors excluding the number itself. 
    /// For example, the proper divisors of 28 are 1, 2, 4, 7, and 14. 
    /// As the sum of these divisors is equal to 28, we call it a perfect number.
    ///
    /// Interestingly the sum of the proper divisors of 220 is 284 and the sum of the proper divisors of 284 is 220, forming a chain of two numbers. 
    /// For this reason, 220 and 284 are called an amicable pair.
    ///
    /// Perhaps less well known are longer chains. For example, starting with 12496, we form a chain of five numbers:
    ///
    /// 12496 → 14288 → 15472 → 14536 → 14264 (→ 12496 → ...)
    ///
    /// Since this chain returns to its starting point, it is called an amicable chain.
    ///
    /// Find the smallest member of the longest amicable chain with no element exceeding one million.
    /// </summary>
    [TestFixture]
    public class Problem_0095_AmicableChains
    {
        private const long Threshold = 1000000;

        [Test]
        [TestCase(220, 2)]
        [TestCase(284,2)]
        [TestCase(12496, 5)]
        [TestCase(14316, 28)]
        public void ConfirmExamples(long startingNumber, int expectedChainLength)
        {
            var actualLength = GetChainLength(startingNumber);
            actualLength.Should().Be(expectedChainLength);
        }

        [Test]
        public void FindLongestChainBelowAMillion()
        {
            int longestChain = 0;
            long longestChainStart = 0;

            for (long startingNumber = 1; startingNumber < Threshold; ++startingNumber)
            {
                var chainLength = GetChainLength(startingNumber);
                if (chainLength <= longestChain) continue;
                longestChain = chainLength;
                longestChainStart = startingNumber;
            }

            Console.WriteLine("Start of {0} has a chain {1}", longestChainStart, longestChain);
        }

        [Test]
        public void FindSmallestMemberOfChain()
        {
            var chainLength = GetChainLength(402170);
            chainLength.Should().Be(65);
            var chain = new HashSet<long>();
            long startingNumber = 402170;

            long currentNumber = startingNumber;
            while (!chain.Contains(currentNumber))
            {
                chain.Add(currentNumber);
                var factors = FactorHelper.GetProperFactorsOf(currentNumber);
                var sum = factors.Sum();
                currentNumber = sum;
            }

            var smallest = chain.Min();
            Console.WriteLine("Smallest member of longest chain: {0}", smallest);
        }

        private static int GetChainLength(long startingNumber)
        {
            var chain = new HashSet<long>();
            long currentNumber = startingNumber;
            while (!chain.Contains(currentNumber))
            {
                chain.Add(currentNumber);
                var factors = FactorHelper.GetProperFactorsOf(currentNumber);
                var sum = factors.Sum();
                if (sum > Threshold) return 0;
                currentNumber = sum;
                if (currentNumber == 1) return 0;
                //if (chain.Count > 200) return 0;
            }

            //long smallestNumber = long.MaxValue;
            //foreach (var l in chain)
            //{
            //    Console.WriteLine("{0}{1}", l, Environment.NewLine);
            //    if (l < smallestNumber)
            //    {
            //        smallestNumber = l;
            //    }
            //}
            //Console.WriteLine("Smallest: {0}", smallestNumber);
            return chain.Count;
        }
    }
}
