using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.Core.Helpers
{
    public static class ArrayMedianHelper
    {
        /// <summary>
        /// Extract the middle element from a sorted array of numbers
        /// - odd length array - middle
        /// - even length array - average of two middle
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static double GetMedian(int[] numbers)
        {
            var midIdx = (double) (numbers.Length-1) / 2;
            var floorMidIdx = Math.Floor(midIdx);
            if (midIdx == floorMidIdx) return numbers[(int)midIdx];

            return (double) (numbers[(int)floorMidIdx] + numbers[(int)floorMidIdx + 1]) / 2;
        }
    }
}
