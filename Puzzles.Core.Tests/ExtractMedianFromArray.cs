using NUnit.Framework;
using Puzzles.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.Core.Tests
{
    [TestFixture]
    public class ExtractMedianFromArray
    {
        [Test]
        [TestCase(new int[] { 1, 2}, 1.5)]
        [TestCase(new int[] { 2, 2}, 2)]
        [TestCase(new int[] { 1, 2, 3, 4}, 2.5)]
        [TestCase(new int[] { 1, 2, 2, 4}, 2)]
        public void ExtractMedianFromEvenLengthArray(int[] numbers, double expectedMedian)
        {
            var median = ArrayMedianHelper.GetMedian(numbers);
            Assert.That(median, Is.EqualTo(expectedMedian));
        }

        [Test]
        [TestCase(new int[] {  1}, 1)]
        [TestCase(new int[] { 1, 2, 3 }, 2)]
        [TestCase(new int[] { 1, 2, 2 }, 2)]
        [TestCase(new int[] { 1, 2, 3, 4, 5 }, 3)]
        [TestCase(new int[] { 1, 2, 4, 5, 6 }, 4)]
        [TestCase(new int[] { 1, 1, 1, 3, 4, 5, 6}, 3)]
        public void ExtractMedianFromOddLengthArray(int[] numbers, double expectedMedian)
        {
            var median = ArrayMedianHelper.GetMedian(numbers);
            Assert.That(median, Is.EqualTo(expectedMedian));
        }
    }
}
