using System;
using FluentAssertions;
using NUnit.Framework;

namespace Puzzles.ProjectEuler.Problems_0001_0100.Problems_0001_0010
{
    /// <summary>
    /// The sum of the squares of the first ten natural numbers is,
    ///
    /// 1squared + 2squared + ... + 10squared = 385
    /// The square of the sum of the first ten natural numbers is,
    ///
    /// (1 + 2 + ... + 10)2 = 55squared = 3025
    /// Hence the difference between the sum of the squares of the first ten natural numbers and the square of the sum is 3025 − 385 = 2640.
    ///
    /// Find the difference between the sum of the squares of the first one hundred natural numbers and the square of the sum.
    /// </summary>
    [TestFixture]
    public class Problem_0006_SumSquareDifference
    {
        [Test]
        public void SumSquaresFirstTenNaturalNumbers()
        {
            var result = FindSumSquaresTo(10);

            Assert.AreEqual(385, result.SumOfSquares, "Sum of squares");
            Assert.AreEqual(3025, result.SquareSum, "Square sum");

            Assert.AreEqual(2640, result.Difference, "Difference");
        }

        /// <summary>
        /// Squares 338350 and 25502500, diff 25164150
        /// </summary>
        [Test]
        public void SumSquaresFirstHundredNaturalNumbers()
        {
            var result = FindSumSquaresTo(100);

            Console.WriteLine("Squares {0} and {1}, diff {2}", result.SumOfSquares, result.SquareSum, result.Difference);

            result.SumOfSquares.Should().Be(338350);
            result.SquareSum.Should().Be(25502500);
            result.Difference.Should().Be(25164150);
        }

        private static SumSquare FindSumSquaresTo(int limit)
        {
            long sumOfSquares = 0;
            long sum = 0;

            for (var number = 1; number <= limit; ++number)
            {
                sumOfSquares += (long)Math.Pow(number, 2);
                sum += number;
            }

            var squareSum = (long)Math.Pow(sum, 2);
            return new SumSquare(sumOfSquares, squareSum);
        }
    }

    public struct SumSquare
    {
        public long SumOfSquares { get; private set; }
        public long SquareSum { get; private set; }

        public long Difference { get { return SquareSum - SumOfSquares; } }

        public SumSquare(long sumOfSquares, long squareSum)
            : this()
        {
            SumOfSquares = sumOfSquares;
            SquareSum = squareSum;
        }
    }
}
