using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// The cube, 41063625 (345^3), can be permuted to produce two other cubes: 
    /// 56623104 (384^3) and 66430125 (405^3). 
    /// In fact, 41063625 is the smallest cube which has exactly three permutations of its digits which are also cube.
    ///
    /// Find the smallest cube for which exactly five permutations of its digits are cube.
    /// </summary>
    [TestFixture]
    public class Problem_0062_CubicPermutations
    {
        [Test]
        public void ConfirmExample()
        {
            var isCube = CubeHelper.IsCube(41063625);
            Assert.IsTrue(isCube, "First is cube");

            isCube = CubeHelper.IsCube(56623104);
            Assert.IsTrue(isCube, "Second is cube");

            isCube = CubeHelper.IsCube(66430125);
            Assert.IsTrue(isCube, "Third is cube");

            FindCubeSet(300, 3);
        }

        //        Key: 012334556789
        //127035954683,352045367981,373559126408,569310543872,589323567104
        [Test, Explicit]
        public void FindPermutationsOfCubes()
        {
            const long lowerLimit = 99999999999;
            const long upperLimit = 1000000000000;

            var dict = new Dictionary<string, List<long>>();

            long n = 0;
            long cube = CubeHelper.GetCube(n);
            while (cube < upperLimit)
            {
                if (cube > lowerLimit)
                {
                    var digits = DigitHelper.GetDigits(cube);
                    var key = string.Concat(digits.OrderBy(d => d));
                    if (!dict.ContainsKey(key))
                        dict.Add(key, new List<long>());

                    dict[key].Add(cube);
                }

                n++;
                cube = CubeHelper.GetCube(n);
            }

            if (dict.Values.Any(c => c.Count == 5))
            {
                foreach (var dictEntry in dict.Where(kvp => kvp.Value.Count == 5))
                {
                    Console.WriteLine("Key: {0}", dictEntry.Key);
                    Console.WriteLine(string.Join(",", dictEntry.Value));
                }
            }
            else
            {
                Console.WriteLine("No five cube set");
                Console.WriteLine("Max entries: {0}", dict.Max(de => de.Value.Count));
            }

            if (dict.ContainsKey("01234566"))
            {
                Console.WriteLine(string.Join(",", dict["01234566"]));
            }

            // 41063625
            // 01234566
        }

        private static void FindCubeSet(long firstBase, int countOfCubesRequired)
        {
            long firstCube = 0;

            while (true)
            {
                try
                {
                    firstCube = CubeHelper.GetCube(firstBase);
                    var digits = DigitHelper.GetDigits(firstCube).ToList();
                    var permutationsOfCube = PermutationHelper.GetLongPermutations(digits);
                    var permutationsOfSameLength =
                        permutationsOfCube.Where(permutation => DigitHelper.GetNumberLength(permutation) == digits.Count()).ToList();
                    if (permutationsOfSameLength.Count() >= countOfCubesRequired)
                    {
                        var countOfCubes = permutationsOfSameLength.Count(CubeHelper.IsCube);
                        if (countOfCubes == countOfCubesRequired)
                        {
                            Console.WriteLine("Starting cube: {0}", firstCube);
                            break;
                        }
                    }

                    firstBase++;

                }
                catch (Exception)
                {
                    Console.WriteLine("Failing with cube {0}", firstCube);
                    throw;
                }
            }
        }
    }
}
