using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// A permutation is an ordered arrangement of objects. 
    /// For example, 3124 is one possible permutation of the digits 1, 2, 3 and 4. 
    /// If all of the permutations are listed numerically or alphabetically, we call it lexicographic order. 
    /// The lexicographic permutations of 0, 1 and 2 are:
    ///
    /// 012   021   102   120   201   210
    ///
    /// What is the millionth lexicographic permutation of the digits 0, 1, 2, 3, 4, 5, 6, 7, 8 and 9?
    /// </summary>
    [TestFixture]
    public class Problem_0024_LexicographicPermutations
    {
        [Test]
        public void ConfirmPermutationsFor012()
        {
            var permutations = GetPermutations(new[] { 0, 1, 2 });

            Assert.AreEqual(6, permutations.Count, "Six permutations");
            Assert.AreEqual("012", permutations[0], "1");
            Assert.AreEqual("021", permutations[1], "2");
            Assert.AreEqual("102", permutations[2], "3");
            Assert.AreEqual("120", permutations[3], "4");
            Assert.AreEqual("201", permutations[4], "5");
            Assert.AreEqual("210", permutations[5], "6");
        }

        [Test]
        [TestCase(1, new[] { 0, 1, 2 }, "012")]
        [TestCase(2, new[] { 0, 1, 2 }, "021")]
        [TestCase(3, new[] { 0, 1, 2 }, "102")]
        [TestCase(4, new[] { 0, 1, 2 }, "120")]
        [TestCase(5, new[] { 0, 1, 2 }, "201")]
        [TestCase(6, new[] { 0, 1, 2 }, "210")]
        public void FindNthPermutation(int position, int[] array, string expectedPermutation)
        {
            var permutation = GetPermutationAt(position, array);
            Assert.AreEqual(expectedPermutation, permutation, "For " + position);
        }

        /// <summary>
        /// 2783915460
        /// </summary>
        [Test, Explicit]
        public void FindMillionthPermutationForTenDigits()
        {
            var permutation = GetPermutationAt(1000000, new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            Console.WriteLine(permutation);

            permutation.Should().Be("2783915460");
        }

        private static string GetPermutationAt(int position, IEnumerable<int> digits)
        {
            var sortedDigits = digits.ToList();
            sortedDigits.Sort();

            var array = sortedDigits.ToArray();

            var currentPosition = 1;

            while (currentPosition < position)
            {
                if (!PermutationHelper.NextPermutation(array))
                    return "Error! - ran out of permutations";

                currentPosition++;
            }

            return string.Concat(array);
        }

        private static IList<string> GetPermutations(IEnumerable<int> digits)
        {
            var sortedDigits = digits.ToList();
            sortedDigits.Sort();

            var array = sortedDigits.ToArray();

            var list = new List<string>();

            do
            {
                list.Add(string.Concat(array));
            } while (PermutationHelper.NextPermutation(array));

            return list;
        }

    }
}
