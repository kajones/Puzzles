using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Puzzles.Core.Helpers
{
    public class CombinationGenerator
    {
        // First key = Number of items to select from
        // Second key = Number of items to select in a combination
        // First list = Set of combinations
        // Second list = Items selected in a particular combination
        private readonly Dictionary<long, Dictionary<int, BinaryFlagSet>> flagPositions = new Dictionary<long, Dictionary<int, BinaryFlagSet>>();

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
        public IEnumerable<List<T>> GetCombinations<T>(List<T> elements, int numberToSelect)
        {
            var combinations = ProduceWithRecursion(elements, numberToSelect);
            return combinations;
        }

        public IEnumerable<List<T>> ProduceWithRecursion<T>(List<T> allValues, int numberToSelect)
        {
            var itemCount = allValues.Count;
            var limit = Math.Pow(2, itemCount);
            Assert.IsTrue(limit > 0, "Limit must be positive");

            EnsureItemCountIsPopulated(itemCount, limit);

            var flagSelections = flagPositions[itemCount][numberToSelect];
            foreach (var flagSelection in flagSelections)
            {
                yield return allValues.Where((value, index) => flagSelection.Contains(index))
                                      .Select(value => value)
                                      .ToList();
            }
        }

        private void EnsureItemCountIsPopulated(long itemCount, double limit)
        {
            if (flagPositions.ContainsKey(itemCount)) return;

            flagPositions.Add(itemCount, new Dictionary<int, BinaryFlagSet>());
            for (var possToSelect = 1; possToSelect <= itemCount; ++possToSelect)
            {
                flagPositions[itemCount].Add(possToSelect, new BinaryFlagSet());
            }

            var positions = new int[itemCount + 1];

            for (var i = 0; i < limit; ++i)
            {
                positions[0]++;
                for (var check = 0; check < itemCount; ++check)
                {
                    if (positions[check] <= 1) continue;

                    positions[check] = 0;
                    positions[check + 1]++;
                }

                var countFlagsSet = positions.Count(p => p == 1);
                var list = new List<int>();
                for (var idx = 0; idx < itemCount; ++idx)
                {
                    if (positions[idx] == 1)
                        list.Add(idx);
                }
                flagPositions[itemCount][countFlagsSet].Add(list);
            }
        }
    }

    public class BinaryFlagSet : List<List<int>>
    {
    }
}