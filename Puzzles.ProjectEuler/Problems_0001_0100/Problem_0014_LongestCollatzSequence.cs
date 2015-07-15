using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// The following iterative sequence is defined for the set of positive integers:
    ///
    /// n → n/2 (n is even)
    /// n → 3n + 1 (n is odd)
    ///
    /// Using the rule above and starting with 13, we generate the following sequence:
    ///
    /// 13 → 40 → 20 → 10 → 5 → 16 → 8 → 4 → 2 → 1
    /// It can be seen that this sequence (starting at 13 and finishing at 1) contains 10 terms. Although it has not been proved yet (Collatz Problem), it is thought that all starting numbers finish at 1.
    ///
    /// Which starting number, under one million, produces the longest chain?
    ///
    /// NOTE: Once the chain starts the terms are allowed to go above one million.
    /// </summary>
    [TestFixture]
    public class Problem_0014_LongestCollatzSequence
    {
        [Test]
        public void StartingAt13GetsATenDigitSequence()
        {
            var list = GetCollatzStartingAt(13);

            Assert.AreEqual(10, list.Count, "Ten items");
            Assert.AreEqual(13, list[0], "13");
            Assert.AreEqual(40, list[1], "40");
            Assert.AreEqual(20, list[2], "20");
            Assert.AreEqual(10, list[3], "10");
            Assert.AreEqual(5, list[4], "5");
            Assert.AreEqual(16, list[5], "16");
            Assert.AreEqual(8, list[6], "8");
            Assert.AreEqual(4, list[7], "4");
            Assert.AreEqual(2, list[8], "2");
            Assert.AreEqual(1, list[9], "1");
        }

        [Test]
        public void ConfirmAChainStartingAt13GetsATenDigitChain()
        {
            var length = GetLengthOfCollatz(13);

            Assert.AreEqual(10, length);
        }

        [Test]
        [TestCase(6, 9, "6 3 10 5 16 8 4 2 1")]
        [TestCase(13, 10, "13, 40, 20, 10, 5, 16, 8, 4, 2, 1")]
        [TestCase(27, 112, "27 82 41...10 5 16 8 4 2 1")]
        [TestCase(871, 179, "Lots")]
        public void ConfirmKnownLengths(int startNumber, int chainLength, string description)
        {
            var calcLength = GetLengthOfCollatz(startNumber);
            Assert.AreEqual(chainLength, calcLength, description);
        }

        [Test]
        [TestCase(1000, 871, 179)]
        [TestCase(10000, 6171, 262)]
        public void FindTheLongestChainForAStartingNumberLimit(int startNumberLimit, int expectedStartNumber, int expectedChainLength)
        {
            var result = FindLongestCollatzChain(startNumberLimit);

            Assert.AreEqual(expectedStartNumber, result.Item1, "Start number");
            Assert.AreEqual(expectedChainLength, result.Item2, "Chain length");
        }

        [Test, Explicit]
        public void FindTheLongestChainForANumberUnderAMillion()
        {
            var result = FindLongestCollatzChain(1000000);

            Console.WriteLine("Start at {0} to get a chain {1} long", result.Item1, result.Item2);
        }

        private static Tuple<long, long> FindLongestCollatzChain(long startNumberLimit)
        {
            long numberForChain = 0;
            long chainLength = 0;

            for (long number = 1; number <= startNumberLimit; ++number)
            {
                var sequenceLength = GetLengthOfCollatz(number);
                //Console.WriteLine("{0}: Length: {1}", number, sequenceLength);
                if (sequenceLength > chainLength)
                {
                    numberForChain = number;
                    chainLength = sequenceLength;
                }
            }

            return new Tuple<long, long>(numberForChain, chainLength);
        }

        private static long GetLengthOfCollatz(long startNumber)
        {
            long chainLength = 1;

            var number = startNumber;
            while (number > 1)
            {
                number = GetNextCollatzNumber(number);
                chainLength++;
            }

            return chainLength;
        }

        private static List<long> GetCollatzStartingAt(long startNumber)
        {
            var list = new List<long> { startNumber };

            var number = startNumber;

            while (number != 1)
            {
                number = GetNextCollatzNumber(number);

                list.Add(number);
            }

            return list;
        }

        private static long GetNextCollatzNumber(long number)
        {
            if (number % 2 == 0)
            {
                number = number / 2;
            }
            else
            {
                number = (3 * number) + 1;
            }
            return number;
        }
    }
}
