using FluentAssertions;
using NUnit.Framework;

namespace Puzzles.LeetCode.Problems_0001_0100
{
    /// Given an array of integers, find two numbers such that they add up to a specific target number.
    ///
    /// The function twoSum should return indices of the two numbers such that they add up to the target, 
    /// where index1 must be less than index2. Please note that your returned answers (both index1 and index2) are not zero-based.
    ///
    /// You may assume that each input would have exactly one solution.
    ///
    /// Input: numbers={2, 7, 11, 15}, target=9
    /// Output: index1=1, index2=2
    [TestFixture]
    public class Problem_0001_TwoSum
    {
        [Test]
        public void RunTest()
        {
            var result = TwoSum(new[] {2, 7, 11, 15}, 9);

            result[0].Should().Be(1);
            result[1].Should().Be(2);
        }

        public int[] TwoSum(int[] nums, int target)
        {
            var result = new int[2];

            for (var i = 0; i < nums.Length; ++i)
            {
                for (var j = i + 1; j < nums.Length; ++j)
                {
                    var sum = nums[i] + nums[j];
                    if (sum != target) continue;

                    result[0] = i + 1;
                    result[1] = j + 1;
                    return result;
                }
            }

            return result;
        }
    }
}
