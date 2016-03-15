using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.LeetCode.Problems_0001_0100
{
    /// <summary>
    /// Given an array and a value, remove all instances of that value in place and return the new length.
    ///
    /// Do not allocate extra space for another array, you must do this in place with constant memory.
    ///
    /// The order of elements can be changed. It doesn't matter what you leave beyond the new length.
    ///
    /// Example:
    /// Given input array nums = [3,2,2,3], val = 3
    ///
    /// Your function should return length = 2, with the first two elements of nums being 2.
    /// </summary>
    [TestFixture]
    public class Problem_0027_RemoveElement
    {
        [Test]
        public void RunExample()
        {
            var nums = new[] { 3, 2, 2, 3 };
            var result = RemoveElement(nums, 3);

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void RunAnotherExample()
        {
            var nums = new[] { 2, 2, 3 };
            var result = RemoveElement(nums, 2);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        [TestCase(new int[] { }, 1, 0, "Empty")]
        [TestCase(new int[] { 1 }, 1, 0, "Remove only element")]
        [TestCase(new int[] { 1 }, 2, 1, "Attempt to remove non-existing item from single entry")]
        [TestCase(new int[] { 1,2}, 1, 1, "Remove first item of two")]
        [TestCase(new int[] { 1,2},2,1, "Remove last item of two")]
        [TestCase(new int[] { 1,2,3}, 1,2, "Remove first item of three")]
        [TestCase(new int[] { 1,2,3},2,2,"Remove middle item of three")]
        [TestCase(new int[] { 1,2,3},3,2, "Remove last item of three")]
        [TestCase(new int[] { 1,2,3,3,4,2,3}, 5, 7, "Attempt to remove non-existing item")]
        [TestCase(new int[] { 1,2,3,3,2,4}, 3, 4, "Remove duplicated item")]
        [TestCase(new int[] { 1,2,2,3,2,1,4}, 3, 6, "Remove single instance item")]
        public void RunTests(int[] nums, int toRemove, int expectedLength, string message)
        {
            var result = RemoveElement(nums, toRemove);
            Assert.That(result, Is.EqualTo(expectedLength), message);
        }

        public int RemoveElement(int[] nums, int val)
        {
            if (nums == null || nums.Length == 0) return 0;

            var toKeepIdx = 0;
            var toCheckIdx = 0;

            while (toCheckIdx < nums.Length)
            {
                while(toKeepIdx < nums.Length &&
                      nums[toKeepIdx] != val)
                {
                    toKeepIdx++;
                }

                toCheckIdx = toKeepIdx + 1;
                while (toCheckIdx < nums.Length &&
                       nums[toCheckIdx] == val)
                {
                    toCheckIdx++;
                }
                if (toCheckIdx >= nums.Length) break;

                for (var toMoveIdx = toCheckIdx; toMoveIdx < nums.Length; ++toMoveIdx)
                {
                    var offset = toMoveIdx - toCheckIdx;
                    nums[toKeepIdx+offset] = nums[toMoveIdx];
                    nums[toMoveIdx] = val;
                }

                toKeepIdx = toKeepIdx + 1;
                toCheckIdx = toKeepIdx+1;
            }

            return toKeepIdx;
        }
    }
}
