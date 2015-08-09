using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// NOTE: This problem is a more challenging version of Problem 81.
    ///
    /// The minimal path sum in the 5 by 5 matrix below, by starting in any cell in the left column and finishing in any cell in the right column,
    /// and only moving up, down, and right, is indicated in red and bold; the sum is equal to 994.
    ///
    ///
    /// 131	673	234	103	18
    ///         --- --- ---
    /// 201	96	342	965	150
    /// --- --- ---
    /// 630	803	746	422	111
    /// 
    /// 537	699	497	121	956
    /// 
    /// 805	732	524	37	331
    ///
    /// Find the minimal path sum, in matrix.txt (right click and 'Save Link/Target As...'), a 31K text file containing a 80 by 80 matrix, 
    /// from the left column to the right column.
    /// </summary>
    [TestFixture]
    public class Problem_0082_PathSumThreeWays
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

            var minPath = PathHelper.LeftToRight(matrix);

            foreach (var row in matrix)
            {
                Console.WriteLine(string.Join(" ", row));
            }

            Assert.AreEqual(994, minPath);
        }

        /// <summary>
        /// 260324
        /// </summary>
        [Test, Explicit]
        public void FindMinimumRouteLeftToRightOfLargeMatrix()
        {
            const string resourcePath = "Puzzles.ProjectEuler.DataFiles.Problem_0082_matrix.txt";

            var fileContent = FileHelper.GetEmbeddedResourceContent(resourcePath);
            var fileLines = fileContent.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
            fileLines.Length.Should().Be(80);

            var matrix = new int[80][];
            var count = 0;
            foreach (var line in fileLines)
            {
                var elements = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                matrix[count] = new int[elements.Length];
                for (var i = 0; i <= elements.Length - 1; ++i)
                {
                    matrix[count][i] = Convert.ToInt32(elements[i]);
                }

                count++;
            }

            var minPath = PathHelper.LeftToRight(matrix);

            Console.WriteLine(minPath);
            minPath.Should().Be(260324);
        }
    }
}
