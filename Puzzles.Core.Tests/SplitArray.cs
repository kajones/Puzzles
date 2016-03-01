using NUnit.Framework;
using Puzzles.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;

namespace Puzzles.Core.Tests
{
    [TestFixture]
    public class SplitArray
    {
        [Test]
        [TestCase(new[] { 1, 2 }, new int[] { 1 })]
        [TestCase(new[] { 1, 2, 3 }, new[] { 1 })]
        [TestCase(new[] { 1, 2, 3, 4 }, new[] { 1, 2 })]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, new[] { 1, 2 })]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 1, 2, 3 })]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7 }, new[] { 1, 2, 3 })]
        public void CanExtractTheLeftHalfOfAnArray(int[] numbers, int[] expectedSplit)
        {
            var actualSplit = ArraySplitter.GetLeftArray(numbers);
            actualSplit.Should().BeEquivalentTo(expectedSplit, string.Join(",", numbers));
        }

        [Test]
        [TestCase(new[] { 1, 2 }, new int[] { 2})]
        [TestCase(new[] { 1, 2, 3 }, new[] { 3 })]
        [TestCase(new[] { 1, 2, 3, 4 }, new[] { 3,4 })]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, new[] { 4, 5 })]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 4,5, 6 })]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7 }, new[] { 5, 6, 7 })]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8 }, new[] { 5, 6, 7, 8 })]
        public void CanExtractTheRightHalfOfAnArray(int[] numbers, int[] expectedSplit)
        {
            var actualSplit = ArraySplitter.GetRightArray(numbers);
            actualSplit.Should().BeEquivalentTo(expectedSplit, string.Join(",", numbers));
        }
    }
}
