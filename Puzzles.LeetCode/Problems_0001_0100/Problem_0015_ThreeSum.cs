using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;
using System.Diagnostics;

namespace Puzzles.LeetCode.Problems_0001_0100
{
    /// <summary>
    /// Given an array S of n integers, are there elements a, b, c in S such that a + b + c = 0? 
    /// Find all unique triplets in the array which gives the sum of zero.
    ///
    /// Note:
    /// Elements in a triplet (a,b,c) must be in non-descending order. (ie, a ≤ b ≤ c)
    /// The solution set must not contain duplicate triplets.
    ///    For example, given array S = {-1 0 1 2 -1 -4},
    ///
    ///    A solution set is:
    ///    (-1, 0, 1)
    ///    (-1, -1, 2)
    /// </summary>
    [TestFixture]
    public class Problem_0015_ThreeSum
    {
        [Test]
        public void RunTests()
        {
            var tests = GetTests();
            foreach(var test in tests)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var actualSolutionSets = ThreeSum(test.OriginalNumbers);
                stopwatch.Stop();
                Console.WriteLine("Time {0}ms for {1}", stopwatch.ElapsedMilliseconds, test);
                foreach(var expectedSolution in test.SolutionSets)
                {
                    var foundMatch = false;
                    foreach(var actualSolution in actualSolutionSets)
                    {
                        if (actualSolution[0] == expectedSolution[0] &&
                            actualSolution[1] == expectedSolution[1] &&
                            actualSolution[2] == expectedSolution[2])
                        {
                            foundMatch = true;
                            break;
                        }
                    }
                    Assert.That(foundMatch, Is.True, test.ToString() + " " + string.Join(",", expectedSolution));
                }

                Assert.That(actualSolutionSets.Count, Is.EqualTo(test.SolutionSets.Count), test.ToString());
            }
        }

        public IList<IList<int>> ThreeSum(int[] nums)
        {
            Array.Sort(nums);

            var solutionSets = new List<IList<int>>();

            for (var idx1 = 0; idx1 < nums.Length; ++idx1)
            {
                // If have already tried this digit as the first in a solution
                // then move on to the next digit
                if (idx1 > 0 && nums[idx1] == nums[idx1 - 1]) continue;

                var requiredSum = 0 - nums[idx1];
                var idx2 = idx1 + 1;
                var idx3 = nums.Length - 1;
                while (idx2 < idx3)
                {
                    // If a match for the required sum then add the solution
                    // otherwise move the index boundaries in towards finding a potential sum match
                    var remainderSum = (nums[idx2] + nums[idx3]);
                    if (remainderSum == requiredSum)
                    {
                        solutionSets.Add(new List<int> { nums[idx1], nums[idx2], nums[idx3] });

                        // Now move past re-using the same digits
                        idx2++;
                        while (idx2 < idx3 && nums[idx2] == nums[idx2 - 1])
                        {
                            idx2++;
                        }
                        idx3--;
                        while (idx2 < idx3 && nums[idx3] == nums[idx3 + 1])
                        {
                            idx3--;
                        }
                    }
                    else if (remainderSum < requiredSum)
                    {
                        idx2++;
                    }
                    else
                    {
                        idx3--;
                    }
                }
            }
            return solutionSets;
        }

        public IEnumerable<ThreeSumTest> GetTests()
        {
            return new[] 
            { 
                new ThreeSumTest { 
                    OriginalNumbers = new int[] {-1, 0, 1, 2, -1, -4},
                    SolutionSets = new List<IList<int>> {
                        new List<int> {-1, 0, 1},
                        new List<int> {-1, -1, 2}
                        }
                    }
            };
        }

        public class ThreeSumTest
        {
            public int[] OriginalNumbers { get; set; }
            public IList<IList<int>> SolutionSets { get; set; }

            public override string ToString()
            {
                return string.Join(",", OriginalNumbers);
            }
        }
    }
}
