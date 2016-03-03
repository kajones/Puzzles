using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Puzzles.Core.Helpers;

namespace Puzzles.LeetCode.Problems_0001_0100
{
    /// <summary>
    /// There are two sorted arrays nums1 and nums2 of size m and n respectively. 
    /// Find the median of the two sorted arrays. 
    /// The overall run time complexity should be O(log (m+n)).
    /// </summary>
    [TestFixture]
    public class Problem_0004_MedianOfTwoSortedArrays
    {
        [Test]
        [TestCase(new[] { 1, 2, 3 }, new[] { 1, 2, 3 }, 2, "[1,2,3] [1,2,3]")]
        [TestCase(new[] { 1, 2, 3 }, new[] { 1, 2, 3, 4 }, 2, "[1,2,3] [1,2,3,4]")]
        [TestCase(new[] { 1, 2, 3 }, new[] { 4, 5, 6, 7 }, 4, "[1,2,3] [4,5,6,7]")]
        [TestCase(new[] { 1, 2, 3 }, new[] { 4, 5, 6 }, 3.5, "[1,2,3] [4,5,6]")]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, new[] { 2, 3, 4, 5, 6 }, 3.5, "[1,2,3,4,5] [2,3,4,5,6]")]
        [TestCase(new[] { 3 }, new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 4.5, "[3] [1,2,3,4,5,6,7,8,9]")]
        [TestCase(new[] { 1, 2, 6 }, new[] { 3, 4, 5 }, 3.5, "[1,2,6] [3,4,5]")]
        [TestCase(new[] { 3, 4, 5 }, new[] { 1, 2, 6, 7, 8 }, 4.5, "[3,4,5] [1,2,6,7,8]")]
        [TestCase(new[] { 1, 3 }, new[] { 2, 4, 5 }, 3, "[1,3] [2,4,5]")]
        [TestCase(new[] { 1, 2 }, new[] { 3, 4, 5, 6 }, 3.5, "[1,2] [3,4,5,6]")]
        [TestCase(new[] { 1, 2 }, new[] { 3,4}, 2.5, "[1,2] [3,4]")]
        public void CalculateMedian(int[] nums1, int[] nums2, double expectedMedian, string message)
        {
            var actualMedian = FindMedianSortedArrays(nums1, nums2);
            Assert.That(actualMedian, Is.EqualTo(expectedMedian), message + " " + GetTestSummary(nums1, nums2, expectedMedian));
        }

        [Test]
        [TestCase(new[] { 1, 2, 3, 4 }, 3, 3)]
        [TestCase(new[] { 1, 2, 3, 4 }, 4, 3)]
        [TestCase(new[] { 1, 2, 3, 4 }, 5, 3)]
        [TestCase(new[] { 1, 2, 5, 6 }, 4, 4)]
        [TestCase(new[] { 1, 2, 5, 6 }, 5, 5)]
        [TestCase(new[] { 1, 2, 5, 6 }, 6, 5)]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, 4, 3.5)]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, 5, 3.5)]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, 3, 3)]
        [TestCase(new[] { 1,2,3,4}, 2, 2)]
        [TestCase(new[] { 1,2,3,4}, 1, 2)]
        [TestCase(new[] { 1,2,3,4}, 0, 2)]
        [TestCase(new[] { 1,2,3,4,5}, 2, 2.5)]
        [TestCase(new[] { 1,2,3,4,5}, 1, 2.5)]
        [TestCase(new[] { 1}, 1, 1)]
        [TestCase(new[] { 1}, 2, 1.5)]
        [TestCase(new[] { 1}, 3, 2)]
        [TestCase(new[] { 1}, 0, 0.5)]
        [TestCase(new[] { 1,2}, 0, 1)]
        [TestCase(new[] { 1,2}, 1, 1)]
        [TestCase(new[] { 1,2},2, 2)]
        [TestCase(new[] { 1,2}, 3, 2)]
        public void ConfirmMedianFromArrayAndValue(int[] nums, int value, double expectedMedian)
        {
            var median = Problem_0004_MedianOfTwoSortedArrays.GetMedianFromArrayAndValue(nums, value);
            Assert.That(median, Is.EqualTo(expectedMedian));
        }

        private static string GetTestSummary(int[] nums1, int[] nums2, double expectedMedian)
        {
            return string.Format("Nums1:{0}; Nums2:{1} -> {2}", string.Join(",", nums1), string.Join(",", nums2), expectedMedian);
        }

        public double FindMedianSortedArraysExample(int[] nums1, int[] nums2)
        {
            int len1 = nums1.Length;
            int len2 = nums2.Length;
            int totalLength = len1 + len2;

            if ((totalLength) % 2 == 1) 
                return FindMedian(nums1, 0, len1 - 1, nums2, 0, len2 - 1, (totalLength) / 2 + 1);

            int first = FindMedian(nums1, 0, len1 - 1, nums2, 0, len2 - 1, (totalLength) / 2);
            int second = FindMedian(nums1, 0, len1 - 1, nums2, 0, len2 - 1, (totalLength) / 2 + 1);
            return (Convert.ToDouble(first) + Convert.ToDouble(second)) / 2;
        }

        public int FindMedian(int[] arrayA, int leftA, int rightA, int[] arrayB, int leftB, int rightB, int overallMidPoint)
        {
            if (rightA < leftA) return arrayB[leftB + overallMidPoint - 1];
            if (rightB < leftB) return arrayA[leftA + overallMidPoint - 1];

            while (leftA <= rightA && leftB <= rightB)
            {
                int middleA = leftA + (rightA - leftA) / 2;
                int middleB = leftB + (rightB - leftB) / 2;
                if (arrayA[middleA] <= arrayB[middleB])
                {
                    if (overallMidPoint <= (middleA - leftA + 1 + middleB - leftB))
                    {
                        rightB = middleB - 1; 
                        continue;
                    }
                    else
                    {
                        overallMidPoint -= (middleA - leftA + 1); leftA = middleA + 1;
                    }
                }
                else
                {
                    if (overallMidPoint <= (middleA - leftA + 1 + middleB - leftB))
                    {
                        rightA = middleA - 1; continue;
                    }
                    else
                    {
                        overallMidPoint -= (middleB - leftB + 1); leftB = middleB + 1;
                    }
                }
            }

            return leftA > rightA 
                ? arrayB[leftB + overallMidPoint - 1] 
                : arrayA[leftA + overallMidPoint - 1];
        }

        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            if (nums1.Length == 0) return ArrayMedianHelper.GetMedian(nums2);
            if (nums2.Length == 0) return ArrayMedianHelper.GetMedian(nums1);

            if (nums1.Length == 1) return GetMedianFromArrayAndValue(nums2, nums1[0]);
            if (nums2.Length == 1) return GetMedianFromArrayAndValue(nums1, nums2[0]);

            if (nums1.Length == 2) return GetMedianFromArrayAndPair(nums2, nums1);
            if (nums2.Length == 2) return GetMedianFromArrayAndPair(nums1, nums2);

            var medianNums1 = ArrayMedianHelper.GetMedian(nums1);
            var medianNums2 = ArrayMedianHelper.GetMedian(nums2);
            if (medianNums1 == medianNums2) return medianNums1;

            // Where one median is less than the other you can discard numbers to the left of that
            // and numbers to the right of the other, knowing that the overall median will be
            // in the upper part of the first or the lower part of the second
            // However since searching for a median you can only discard the minimum count of numbers to keep the balance
            // remaining from those two discard "piles" e.g. if 2 from the first and 3 from the second can only discard 2
            var maxToDiscardFrom1 = ((double)nums1.Length / 2) - 1;
            var maxToDiscardFrom2 = ((double)nums2.Length / 2) - 1;
            double maxToDiscard = Math.Min(maxToDiscardFrom1, maxToDiscardFrom2);
            int maxToDiscardCount = Convert.ToInt32(Math.Floor(maxToDiscard));
            if (maxToDiscardCount < 1)
                maxToDiscardCount = 1;

            int[] reducedNums1;
            int[] reducedNums2;

            if (medianNums1 < medianNums2)
            {
                reducedNums1 = GetLeftDiscarding(nums1, maxToDiscardCount);
                reducedNums2 = GetRightDiscarding(nums2, maxToDiscardCount);
            }
            else
            {
                reducedNums1 = GetRightDiscarding(nums1, maxToDiscardCount);
                reducedNums2 = GetLeftDiscarding(nums2, maxToDiscardCount);
            }

            return FindMedianSortedArrays(reducedNums1, reducedNums2);
        }

        private static int[] GetLeftDiscarding(int[] source, int toDiscard)
        {
            var reducedLength = source.Length - toDiscard;
            var destination = new int[reducedLength];
            Array.Copy(source, toDiscard, destination, 0, reducedLength);
            return destination;
        }

        private static int[] GetRightDiscarding(int[] source, int toDiscard)
        {
            var reducedLength = source.Length - toDiscard;
            var destination = new int[reducedLength];
            Array.Copy(source, 0, destination, 0, reducedLength);
            return destination;
        }

        private static double GetMedianFromArrayAndValue(int[] nums, int value)
        {
            var median = ArrayMedianHelper.GetMedian(nums);
            if (median == value) return median;

            var isEvenLengthArray = nums.Length % 2 == 0;
            return isEvenLengthArray
                ? GetMedianFromEvenLengthArrayAndValue(nums, value, median)
                : GetMedianFromOddLengthArrayAndValue(nums, value, median);
        }

        private static double GetMedianFromEvenLengthArrayAndValue(int[] nums, int value, double median)
        {
            if (median < value)
            {
                // The median was the mean of the middle two values
                // So look at the right middle one - if its less than the value
                // then its the combined median otherwise the value is the combined median
                var midRightIdx = Math.Ceiling((double)nums.Length / 2);
                var midRightValue = nums[(int)midRightIdx];
                return midRightValue < value ? midRightValue : value;
            }

            // Similarly with the left middle value
            var midLeftIdx = Math.Floor((double)nums.Length / 2)-1;
            var midLeftValue = nums[(int)midLeftIdx];
            return midLeftValue > value ? midLeftValue : value;
        }

        private static double GetMedianFromOddLengthArrayAndValue(int[] nums, int value, double median)
        {
            if (nums.Length == 1)
            {
                return (nums[0] + (double)value) / 2;
            }

            if (median < value)
            {
                // The middle value in an odd-length array is the median
                // So the revised median will either be the mean of that plus the number to the right in the array
                // or the mean of the middle value plus the value to add
                var idxToRightOfMiddle = Math.Ceiling((double)nums.Length / 2);
                var valueToRightOfMiddle = nums[(int)idxToRightOfMiddle];
                var valueToAddFromRight = (valueToRightOfMiddle > value) ? value : valueToRightOfMiddle;
                return (median + valueToAddFromRight) / 2;
            }

            // Similarly with the value to the left of the middle
            var idxToLeftOfMiddle = Math.Floor((double)nums.Length / 2) - 1;
            var valueToLeftOfMiddle = nums[(int)idxToLeftOfMiddle];
            var valueToAddFromLeft = (valueToLeftOfMiddle > value) ? valueToLeftOfMiddle : value;
            return (median + valueToAddFromLeft) / 2;
        }

        public double GetMedianFromArrayAndPair(int[] array, int[] pair)
        {
            var isEvenArray = array.Length % 2 == 0;

            if (isEvenArray)
                return GetMedianFromEvenLengthArrayAndPair(array, pair);

            return GetMedianFromOddLengthArrayAndPair(array, pair);
        }

        private double GetMedianFromEvenLengthArrayAndPair(int[] array, int[] pair)
        {
            if (array.Length == 2)
            {
                return ((double)Math.Max(array[0], pair[0]) + Math.Min(array[1], pair[1])) / 2;
            }

            var rightMidPoint = array.Length / 2;
            var leftMidPoint = rightMidPoint - 1;

            var leftOfPair = pair[0];
            var rightOfPair = pair[1];

            if (leftOfPair > array[rightMidPoint])
            {
                // Order is ... LMP RMP ... LoP RoP ...
                var arrayOneRight = array[rightMidPoint + 1];
                if (arrayOneRight < leftOfPair)
                    return ((double)arrayOneRight + array[rightMidPoint]) / 2;
                return ((double)leftOfPair + array[rightMidPoint]) / 2;
            }
            if (rightOfPair < array[leftMidPoint])
            {
                // Order is ... LoP RoP ... LMP RMP ...
                var arrayOneLeft = array[leftMidPoint - 1];
                if (arrayOneLeft > rightOfPair)
                    return ((double)arrayOneLeft + array[leftMidPoint]) / 2;
                return ((double)rightOfPair + array[leftMidPoint]) / 2;
            }

            var leftValue = Math.Max(leftOfPair, array[leftMidPoint]);
            var rightValue = Math.Min(rightOfPair, array[rightMidPoint]);

            return ((double)leftValue + rightValue) / 2;
        }

        private double GetMedianFromOddLengthArrayAndPair(int[] array, int[] pair)
        {
            var midPoint = array.Length / 2;

            var leftOfPair = pair[0];
            var rightOfPair = pair[1];
            
            if (leftOfPair > array[midPoint])
            {
                // Both of pair above the mid
                var arrayOneRight = array[midPoint + 1];
                return (arrayOneRight < leftOfPair) ? arrayOneRight : leftOfPair;
            }
            else if (rightOfPair < array[midPoint])
            {
                // Both of pair below the mid
                var arrayOneLeft = array[midPoint - 1];
                return (arrayOneLeft > rightOfPair) ? arrayOneLeft : rightOfPair;
            }
            return array[midPoint];
        }
    }
}
