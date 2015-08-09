using System;
using FluentAssertions;
using NUnit.Framework;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// In England the currency is made up of pound, £, and pence, p, and there are eight coins in general circulation:
    ///
    /// 1p, 2p, 5p, 10p, 20p, 50p, £1 (100p) and £2 (200p).
    /// It is possible to make £2 in the following way:
    ///
    /// 1×£1 + 1×50p + 2×20p + 1×5p + 1×2p + 3×1p
    /// How many different ways can £2 be made using any number of coins?
    /// </summary>
    [TestFixture]
    public class Problem_0031_CoinSums
    {
        private static readonly int[] Coins = { 1, 2, 5, 10, 20, 50, 100, 200 };

        /// <summary>
        /// 73682
        /// </summary>
        [Test, Explicit]
        public void FindCombinationsOfCoinsToValue()
        {
            var count = GetPay(200);

            Console.WriteLine("Number of ways: {0}", count);

            count.Should().Be(73682);
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(5, 4)]
        public void ConfirmCombinationCounts(int requiredAmount, int expectedCount)
        {
            var count = GetPay(requiredAmount);
            Assert.AreEqual(expectedCount, count, "Combinations for {0}", requiredAmount);
        }

        private static int GetPay(int amount)
        {
            return GetPay(amount, Coins.Length);
        }

        private static int GetPay(int amount, int coinIndex)
        {
            if (coinIndex == 1) return 1;
            if (amount == 0) return 1;
            if (amount < 0) return 0;

            return GetPay(amount, coinIndex - 1) + GetPay(amount - Coins[coinIndex - 1], coinIndex);
        }
    }
}
