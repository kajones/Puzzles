using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// NOTE: This problem is a significantly more challenging version of Problem 81.
    ///
    /// In the 5 by 5 matrix below, the minimal path sum from the top left to the bottom right, by moving left, right, up, and down, 
    /// is indicated in bold red and is equal to 2297.
    ///
    ///
    /// 131	673	234	103	18
    /// (1)     (5) (6) (7)
    /// 201	96	342	965	150
    /// (2) (3) (4)     (8)
    /// 630	803	746	422	111
    ///            (10) (9)
    /// 537	699	497	121	956
    ///            (11)
    /// 805	732	524	37	331
    ///            (12) (13)
    ///Find the minimal path sum, in matrix.txt (right click and 'Save Link/Target As...'), a 31K text file containing a 80 by 80 matrix, 
    /// from the top left to the bottom right by moving left, right, up, and down.
    /// </summary>
    [TestFixture]
    public class Problem_0083_PathSumFourWays
    {
        [Test]
        public void ConfirmExample()
        {
            var matrix = new[]
            {
                new long [] {131, 673, 234, 103, 18}, 
                new long [] {201, 96,  342,	965, 150}, 
                new long [] {630, 803, 746, 422, 111}, 
                new long [] {537, 699, 497, 121, 956},
                new long [] {805, 732, 524, 37, 331}
            };

            var pathHelper = new AStarPathHelper(matrix);
            var minPathTopLeftBottomRight = pathHelper.TopLeftBottomRight();

            foreach (var row in matrix)
            {
                Console.WriteLine(string.Empty);
                for (var idx = 0; idx < row.Length; ++idx)
                {
                    Console.Write("{0}  ", row[idx].ToString("0000"));
                }
                Console.WriteLine("---");

            }

            Assert.AreEqual(2297, minPathTopLeftBottomRight);
        }

        [Test]
        public void ConfirmAvoidingCirclingIntoSelf()
        {
            var matrix = new[]
            {
                new long [] {100, 673, 234, 103, 18}, 
                new long [] {100, 100, 342,	965, 150}, 
                new long [] {630, 100, 5,     4, 3}, 
                new long [] {537, 699, 6,   121, 2},
                new long [] {805, 732, 7,     8, 1}
            };

            var pathHelper = new AStarPathHelper(matrix);
            var minPath = pathHelper.TopLeftBottomRight();

            foreach (var row in matrix)
            {
                Console.WriteLine(string.Join(" ", row));
            }

            Assert.AreEqual(415, minPath);
        }

        /// <summary>
        /// 425185
        /// </summary>
        [Test, Explicit]
        public void FindMinimumPathForLargeMatrix()
        {
            const string filePath = "Puzzles.ProjectEuler.DataFiles.Problem_0083_matrix.txt";

            var fileContent = FileHelper.GetEmbeddedResourceContent(filePath);
            var fileLines = fileContent.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
            fileLines.Length.Should().Be(80);

            var matrix = new long[80][];
            var count = 0;
            foreach (var line in fileLines)
            {
                var elements = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                matrix[count] = new long[elements.Length];
                for (var i = 0; i <= elements.Length - 1; ++i)
                {
                    matrix[count][i] = Convert.ToInt32(elements[i]);
                }

                count++;
            }

            var pathHelper = new AStarPathHelper(matrix);
            var minPath = pathHelper.TopLeftBottomRight();

            Console.WriteLine("Path: {0}", minPath);

            minPath.Should().Be(425185);
        }
    }
}
