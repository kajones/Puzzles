using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.LeetCode.Problems_0001_0100
{
    /// <summary>
    /// Given n non-negative integers a(1), a(2), ..., a(n), where each represents a point at coordinate (i, a(i)).
    /// () => subscript
    /// n vertical lines are drawn such that the two endpoints of line i is at (i, a(i)) and (i, 0). 
    /// Find two lines, which together with x-axis forms a container, such that the container contains the most water.
    ///
    /// Note: You may not slant the container.
    /// 
    /// ----
    /// 
    /// Idea is that you are given a set of heights at each position and you're trying to work out
    /// which combination of limits gets you the most water between those limits
    /// e.g.
    /// |
    /// |
    /// |   |
    /// |   |     |
    /// | | | | | |
    /// -----------
    /// </summary>
    [TestFixture]
    public class Problem_0011_ContainerWithMostWater
    {
        [Test]
        [TestCase(new int[] { }, 0)]
        [TestCase(new[] { 1, 2}, 1)]
        [TestCase(new[] { 1, 2, 3}, 2)]
        [TestCase(new[] { 1,2,3,2},4)]
        [TestCase(new[] { 1,2,3,2,1,1},5)]
        public void RunTests(int[] height, int expectedMaxArea)
        {
            var actualMaxArea = MaxArea(height);
            Assert.That(actualMaxArea, Is.EqualTo(expectedMaxArea));
        }

        public int MaxArea(int[] height)
        {
            var left = 0;
            var right = height.Length - 1;
            var maxArea = 0;

            while (left < right)
            {
                // Area in box is width (distance between limits) times the minimum height of the two limits
                // - water would overflow any higher...
                var currentArea = (right - left) * Math.Min(height[left], height[right]);
                maxArea = Math.Max(maxArea, currentArea);

                if (height[left] < height[right])
                    left++;
                else
                    right--;
            }

            return maxArea;
        }
    }
}
