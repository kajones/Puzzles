using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// In the 5 by 5 matrix below, the minimal path sum from the top left to the bottom right, 
    /// by only moving to the right and down, is indicated in bold red and is equal to 2427.
    ///
    /// 131	673	234	103	18
    /// ---
    /// 201	96	342	965	150
    /// --- --- ---
    /// 630	803	746	422	111
    ///         --- ---
    /// 537	699	497	121	956
    ///             ---
    /// 805	732	524	37	331
    ///             --- ---
    /// Find the minimal path sum, in matrix.txt (right click and 'Save Link/Target As...'), 
    /// a 31K text file containing a 80 by 80 matrix, from the top left to the bottom right by only moving right and down.
    /// </summary>
    [TestFixture]
    public class Problem_0081_PathSumTwoWays
    {
        [Test]
        public void ConfirmExample()
        {
            var matrix = new[]
            {
                new [] {131, 673, 234, 103, 18}, 
                new [] {201, 96,  342,	965, 150}, 
                new [] {630, 803, 746, 422, 111}, 
                new [] {537, 699, 497, 121, 956},
                new [] {805, 732, 524, 37, 331}
            };

            var minPath = PathHelper.GetMinimumSumRightDown(matrix);

            foreach (var row in matrix)
            {
                Console.WriteLine(string.Join(" ", row));
            }

            Assert.AreEqual(2427, minPath);
        }

        /// <summary>
        /// 427337
        /// </summary>
        [Test, Explicit]
        public void FindMinimumPathFromLargeMatrix()
        {
            const string resourcePath = "Puzzles.ProjectEuler.DataFiles.Problem_0081_matrix.txt";
            var fileContent = FileHelper.GetEmbeddedResourceContent(resourcePath);
            var fileEntries = fileContent.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
            fileEntries.Length.Should().Be(80);

            var matrix = new int[80][];
            var count = 0;
            foreach (var line in fileEntries)
            {
                var elements = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                matrix[count] = new int[elements.Length];
                for (var i = 0; i <= elements.Length - 1; ++i)
                {
                    matrix[count][i] = Convert.ToInt32(elements[i]);
                }

                count++;
            }

            var minPath = PathHelper.GetMinimumSumRightDown(matrix);

            Console.WriteLine(minPath);

            minPath.Should().Be(427337);
        }
    }
}
