using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    ///     /// By starting at the top of the triangle below and moving to adjacent numbers on the row below, 
    /// the maximum total from top to bottom is 23.
    ///
    ///    3
    ///   7 4
    ///  2 4 6
    /// 8 5 9 3
    ///
    /// That is, 3 + 7 + 4 + 9 = 23.
    ///
    /// Find the maximum total from top to bottom in triangle.txt (right click and 'Save Link/Target As...'), 
    /// a 15K text file containing a triangle with one-hundred rows.
    ///
    /// NOTE: This is a much more difficult version of Problem 18. 
    /// 
    /// It is not possible to try every route to solve this problem, as there are 299 altogether! 
    /// If you could check one trillion (1012) routes every second it would take over twenty billion years to check them all. 
    /// There is an efficient algorithm to solve it. ;o)
    /// </summary>
    [TestFixture]
    public class Problem_0067_MaximumPathSumII
    {
        /// <summary>
        /// Sum: 7273
        /// </summary>
        [Test, Explicit]
        public void FindMaxPathForLargeTriangle()
        {
            const string resourcePath = @"Puzzles.ProjectEuler.DataFiles.Problem_0067_triangle.txt";

            var fileContent = FileHelper.GetEmbeddedResourceContent(resourcePath);
            var fileLines = fileContent.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
            fileLines.Count().Should().Be(100);

            var triangle = new int[100][];
            var count = 0;
            foreach (var line in fileLines)
            {
                var elements = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                triangle[count] = new int[elements.Length];
                for (var i = 0; i <= elements.Length - 1; ++i)
                {
                    triangle[count][i] = Convert.ToInt32(elements[i]);
                }

                count++;
            }

            var maxSum = PathHelper.GetMaximumSum(triangle);
            Console.WriteLine("Sum: {0}", maxSum);

            maxSum.Should().Be(7273);
        }
    }
}
