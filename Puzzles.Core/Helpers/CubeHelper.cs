using System;

namespace Puzzles.Core.Helpers
{
    public static class CubeHelper
    {
        private const double AThird = (double)1 / (double)3;

        public static bool IsCube(long candidate)
        {
            var candidateCeiling = Math.Ceiling(Math.Pow(candidate, AThird));
            var candidateFloor = Math.Floor(Math.Pow(candidate, AThird));

            var ceilingCube = (long)Math.Pow(candidateCeiling, 3);
            var floorCube = (long)Math.Pow(candidateFloor, 3);
            
            return (candidate == ceilingCube || candidate == floorCube);
        }

        public static long GetCube(long n)
        {
            return (n * n * n);
        }
    }
}