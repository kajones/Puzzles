using System;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// It is possible to write five as a sum in exactly six different ways:
    ///
    /// 4 + 1
    /// 3 + 2
    /// 3 + 1 + 1
    /// 2 + 2 + 1
    /// 2 + 1 + 1 + 1
    /// 1 + 1 + 1 + 1 + 1
    ///
    /// How many different ways can one hundred be written as a sum of at least two positive integers?
    /// </summary>
    [TestFixture]
    public class Problem_0076_CountingSummations
    {
        [Test]
        [TestCase(2, 1)]
        [TestCase(3, 2)]
        [TestCase(4, 4)]
        [TestCase(5, 6)]
        public void ConfirmExample(int targetTotal, int expectedWays)
        {
            var summationHelper = new SummationHelper(targetTotal);
            var waysToSum = summationHelper.WaysToSum(targetTotal);
            Assert.AreEqual(expectedWays, waysToSum);
        }

        /// <summary>
        /// 190569291
        /// </summary>
        [Test, Explicit]
        public void FindWaysToSumToAHundred()
        {
            var summationHelper = new SummationHelper(100);
            var waysToSum = summationHelper.WaysToSum(100);
            Console.WriteLine(waysToSum);

            waysToSum.Should().Be(190569291);
        }
    }
}
