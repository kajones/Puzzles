using System;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0101_0200
{
    /// <summary>
    /// Three distinct points are plotted at random on a Cartesian plane, 
    /// for which -1000 ≤ x, y ≤ 1000, such that a triangle is formed.
    ///
    /// Consider the following two triangles:
    ///
    /// A(-340,495), B(-153,-910), C(835,-947)
    ///
    /// X(-175,41), Y(-421,-714), Z(574,-645)
    ///
    /// It can be verified that triangle ABC contains the origin, 
    /// whereas triangle XYZ does not.
    ///
    /// Using triangles.txt (right click and 'Save Link/Target As...'), 
    /// a 27K text file containing the co-ordinates of one thousand "random" triangles, 
    /// find the number of triangles for which the interior contains the origin.
    ///
    /// NOTE: The first two examples in the file represent the triangles 
    /// in the example given above.
    /// </summary>
    [TestFixture]
    public class Problem_0102_TriangleContainment
    {
        [Test]
        [TestCase(-340,495,-153,-910,835,-947, true)]
        [TestCase(-175,41,-421,-714,574,-645, false)]
        public void ConfirmExamplePointInTriangle(int aX, int aY, int bX, int bY, int cX, int cY, bool expectedIncludeOrigin)
        {
            var includesOrigin = DoesIncludeOrigin(aX, aY, bX, bY, cX, cY);
            includesOrigin.Should().Be(expectedIncludeOrigin);
        }

        [Test]
        public void CountTrianglesContainingOrigin()
        {
            var fileContent = FileHelper.GetEmbeddedResourceContent("Puzzles.ProjectEuler.DataFiles.Problem_0102_triangles.txt");
            var fileLines = fileContent.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            fileLines.Length.Should().Be(1000);

            var countContainingOrigin = 0;

            foreach(var fileLine in fileLines)
            {
                var coordinates = fileLine.Split(',');
                coordinates.Length.Should().Be(6);
                var aX = Convert.ToInt32(coordinates[0]);
                var aY = Convert.ToInt32(coordinates[1]);
                var bX = Convert.ToInt32(coordinates[2]);
                var bY = Convert.ToInt32(coordinates[3]);
                var cX = Convert.ToInt32(coordinates[4]);
                var cY = Convert.ToInt32(coordinates[5]);

                var includesOrigin = DoesIncludeOrigin(aX, aY, bX, bY, cX, cY);
                if (includesOrigin)
                {
                    countContainingOrigin++;
                }
            }

            System.Console.WriteLine("Includes origin: {0}", countContainingOrigin);

            countContainingOrigin.Should().Be(228);
        }

        /// <summary>
        /// Barycentric coordinate allows to express new p coordinates as a linear combination of p1, p2, p3. More precisely, it defines 3 scalars a, b, c such that :
        ///
        /// x = a * x1 + b * x2  + c * x3
        /// y = a * y1 + b * y2 + c * y3
        /// a + b + c = 1
        ///
        /// The way to compute a, b, c is not difficult :
        ///
        /// a = ((y2 - y3)*(x - x3) + (x3 - x2)*(y - y3)) / ((y2 - y3)*(x1 - x3) + (x3 - x2)*(y1 - y3))
        /// b = ((y3 - y1)*(x - x3) + (x1 - x3)*(y - y3)) / ((y2 - y3)*(x1 - x3) + (x3 - x2)*(y1 - y3))
        /// c = 1 - a - b
        ///
        /// Then we just need to apply the interesting following property :
        ///
        /// p lies in T if and only if 0 <= a <= 1 and 0 <= b <= 1 and 0 <= c <= 1
        /// </summary>
        private static bool DoesIncludeOrigin(int aX, int aY, int bX, int bY, int cX, int cY)
        {
            double denominator = ((bY - cY) * (aX - cX) + (cX - bX) * (aY - cY));
            var a = ((bY - cY) * (0 - cX) + (cX - bX) * (0 - cY)) / denominator;
            var b = ((cY - aY) * (0 - cX) + (aX - cX) * (0 - cY)) / denominator;
            var c = 1 - a - b;

            if (!InRangeZeroToOne(a)) return false;
            if (!InRangeZeroToOne(b)) return false;
            if (!InRangeZeroToOne(c)) return false;

            return true;
        }

        private static bool InRangeZeroToOne(double candidate)
        {
            return 0 <= candidate && candidate <= 1;
        }
    }
}
