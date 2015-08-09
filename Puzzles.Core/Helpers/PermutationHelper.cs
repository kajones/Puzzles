using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Puzzles.Core.Helpers
{
    public static class PermutationHelper
    {
        public static bool ArePermutations(long first, long second)
        {
            var firstChars = first.ToString(CultureInfo.InvariantCulture).ToCharArray();
            var secondChars = second.ToString(CultureInfo.InvariantCulture).ToList();

            if (firstChars.Count() != secondChars.Count) return false;

            foreach (var firstChar in firstChars)
            {
                secondChars.Remove(firstChar);
            }

            return secondChars.Count == 0;
        }

        /// <summary>
        /// Return permutations that are rotations of the original
        /// i.e. abc => abc, bca, cab
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static IList<int> GetRotationPermutations(int number)
        {
            var digits = DigitHelper.GetDigits(number).ToList();

            var list = new List<int>();

            var length = digits.Count();

            for (var offset = 0; offset < length; ++offset)
            {
                var permutation = 0;
                for (var idx = 0; idx < length; ++idx)
                {
                    permutation *= 10;

                    var position = (offset + idx) % length;
                    permutation += digits[position];
                }

                list.Add(permutation);
            }

            return list;
        }

        public static IList<int> GetPermutations(IEnumerable<int> digits)
        {
            var sortedDigits = digits.ToList();
            sortedDigits.Sort();

            var array = sortedDigits.ToArray();

            var list = new List<int>();

            do
            {
                list.Add(Convert.ToInt32(string.Concat(array)));
            } while (NextPermutation(array));

            return list;
        }

        public static IList<long> GetLongPermutations(long number)
        {
            var digits = DigitHelper.GetDigits(number);
            return GetLongPermutations(digits);
        }

        public static IList<long> GetLongPermutations(IEnumerable<int> digits)
        {
            var sortedDigits = digits.ToList();
            sortedDigits.Sort();

            var array = sortedDigits.ToArray();

            var list = new List<long>();

            do
            {
                try
                {
                    list.Add(Convert.ToInt64(string.Concat(array)));
                }
                catch (Exception)
                {
                    Console.WriteLine(string.Join(",", digits));
                    throw;
                }
            } while (NextPermutation(array));

            return list;
        }

        /// <summary>
        /// http://nayuki.eigenstate.org/page/next-lexicographical-permutation-algorithm
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool NextPermutation(int[] array)
        {
            // Find longest non-increasing suffix
            int i = array.Length - 1;
            while (i > 0 && array[i - 1] >= array[i])
                i--;
            // Now i is the head index of the suffix

            // Are we at the last permutation already?
            if (i == 0)
                return false;

            // Let array[i - 1] be the pivot
            // Find rightmost element that exceeds the pivot
            int j = array.Length - 1;
            while (array[j] <= array[i - 1])
                j--;
            // Now the value array[j] will become the new pivot
            // Assertion: j >= i

            // Swap the pivot with j
            int temp = array[i - 1];
            array[i - 1] = array[j];
            array[j] = temp;

            // Reverse the suffix
            j = array.Length - 1;
            while (i < j)
            {
                temp = array[i];
                array[i] = array[j];
                array[j] = temp;
                i++;
                j--;
            }

            // Successfully computed the next permutation
            return true;
        }
    }
}
