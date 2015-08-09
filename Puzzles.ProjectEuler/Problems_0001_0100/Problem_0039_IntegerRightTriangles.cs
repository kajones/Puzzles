using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// If p is the perimeter of a right angle triangle with integral length sides, 
    /// {a,b,c}, there are exactly three solutions for p = 120.
    ///
    /// {20,48,52}, {24,45,51}, {30,40,50}
    ///
    /// For which value of p ≤ 1000, is the number of solutions maximised?
    [TestFixture]
    public class Problem_0039_IntegerRightTriangles
    {
        [Test]
        public void ConfirmNumberOfSolutionsFor120()
        {
            var solutions = FindSolutions(120);
            Assert.AreEqual(3, solutions.Count, "Three");

            foreach (var solution in solutions)
            {
                Console.WriteLine(solution);
            }
        }

        [Test, Explicit]
        public void ConfirmSolutionsFor840()
        {
            var solutions = FindSolutions(840);
            foreach (var solution in solutions)
            {
                Console.WriteLine(solution);
            }
        }

        /// <summary>
        /// 840 (8)
        /// </summary>
        [Test, Explicit]
        public void FindThePerimeterWithTheMostTriangles()
        {
            var maxSolutions = 0;
            var maxPerimeter = 0;

            for (var perimeter = 5; perimeter <= 1000; ++perimeter)
            {
                var solutions = FindSolutions(perimeter);
                if (solutions.Count > maxSolutions)
                {
                    maxSolutions = solutions.Count;
                    maxPerimeter = perimeter;
                }
            }

            Console.WriteLine("Max: {0} with {1}", maxPerimeter, maxSolutions);

            maxPerimeter.Should().Be(840);
        }

        private IList<Triangle> FindSolutions(int perimeter)
        {
            var triangles = new List<Triangle>();

            var sideMax = perimeter / 2;
            for (var h = 5; h <= sideMax; ++h)
            {
                for (var a = 1; a < h; ++a)
                {
                    var b = perimeter - h - a;
                    if (b < a) continue;
                    var isRightAngledTriangle = IsRightAngledTriangle(a, b, h);
                    if (isRightAngledTriangle)
                    {
                        triangles.Add(new Triangle(a, b, h));
                    }
                }
            }

            return triangles;
        }

        private bool IsRightAngledTriangle(int a, int b, int h)
        {
            return ((Math.Pow(h, 2) - Math.Pow(a, 2) - Math.Pow(b, 2)) == 0);
        }
    }

    public struct Triangle
    {
        public int a { get; private set; }
        public int b { get; private set; }
        public int h { get; private set; }

        public Triangle(int a, int b, int h)
            : this()
        {
            this.a = a;
            this.b = b;
            this.h = h;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", a, b, h);
        }
    }
}
