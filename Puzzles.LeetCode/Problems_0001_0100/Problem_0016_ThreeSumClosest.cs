using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.LeetCode.Problems_0001_0100
{
    /// <summary>
    /// Given an array S of n integers, find three integers in S 
    /// such that the sum is closest to a given number, target. 
    /// 
    /// Return the sum of the three integers. 
    /// 
    /// You may assume that each input would have exactly one solution.
    ///
    /// For example, given array S = {-1 2 1 -4}, and target = 1.
    ///
    /// The sum that is closest to the target is 2. (-1 + 2 + 1 = 2).
    /// </summary>
    [TestFixture]
    public class Problem_0016_ThreeSumClosest
    {
        [Test]
        [TestCase(new[] {  -1, 2, 1, -4}, 1, 2)]
        [TestCase(new[] {1,1,1,0}, -100, 2)]
        public void RunTests(int[] nums, int target, int expectedSum)
        {
            var closest = ThreeSumClosest(nums, target);
            Assert.That(closest, Is.EqualTo(expectedSum), string.Join(",", nums));
        }

        public int ThreeSumClosest(int[] nums, int target)
        {
            Array.Sort(nums);
            var bestSum = 0;
            var bestDiff = Int32.MaxValue;
            for(var idx1 = 0; idx1 < nums.Length; ++idx1)
            {
                // Don't bother re-trying a given number
                if (idx1 > 0 && nums[idx1] == nums[idx1 - 1]) continue;

                for(var idx2 = idx1+1; idx2 < nums.Length; ++idx2)
                {
                    for (var idx3 = idx2+1; idx3 < nums.Length; ++idx3)
                    {
                        var sum = nums[idx1] + nums[idx2] + nums[idx3];
                        if (sum == target) return target;

                        var diff = Math.Abs(sum - target);
                        if (diff < bestDiff)
                        {
                            bestSum = sum;
                            bestDiff = diff;
                        }
                    }
                }
            }

            return bestSum;
        }
    }
}
