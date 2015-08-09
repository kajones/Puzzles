using System;
using System.Security.Cryptography.X509Certificates;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// The points P (x1, y1) and Q (x2, y2) are plotted at integer co-ordinates and are joined to the origin, O(0,0), to form ΔOPQ.
    /// 
    /// There are exactly fourteen triangles containing a right angle that can be formed when each co-ordinate lies between 0 and 2 inclusive; 
    /// that is, 0 ≤ x1, y1, x2, y2 ≤ 2.
    /// 
    /// Given that 0 ≤ x1, y1, x2, y2 ≤ 50, how many right triangles can be formed?
    /// </summary>
    [TestFixture]
    public class Problem_0091_RightTriangleIntegerCoordinates
    {
        [Test]
        public void ConfirmExample()
        {
            var actualTriangles = CountTriangles(2);
            actualTriangles.Should().Be(14);
        }

        /// <summary>
        /// 14234
        /// </summary>
        [Test, Explicit]
        public void FindTrianglesIn50by50Grid()
        {
            var actualTriangles = CountTriangles(50);
            Console.WriteLine(actualTriangles);
            actualTriangles.Should().Be(14234);
        }

        private static long CountTriangles(int sideLength)
        {
            // Where right angle is at origin, have sideLength * sideLength possibilities
            // Similarly where rotating 90 degrees clockwise/anticlockwise still involving the origin you have sideLength * sideLength possibilities
            // Therefore the cases involving the axes are 3 * sideLength * sideLength
            var axesTriangles = 3*sideLength*sideLength;

            // The other cases are where the right angle is within the grid so the line from the origin to a point in the grid then forms
            // a right angle to reach another point in the grid
            // Given any point x1 y1 in order to form a right angle the slope away must be y1 across -x1 down from the grid point
            // There can be a number of these until either that line passes the limit (the length of the side) OR the line goes below the x axis (i.e. y=0)
            // Each of these points will be at the GCD from the mid grid point
            // Then similarly you can do a right angle towards the y axis so double the answer found
            long result = axesTriangles;

            for (var x = 1; x <= sideLength; ++x)
            {
                for (var y = 1; y <= sideLength; ++y)
                {
                    var gcd = FactorHelper.GetGreatestCommonDivisor(x, y);
                    var optionsBeforeXLimit = (sideLength - x) * gcd/y;
                    var optionsBeforeXAxis = (y*gcd)/x;
                    var optionsAvailable = Math.Min(optionsBeforeXAxis, optionsBeforeXLimit)*2;
                    result += optionsAvailable;
                }
            }

            return result;
        }
    }
}
