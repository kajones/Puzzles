using System;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// It turns out that 12 cm is the smallest length of wire that can be bent to form an integer sided right angle triangle 
    /// in exactly one way, but there are many more examples.
    ///
    /// 12 cm: (3,4,5)
    /// 24 cm: (6,8,10)
    /// 30 cm: (5,12,13)
    /// 36 cm: (9,12,15)
    /// 40 cm: (8,15,17)
    /// 48 cm: (12,16,20)
    ///
    /// In contrast, some lengths of wire, like 20 cm, cannot be bent to form an integer sided right angle triangle, 
    /// and other lengths allow more than one solution to be found; for example, 
    /// using 120 cm it is possible to form exactly three different integer sided right angle triangles.
    ///
    /// 120 cm: (30,40,50), (20,48,52), (24,45,51)
    ///
    /// Given that L is the length of the wire, for how many values of L ≤ 1,500,000 
    /// can exactly one integer sided right angle triangle be formed?
    /// </summary>
    [TestFixture]
    public class Problem_0075_SingleIntegerRightTriangles
    {
        [Test, Explicit]
        [TestCase(12, 1)]
        [TestCase(20, 0)]
        [TestCase(24, 1)]
        [TestCase(30, 1)]
        [TestCase(36, 1)]
        [TestCase(40, 1)]
        [TestCase(48, 1)]
        public void ConfirmExamples(long length, int expectedCount)
        {
            var count = FindCountRightTriangles(length);
            Assert.AreEqual(expectedCount, count);
        }

        [Test, Explicit]
        [TestCase(12, true)]
        [TestCase(20, false)]
        [TestCase(24, true)]
        [TestCase(30, true)]
        [TestCase(36, true)]
        [TestCase(40, true)]
        [TestCase(48, true)]
        [TestCase(120, false)]
        public void ConfirmSingleTriangle(long length, bool expectcedIsSingle)
        {
            var isSingle = FindIfSingleRightTrianglesUsingSemiPerimeter(length);
            Assert.AreEqual(expectcedIsSingle, isSingle);
        }

        [Test]
        public void FindCountOfSingleTrianglesUsingTripleGeneration()
        {
            const int limit = 1500000;
            var triangles = new int[limit + 1];

            int result = 0;
            int mlimit = (int)Math.Sqrt(limit / 2);

            for (long m = 2; m < mlimit; m++)
            {
                for (long n = 1; n < m; n++)
                {
                    if (((n + m) % 2) == 1 && FactorHelper.AreCoprime(m, n))
                    {
                        long a = m * m + n * n;
                        long b = m * m - n * n;
                        long c = 2 * m * n;
                        long p = a + b + c;
                        while (p <= limit)
                        {
                            triangles[p]++;
                            if (triangles[p] == 1) result++;
                            if (triangles[p] == 2) result--;
                            p += a + b + c;
                        }
                    }
                }
            }

            Console.WriteLine("Number of single triangles: {0}", result);

            result.Should().Be(161667);
        }

        [Test, Ignore("Too slow")]
        public void FindCountOneTriangleOnly()
        {
            var count = 0;

            for (var length = 12; length <= 1500000; length += 2)
            {
                var triangles = FindCountRightTriangles(length);
                if (triangles == 1)
                    count++;
            }

            Console.WriteLine("Count: {0}", count);
        }

        private bool FindIfSingleRightTrianglesUsingSemiPerimeter(long length)
        {
            var semiPerimeter = length / 2;
            var count = 0;
            for (var c = length / 3; c < semiPerimeter; ++c)
            {
                var numerator = semiPerimeter * (semiPerimeter - c);
                for (var a = 1; a < c; ++a)
                {
                    var denominator = semiPerimeter - a;
                    if ((numerator % denominator) != 0) continue;

                    var b = length - c - a;
                    if (b < a) break;
                    if (b >= c) continue;

                    var calcB = semiPerimeter - (numerator / denominator);
                    if (calcB == b)
                    {
                        count++;
                        if (count > 1) return false;
                    }
                }
            }

            return (count == 1);
        }

        private int FindCountRightTriangles(long length)
        {
            var count = 0;

            for (long hyp = length / 3; hyp < length / 2; ++hyp)
            {
                var hypSquared = (long)Math.Pow(hyp, 2);
                for (long a = 1; a < hyp; ++a)
                {
                    var b = length - hyp - a;
                    if (b < a) break;
                    if (b > hyp) continue;

                    var sumSquareSides = (long)Math.Pow(a, 2) + (long)Math.Pow(b, 2);
                    var isRight = (hypSquared == sumSquareSides);
                    if (isRight)
                        count++;

                    if (count > 1) return count;
                }
            }

            return count;
        }

        private bool IsRightTriangle(long a, long b, long hyp)
        {
            var hypSquared = (long)Math.Pow(hyp, 2);
            var squareSides = (long)Math.Pow(a, 2) + Math.Pow(b, 2);

            return (hypSquared == squareSides);
        }
    }
}
