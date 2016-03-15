using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.LeetCode.Problems_0001_0100
{
    /// <summary>
    /// Given a sorted array, remove the duplicates in place such that each element appear only once and return the new length.
    ///
    /// Do not allocate extra space for another array, you must do this in place with constant memory.
    ///
    /// For example,
    /// Given input array nums = [1,1,2],
    ///
    /// Your function should return length = 2, with the first two elements of nums being 1 and 2 respectively. 
    /// It doesn't matter what you leave beyond the new length.
    /// </summary>
    [TestFixture]
    public class Problem_0026_RemoveDuplicatesFromSortedArray
    {
        [Test]
        public void RunExample()
        {
            var test = new[] { 1, 1, 2 };
            var result = RemoveDuplicates(test);
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        [TestCase(new int[] { }, 0, "Empty")]
        [TestCase(new[] { 1}, 1, "Single item")]
        [TestCase(new[] { 1,1}, 1, "Duplicate single value")]
        [TestCase(new[] { 1,2}, 2, "Two distinct values")]
        [TestCase(new[] { 1,2,3}, 3, "Three distinct values")]
        [TestCase(new[] { 1,2,3,4},4, "Four distinct values")]
        [TestCase(new[] { 1,2,2,3,3,3,4,4,4,4}, 4, "Four values most duplicated")]
        [TestCase(new[] {  7,7,7}, 1, "One non-1 duplicated value")]
        [TestCase(new[] { 7,8,8,9}, 3, "Three values with duplication starting > 1")]
        [TestCase(new[] { 1,1,2,2,3,3,4}, 4, "No duplication of final value")]
        public void RunTests(int[] nums, int expectedLength, string message)
        {
            var result = RemoveDuplicates(nums);
            Assert.That(result, Is.EqualTo(expectedLength));
        }

        public int RemoveDuplicates(int[] nums)
        {
            if (nums.Length < 2)
                return nums.Length;

            int uniqueIndex = 1;

            for (int idx = 1; idx < nums.Length; idx++)
            {
                if (nums[idx - 1] < nums[idx])
                {
                    // Store the next unique number in the next unique slot
                    nums[uniqueIndex] = nums[idx];
                    uniqueIndex++;
                }
            }

            return uniqueIndex;
        }
    }
}
