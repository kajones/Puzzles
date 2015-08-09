using System;
using System.Collections.Generic;

namespace Puzzles.Core.Helpers
{
    public static class SquareHelper
    {
        public static bool IsSquare(int n)
        {
            var rad = Math.Sqrt(n);
            var a = (int) rad*(int) rad;
            return (a == n);
        }

        public static long GetSquare(long n)
        {
            return (n*n);
        }

        public static List<int> GetSquaresUpTo(int limit)
        {
            var list = new List<int>();

            var i = 1;
            var square = i*i;
            while (square < limit)
            {
                list.Add(square);
                i++;
                square = i*i;
            }

            return list;
        }
    }
}