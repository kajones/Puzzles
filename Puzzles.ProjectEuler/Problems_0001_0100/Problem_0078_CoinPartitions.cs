using System;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// Let p(n) represent the number of different ways in which n coins can be separated into piles. 
    /// For example, five coins can separated into piles in exactly seven different ways, so p(5)=7.
    ///
    /// OOOOO
    /// OOOO   O
    /// OOO   OO
    /// OOO   O   O
    /// OO   OO   O
    /// OO   O   O   O
    /// O   O   O   O   O
    /// Find the least value of n for which p(n) is divisible by one million.
    /// </summary>
    [TestFixture]
    public class Problem_0078_CoinPartitions
    {
        [Test]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        [TestCase(4, 5)]
        [TestCase(5, 7)]
        public void ConfirmExample(long targetTotal, long expectedWays)
        {
            var ways = CalculateWays(targetTotal);

            Assert.AreEqual(expectedWays, ways);
        }

        [Test]
        public void FindBallpark()
        {
            // 10: 42
            // 20: 627
            // 50: 204226
            // 60: 966467
            // 70: 4087968
            // 100: 190569292
            var ways = CalculateWays(70);
            Console.WriteLine(ways);
        }

        [Test, Explicit]
        public void FindLeastTargetWhereWaysIsDivisibleByOneMillion()
        {
            Assert.Fail("To implement - find the solution");
            const long AMillion = 1000000;

            var foundWays = false;

            var lowerLimit = 2800;
            
            for (long number = lowerLimit; number <= (lowerLimit + 100); ++number)
            {
                var ways = CalculateWays(number);
                if (ways % AMillion == 0)
                {
                    foundWays = true;
                    Console.WriteLine("Number: {0}  Ways: {1}", number, ways);
                    break;
                }

                //  number++;
            }

            Assert.IsTrue(foundWays);
        }

        private static long CalculateWays(long targetTotal)
        {
            var summationHelper = new SummationHelper(targetTotal);
            var ways = summationHelper.WaysToSum(targetTotal);

            return (ways + 1); // Include just single pile
        }
    }
}
