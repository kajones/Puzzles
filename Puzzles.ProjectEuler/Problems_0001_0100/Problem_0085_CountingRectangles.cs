using System;
using FluentAssertions;
using NUnit.Framework;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// By counting carefully it can be seen that a rectangular grid measuring 3 by 2 contains eighteen rectangles:
    /// 6x 1x1
    /// 4x 2x1
    /// 2x 3x1
    /// 3x 1x2
    /// 2x 2x2
    /// 1x 3x2
    /// 
    /// Although there exists no rectangular grid that contains exactly two million rectangles, 
    /// find the area of the grid with the nearest solution.
    /// </summary>
    [TestFixture]
    public class Problem_0085_CountingRectangles
    {
        [Test]
        [TestCase(1, 1, 1)]
        [TestCase(1, 2, 3)]
        [TestCase(3, 2, 18)]
        [TestCase(2, 3, 18)]
        public void ConfirmExample(int width, int height, long expectedRectangles)
        {
            var rectangleCount = CountRectangles(width, height);
            Assert.AreEqual(expectedRectangles, rectangleCount);
        }

        /// <summary>
        /// 77 by 36 gives 1999998 rectangles; area: 2772
        /// </summary>
        [Test, Explicit]
        public void FindRectangleNearestToTwoMillion()
        {
            const long TwoMillion = 2000000;

            long width = 0;
            long height = 0;
            long rectangles = 0;
            long smallestDiff = long.MaxValue;

            const long limit = 1000;

            for (long propWidth = 2; propWidth < limit; ++propWidth)
            {
                for (long propHeight = 2; propHeight < propWidth; ++propHeight)
                {
                    var count = CountRectangles(propWidth, propHeight);
                    var diff = Math.Abs(TwoMillion - count);
                    if (diff < smallestDiff)
                    {
                        Console.WriteLine("Rect: {0:0,000,000} ({1} {2})", count, propWidth, propHeight);
                        width = propWidth;
                        height = propHeight;
                        rectangles = count;
                        smallestDiff = diff;
                    }

                    if (count > TwoMillion) break;
                }
            }

            Console.WriteLine("{0} by {1} gives {2} rectangles; area: {3}", width, height, rectangles, (width * height));

            (width*height).Should().Be(2772);
        }

        private static long CountRectangles(long width, long height)
        {
            long total = 0;

            for (var widthAdj = 0; widthAdj < width; ++widthAdj)
            {
                for (var heightAdj = 0; heightAdj < height; ++heightAdj)
                {
                    var rectWidth = (width - widthAdj);
                    var rectHeight = (height - heightAdj);
                    var countThisSize = (rectHeight * rectWidth);
                    total += countThisSize;
                }
            }

            return total;
        }
    }
}
