using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Puzzles.Core.Helpers
{
    public static class CombinationHelper
    {
        /// <summary>
        /// nCr = n!
        ///       --------
        ///       r!(n−r)!
        /// Where n = number of items available
        ///       r = number of items to select
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="elements"></param>
        /// <param name="numberToSelect"></param>
        /// <returns></returns>
        public static IEnumerable<List<T>> GetCombinations<T>(List<T> elements, int numberToSelect)
        {
            var combinations = ProduceWithRecursion(elements);
            return combinations.Where(c => c.Count == numberToSelect);
            //var combinations = GetCombinations(elements);
            //return combinations.Where(list => list.Count == numberToSelect).ToList();
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="elements"></param>
        /// <returns></returns>
        public static List<List<T>> GetCombinationsHide<T>(List<T> elements)
        {
            var collection = new List<List<T>>();
            var limit = (1 << elements.Count);
            Assert.IsTrue(limit > 0, "Limit must be positive");

            for (long counter = 0; counter < limit; ++counter)
            {
                var combination = new List<T>();
                for (int i = 0; i < elements.Count; ++i)
                {
                    if ((counter & (1 << i)) == 0)
                        combination.Add(elements[i]);
                }

                collection.Add(combination);
            }
            return collection;
        }

        public static IEnumerable<List<T>> ProduceWithRecursion<T>(List<T> allValues)
        {
            var itemCount = allValues.Count;
            var limit = Math.Pow(2, itemCount);
            Assert.IsTrue(limit > 0, "Limit must be positive");

            for (var i = 0; i < limit; i++)
            {
                yield return ConstructSetFromBits(itemCount, i).Select(n => allValues[n]).ToList();
            }
        }

        public static IEnumerable<int> ConstructSetFromBits(long itemCount, double positionsFor)
        {
            var positions = new int[itemCount + 1];

            for (var i = 0; i < positionsFor; ++i)
            {
                positions[0]++;
                for (var check = 0; check < itemCount; ++check)
                {
                    if (positions[check] <= 1) continue;

                    positions[check] = 0;
                    positions[check + 1]++;
                }
            }

            var list = new List<int>();
            for (var idx = 0; idx < positions.Length; ++idx)
            {
                if (positions[idx] == 1)
                    list.Add(idx);
            }
            return list;
        }

        public static List<List<int>> GetCombinations(int digitsToCombine)
        {
            var output = new List<List<int>>();

            if (digitsToCombine == 0)
            {
                output.Add(new List<int> { 0 });
                return output;
            }

            // Working buffer to build new sub-strings
            var buffer = new int[digitsToCombine];

            var digits = DigitHelper.GetDigits(digitsToCombine).ToArray();

            Combination2Recurse(digits, 0, buffer, 0, output);

            return output;
        }

        public static void Combination2Recurse(int[] input, int inputPos, int[] buffer, int bufferPos, List<List<int>> output)
        {
            if (inputPos >= input.Length)
            {
                // Add only non-empty strings
                if (bufferPos > 0)
                {
                    var res = new List<int>();
                    for (var i = 0; i < bufferPos; ++i)
                    {
                        res.Add(buffer[i]);
                    }
                    output.Add(res);
                }

                return;
            }

            // Recurse 2 times - one time without adding current input char, one time with.
            Combination2Recurse(input, inputPos + 1, buffer, bufferPos, output);

            buffer[bufferPos] = input[inputPos];
            Combination2Recurse(input, inputPos + 1, buffer, bufferPos + 1, output);
        }
    }

}