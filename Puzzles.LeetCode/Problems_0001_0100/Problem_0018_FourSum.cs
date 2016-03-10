using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.LeetCode.Problems_0001_0100
{
    /// <summary>
    /// Given an array S of n integers, are there elements a, b, c, and d in S 
    /// such that a + b + c + d = target? 
    /// Find all unique quadruplets in the array which gives the sum of target.
    ///
    /// Note:
    /// Elements in a quadruplet (a,b,c,d) must be in non-descending order. (ie, a ≤ b ≤ c ≤ d)
    /// The solution set must not contain duplicate quadruplets.
    ///     For example, given array S = {1 0 -1 0 -2 2}, and target = 0.
    ///
    ///    A solution set is:
    ///    (-1,  0, 0, 1)
    ///    (-2, -1, 1, 2)
    ///    (-2,  0, 0, 2)
    /// </summary>
    [TestFixture]
    public class Problem_0018_FourSum
    {
        [Test]
        public void RunTests()
        {
            var tests = GetTests();
            foreach(var test in tests)
            {
                var actualSolutionSet = FourSum(test.OriginalNumbers, test.Target);
                foreach(var expectedSolution in test.SolutionSet)
                {
                    var foundMatch = false;
                    foreach(var actualSolution in actualSolutionSet)
                    {
                        if (actualSolution[0] == expectedSolution[0] &&
                            actualSolution[1] == expectedSolution[1] &&
                            actualSolution[2] == expectedSolution[2] &&
                            actualSolution[3] == expectedSolution[3])
                        {
                            foundMatch = true;
                            break;
                        }
                    }
                    Assert.That(foundMatch, Is.True, "Found " + string.Join(",", expectedSolution) + " when starting from " + string.Join(",", test.OriginalNumbers));
                }
                Assert.That(actualSolutionSet.Count, Is.EqualTo(test.SolutionSet.Count));
            }
        }

        public IList<IList<int>> FourSum(int[] nums, int target)
        {
            Array.Sort(nums);

            var solutionSets = new List<IList<int>>();

            for (var idx1 = 0; idx1 < nums.Length; ++idx1)
            {
                // Ignore repeated digits
                if (idx1 > 0 && nums[idx1] == nums[idx1 - 1]) continue;

                for(var idx2 = idx1 + 1; idx2 < nums.Length; ++idx2)
                {
                    // Ignore repeated digits (but ok to be same as first digit, just don't re-use the same digit in the second position)
                    if (idx2 > (idx1+1) && nums[idx2] == nums[idx2 - 1]) continue;

                    var requiredSum = target - nums[idx1] - nums[idx2];
                    var left = idx2 + 1;
                    var right = nums.Length - 1;
                    while (left < right)
                    {
                        var actualSum = nums[left] + nums[right];
                        if (actualSum == requiredSum)
                        {
                            solutionSets.Add(new List<int> { nums[idx1], nums[idx2], nums[left], nums[right] });
                            left++;
                            while(left < right && nums[left] == nums[left-1])
                            {
                                left++;
                            }
                            right--;
                            while(left < right && nums[right] == nums[right+1])
                            {
                                right--;
                            }
                        }
                        else if (actualSum < requiredSum)
                        {
                            left++;
                        }
                        else
                        {
                            right--;
                        }
                    }
                }
            }

            return solutionSets;
        }

        private IList<FourSumTest> GetTests()
        {
            return new List<FourSumTest> { 
                new FourSumTest { 
                    OriginalNumbers = new[] { 1, 0, -1, 0, -2, 2},
                    Target = 0,
                    SolutionSet = new List<IList<int>> {
                        new List<int> {-1,  0, 0, 1},
                        new List<int> {-2, -1, 1, 2},
                        new List<int> {-2,  0, 0, 2}
                    }
                },
                new FourSumTest {
                    OriginalNumbers = new [] {-3,-2,-1,0,0,1,2,3},
                    Target = 0,
                    SolutionSet = new List<IList<int>> {
                        new List<int> {-3,-2,2,3},
                        new List<int> {-3,-1,1,3},
                        new List<int> {-3,0,0,3},
                        new List<int> {-3,0,1,2},
                        new List<int> {-2,-1,0,3},
                        new List<int> {-2,-1,1,2},
                        new List<int> {-2,0,0,2},
                        new List<int> {-1,0,0,1}
                    }
                },
                new FourSumTest {
                    OriginalNumbers = new [] {0,0,0,0},
                    Target = 0,
                    SolutionSet = new List<IList<int>> {
                        new List<int> { 0,0,0,0}
                    }
                }
            };
        }
    }

    public class FourSumTest
    {
        public int[] OriginalNumbers { get; set;}
        public int Target { get; set; }

        public IList<IList<int>> SolutionSet { get; set; }

    }
}
