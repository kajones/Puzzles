using System;
using NUnit.Framework;
using Puzzles.Core;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// Starting in the top left corner of a 2×2 grid, and only being able to move to the right and down, 
    /// there are exactly 6 routes to the bottom right corner.
    /// 1. R R D D
    /// 2. R D R D
    /// 3. R D D R
    /// 4. D R R D
    /// 5. D R D R
    /// 6. D D R R
    /// 
    /// How many such routes are there through a 20×20 grid?
    /// </summary>
    [TestFixture]
    public class Problem_0015_LatticePaths
    {
        [Test]
        [TestCase(1, 2)]
        [TestCase(2, 6)]
        [TestCase(3, 20)]
        public void ConfirmKnownNumberOfRoutes(int gridSize, int expectedRoutes)
        {
            var routes = CalculateRoutes(gridSize);

            Assert.AreEqual(expectedRoutes, routes, "For grid " + gridSize);
        }

        //[Test, Explicit]
        //public void FindRoutesThroughATwentyByTwentyGrid()
        //{
        //    // 137846528820.00
        //    // 40*39*38...*21
        //    // divided by 20! 
        //    var routes = CalculateRoutes(20);

        //    Console.WriteLine(routes);
        //}

        /// <summary>
        /// Number of routes = Total number of steps (i.e. across and down the grid) required Factorial
        ///                    --------------------------------------------------------------
        ///                    Number of indistinguishable items (i.e. steps across) Factorial * Number of steps down Factorial
        /// 
        /// Since R1 = R2 = R3 i.e. you can't distinguish steps right
        /// </summary>
        /// <param name="gridSize"></param>
        /// <returns></returns>
        private static long CalculateRoutes(int gridSize)
        {
            var steps = gridSize * 2; // Right and down

            long numerator = 1;
            long denominator = MathHelper.Factorial(gridSize);

            for (var n = steps; n > gridSize; n--)
            {
                numerator *= n;
            }

            return numerator / denominator;
        }

    }
}
