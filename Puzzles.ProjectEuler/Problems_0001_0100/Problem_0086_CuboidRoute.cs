using System;
using NUnit.Framework;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// A spider, S, sits in one corner of a cuboid room, measuring 6 by 5 by 3, and a fly, F, sits in the opposite corner. 
    /// By travelling on the surfaces of the room the shortest "straight line" distance from S to F is 10 and the path is shown on the diagram.
    ///
    ///  W = 6
    ///  D = 5
    ///  H = 3
    ///
    /// However, there are up to three "shortest" path candidates for any given cuboid and the shortest route doesn't always have integer length.
    ///
    /// By considering all cuboid rooms with integer dimensions, up to a maximum size of M by M by M, 
    /// there are exactly 2060 cuboids for which the shortest route has integer length when M=100, 
    /// and this is the least value of M for which the number of solutions first exceeds two thousand; 
    /// the number of solutions is 1975 when M=99.
    ///
    /// Find the least value of M such that the number of solutions first exceeds one million.
    /// </summary>
    [TestFixture]
    public class Problem_0086_CuboidRoute
    {
        [Test, Explicit]
        public void CalculateShortestRouteExample()
        {
            var shortestRoute = CalculateShortestRoute(3, 5, 6);
            Assert.AreEqual(10, shortestRoute);
        }

        [Test, Explicit]
        public void ValueIsInteger()
        {
            double myValue = 3.14;
            double myIntegerValue = 4;

            Assert.IsFalse(IsInteger(myValue));
            Assert.IsTrue(IsInteger(myIntegerValue));
        }

        [Test]
        [TestCase(100, 2060)]
        [TestCase(99, 1975)]
        public void ConfirmExamples(long maximumSize, long expectedIntegerRouteIsShortes)
        {
            var count = CountOfIntegerShortRoutes(maximumSize);
            Assert.AreEqual(expectedIntegerRouteIsShortes, count);
        }

        [Test]
        public void GivenXCalculateXPlusOne()
        {
            //long maximumSize = 99;
            //long count = 1975;

            var countForMax = CountOfIntegerShortRoutesUsing(100);
            Assert.AreEqual(85, countForMax, "2060-1975");
        }

        /// <summary>
        /// Max: 1818 creates 1000457
        /// </summary>
        [Test, Explicit]
        public void FindLowestMWhereAMillionShortIntegerRoutes()
        {
            long maximumSize = 1800;
            long total = 986995;

            while (total < 1000000)
            {
                Console.WriteLine("Current: {0}: {1}", maximumSize, total);
                maximumSize++;
                var count = CountOfIntegerShortRoutesUsing(maximumSize);
                total += count;
            }

            Console.WriteLine("Max: {0} creates {1}", maximumSize, total);
        }


        //taking a cube of size (x y z) with x <= y <= z,
        //the shortest path will always be
        //path(x,y,z) = sqrt( (x+y)^2 + z^2 )

        //so the result for any M is counting all the different instances for x y z satisfying 1 <= x <= y <= z <= M 
        //where path(x,y,z) is an integer

        //there are a lot of combinations of x and y where path
        //computes essentially the same value, so to save time, i = x + y can be substituted and the results multiplied by the number of combinations of x and y that can make i

        //I can post some nearly impossible to read lisp code that runs in 4 seconds on my computer, if you like, but it looks mostly like this

        //sum = 0
        //loop for j from 1 to M
        //  loop for i from 2 to 2*j
        //    if path_is_integral (i j) then sum = sum + (combinations i j)

        //where combinations (i j) is the number of ways to choose x and y satisfying 1 <= x <= y <= j and x + y = i

        private long CountOfIntegerShortRoutesUsing(long maximumSize)
        {
            long count = 0;

            long width = maximumSize;

            for (long depth = 1; depth <= width; ++depth)
            {
                for (long height = 1; height <= depth; ++height)
                {
                    var shortestIsInteger = IsShortestRouteInteger(width, depth, height);
                    if (shortestIsInteger)
                        count++;
                }
            }

            return count;
        }

        private long CountOfIntegerShortRoutes(long maximumSize)
        {
            long count = 0;

            for (long width = 1; width <= maximumSize; ++width)
            {
                for (long depth = 1; depth <= width; ++depth)
                {
                    for (long height = 1; height <= depth; ++height)
                    {
                        var shortestIsInteger = IsShortestRouteInteger(width, depth, height);
                        if (shortestIsInteger)
                            count++;
                    }
                }
            }

            return count;
        }

        private bool IsShortestRouteInteger(long width, long depth, long height)
        {
            var shortestRoute = CalculateShortestRoute(width, depth, height);
            return IsInteger(shortestRoute);
        }

        private static bool IsInteger(double doubleValue)
        {
            return doubleValue == Math.Floor(doubleValue);
        }

        private static double CalculateShortestRoute(long width, long depth, long height)
        {
            var short1 = Math.Sqrt((width * width) + ((depth + height) * (depth + height)));
            var short2 = Math.Sqrt((depth * depth) + ((width + height) * (width + height)));
            var short3 = Math.Sqrt((height * height) + ((width + depth) * (width + depth)));

            return Math.Min(short1, Math.Min(short2, short3));
        }
    }
}
