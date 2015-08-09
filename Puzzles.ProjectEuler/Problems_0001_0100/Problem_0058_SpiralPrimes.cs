using System;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// Starting with 1 and spiralling anticlockwise in the following way, a square spiral with side length 7 is formed.
    ///
    /// 37 36 35 34 33 32 31
    /// 38 17 16 15 14 13 30
    /// 39 18  5  4  3 12 29
    /// 40 19  6  1  2 11 28
    /// 41 20  7  8  9 10 27
    /// 42 21 22 23 24 25 26
    /// 43 44 45 46 47 48 49
    ///
    /// It is interesting to note that the odd squares lie along the bottom right diagonal, 
    /// but what is more interesting is that 8 out of the 13 numbers lying along both diagonals are prime; that is, a ratio of 8/13 ≈ 62%.
    ///
    /// If one complete new layer is wrapped around the spiral above, a square spiral with side length 9 will be formed. 
    /// If this process is continued, what is the side length of the square spiral 
    /// for which the ratio of primes along both diagonals first falls below 10%?
    /// </summary>
    [TestFixture]
    public class Problem_0058_SpiralPrimes
    {
        [Test]
        [TestCase(1, 1, 0, 0, 0)]
        [TestCase(3, 5, 3, 9, 7)]
        [TestCase(5, 17, 13, 25, 21)]
        [TestCase(7, 37, 31, 49, 43)]
        [TestCase(9, 65, 57, 81, 73)]
        public void ConfirmCorners(int length, long expectedTopLeft, long expectedTopRight, long expectedBottomRight, long expectedBottomLeft)
        {
            var corners = GetCorners(length);

            Assert.AreEqual(expectedTopLeft, corners.TopLeft, "Top Left");
            Assert.AreEqual(expectedTopRight, corners.TopRight, "Top Right");
            Assert.AreEqual(expectedBottomRight, corners.BottomRight, "Bottom Right");
            Assert.AreEqual(expectedBottomLeft, corners.BottomLeft, "BottomLeft");
        }

        /// <summary>
        /// Ratio 0.0999980945485033 at length 26241
        /// </summary>
        [Test, Explicit]
        public void FindSideLengthWithPrimeRatioBelowTenPercent()
        {
            long primeCount = 3;
            long cornerCount = 5;
            int length = 3;
            double primeRatio = (double)primeCount / (double)cornerCount;

            while (primeRatio > 0.1)
            {
                length += 2;
                var corners = GetCorners(length);
                cornerCount += 4;

                if (PrimeHelper.IsPrime(corners.TopLeft)) primeCount++;
                if (PrimeHelper.IsPrime(corners.TopRight)) primeCount++;
                if (PrimeHelper.IsPrime(corners.BottomLeft)) primeCount++;
                // Bottom right always square of length so not prime

                primeRatio = (double)primeCount / (double)cornerCount;
            }

            Console.WriteLine("Ratio {0} at length {1}", primeRatio, length);

            length.Should().Be(26241);
        }

        private Corners GetCorners(int length)
        {
            if (length == 1) return new Corners(1, 0, 0, 0);
            long topLeft = 5;
            long topRight = 3;
            long bottomRight = 9;
            long bottomLeft = 7;
            if (length == 2) return new Corners(topLeft, topRight, bottomRight, bottomLeft);

            var sideLength = 3;
            while (sideLength < length)
            {
                var previousSideLength = sideLength;
                sideLength += 2;

                topLeft += (4 * previousSideLength);
                topRight += (4 * previousSideLength) - 2;
                bottomRight = (sideLength * sideLength);
                bottomLeft += (previousSideLength - 1) + (3 * (sideLength - 1));
            }

            return new Corners(topLeft, topRight, bottomRight, bottomLeft);
        }
    }

    public struct Corners
    {
        public long TopLeft { get; private set; }
        public long TopRight { get; private set; }
        public long BottomRight { get; private set; }
        public long BottomLeft { get; private set; }

        public Corners(long topLeft, long topRight, long bottomRight, long bottomLeft)
            : this()
        {
            TopLeft = topLeft;
            TopRight = topRight;
            BottomRight = bottomRight;
            BottomLeft = bottomLeft;
        }
    }
}
