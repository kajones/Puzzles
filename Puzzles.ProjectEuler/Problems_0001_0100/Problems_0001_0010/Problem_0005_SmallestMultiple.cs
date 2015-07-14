using System;
using FluentAssertions;
using NUnit.Framework;

namespace Puzzles.ProjectEuler.Problems_0001_0100.Problems_0001_0010
{
    /// <summary>
    /// 2520 is the smallest number that can be divided by each of the numbers from 1 to 10 without any remainder.
    ///
    /// What is the smallest positive number that is evenly divisible by all of the numbers from 1 to 20?
    /// </summary>
    [TestFixture]
    public class Problem_0005_SmallestMultiple
    {
        [Test]
        public void FindSmallestNumberDivisibleBy1To10()
        {
            var smallestNumber = FindSmallestNumberDivisibleByAllFrom(1, 10);

            Assert.AreEqual(2520, smallestNumber);
        }

        /// <summary>
        /// 232792560
        /// </summary>
        [Test, Explicit]
        public void FindSmallestNumberDivisibleBy1To20()
        {
            var smallestNumber = FindSmallestNumberDivisibleByAllFrom(1, 20);

            Console.WriteLine(smallestNumber);

            smallestNumber.Should().Be(232792560);
        }

        private static int FindSmallestNumberDivisibleByAllFrom(int lowerFactor, int upperFactor)
        {
            var product = upperFactor;

            while (true)
            {
                var foundProduct = true;

                for (var factor = upperFactor; factor >= lowerFactor; --factor)
                {
                    if (product % factor == 0) continue;

                    foundProduct = false;
                    break;
                }

                if (foundProduct) break;

                product++;
            }

            return product;
        }
    }
}
