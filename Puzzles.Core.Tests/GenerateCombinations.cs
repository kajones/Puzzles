using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.Core.Tests
{
    [TestFixture]
    public class GenerateCombinations
    {
        [Test]
        public void ConfirmSingleDigitCombinationsUsingZero()
        {
            var combinations = CombinationHelper.GetCombinations(0);
            Assert.AreEqual(1, combinations.Count, "One combination set");
            Assert.AreEqual(1, combinations[0].Count, "One entry in single combination");
            Assert.AreEqual(0, combinations[0][0], "Zero is only entry");
        }

        [Test]
        public void ConfirmTwoDigitCombinationsUsingZero()
        {
            var combinations = CombinationHelper.GetCombinations(10);
            Assert.AreEqual(3, combinations.Count, "Three");

            Assert.AreEqual(1, combinations[0].Count, "First combo; zero only");
            Assert.AreEqual(0, combinations[0][0], "First combo 0");

            Assert.AreEqual(1, combinations[1].Count, "Second combo one entry");
            Assert.AreEqual(1, combinations[1][0], "Second combo 1 only");

            Assert.AreEqual(2, combinations[2].Count, "Third combo two entries");
            Assert.AreEqual(1, combinations[2][0], "Third combo 1");
            Assert.AreEqual(0, combinations[2][1], "Third combo 0");
        }

        [Test]
        public void ConfirmCombinationSingleDigit()
        {
            var combinations = CombinationHelper.GetCombinations(3);
            Assert.AreEqual(1, combinations.Count, "One possibility");
            Assert.AreEqual(1, combinations[0].Count, "One entry in the possibility");
            Assert.AreEqual(3, combinations[0][0], "One entry");
        }

        [Test]
        public void ConfirmCombinationsTwoDigits()
        {
            var combinations = CombinationHelper.GetCombinations(12);
            Assert.AreEqual(3, combinations.Count, "Three possibilities");

            Assert.AreEqual(2, combinations[0][0], "First combo: 2 only");

            Assert.AreEqual(1, combinations[1][0], "Second combo 1 only");

            Assert.AreEqual(1, combinations[2][0], "Third combo 1");
            Assert.AreEqual(2, combinations[2][1], "Third combo 2");
        }

        /// <summary>
        /// 234 => 2,3,4,23,24,34,234
        /// </summary>
        [Test]
        public void ConfirmCombinationsThreeDigits()
        {
            var combinations = CombinationHelper.GetCombinations(234);
            Assert.AreEqual(7, combinations.Count, "Number of combination possibilities");

            Assert.AreEqual(1, combinations[0].Count, "First combo single entry");
            Assert.AreEqual(4, combinations[0][0], "First combo 4");

            Assert.AreEqual(1, combinations[1].Count, "Second combo single entry");
            Assert.AreEqual(3, combinations[1][0], "Second combo 3");

            Assert.AreEqual(2, combinations[2].Count, "Third combo two entries");
            Assert.AreEqual(3, combinations[2][0], "Third combo 3");
            Assert.AreEqual(4, combinations[2][1], "Third combo 4");

            Assert.AreEqual(1, combinations[3].Count, "Fourth combo single entry");
            Assert.AreEqual(2, combinations[3][0], "Fourth combo 2");

            Assert.AreEqual(2, combinations[4].Count, "Fifth combo two entries");
            Assert.AreEqual(2, combinations[4][0], "Fifth combo 2");
            Assert.AreEqual(4, combinations[4][1], "Fifth combo 4");

            Assert.AreEqual(2, combinations[5].Count, "Sixth combo two entries");
            Assert.AreEqual(2, combinations[5][0], "Sixth combo 2");
            Assert.AreEqual(3, combinations[5][1], "Sixth combo 3");

            Assert.AreEqual(3, combinations[6].Count, "Seventh combo three entries");
            Assert.AreEqual(2, combinations[6][0], "Sixth combo 2");
            Assert.AreEqual(3, combinations[6][1], "Sixth combo 3");
            Assert.AreEqual(4, combinations[6][2], "Sixth combo 4");
        }

        [Test]
        public void ExtractCombinationsOfThreeElementsFromFive()
        {
            var combinations = CombinationHelper.GetCombinations(new List<string> { "A", "B", "C", "D", "E" }, 3);

            Assert.AreEqual(10, combinations.Count(), "Ten ways to do this");

            // ABC
            // ABD
            // ABE
            // ACD
            // ACE
            // ADE
            // BCD
            // BCE
            // BDE
            // CDE

            var list = combinations.Select(combination => string.Concat(combination)).ToList();

            Assert.IsTrue(list.Contains("ABC"));
            Assert.IsTrue(list.Contains("ABD"));
            Assert.IsTrue(list.Contains("ABE"));
            Assert.IsTrue(list.Contains("ACD"));
            Assert.IsTrue(list.Contains("ACE"));
            Assert.IsTrue(list.Contains("ADE"));
            Assert.IsTrue(list.Contains("BCD"));
            Assert.IsTrue(list.Contains("BCE"));
            Assert.IsTrue(list.Contains("BDE"));
            Assert.IsTrue(list.Contains("CDE"));
        }

        [Test, Explicit("Very slow test")]
        [TestCase(168, 5)]
        [TestCase(50, 5)]
        [TestCase(40, 5)]
        [TestCase(30, 5)]
        [TestCase(20, 5)]
        public void ConfirmSizeOfLargerCombinationExtractions(int n, int r)
        {
            var expectedCombinationCount = MathHelper.LargeFactorial(n) /
                                           (MathHelper.LargeFactorial(r) * MathHelper.LargeFactorial(n - r));

            var combinations = CombinationHelper.GetCombinations(Enumerable.Range(1, n).ToList(), r);
            Assert.AreEqual(expectedCombinationCount, (BigInteger)combinations.Count(), "Have sufficient combinations");
        }

        [Test, Explicit("Very slow test")]
        public void ConfirmNumberOfCombinationsGenerated()
        {
            const int limit = 20;

            var factorialDictionary = new Dictionary<int, BigInteger>();
            for (int i = 0; i <= limit; ++i)
            {
                var fact = MathHelper.LargeFactorial(i);

                factorialDictionary.Add(i, fact);
            }

            for (var n = 2; n <= limit; ++n)
            {
                for (var r = 1; r < n; ++r)
                {
                    var combinations = CombinationHelper.GetCombinations(Enumerable.Range(1, n).ToList(), r);

                    var expectedCount = (factorialDictionary[n] / (factorialDictionary[r] * factorialDictionary[n - r]));

                    Assert.AreEqual(expectedCount, (BigInteger)combinations.Count(), string.Format("n: {0}  r: {1}", n, r));
                }
            }
        }
    }
}
