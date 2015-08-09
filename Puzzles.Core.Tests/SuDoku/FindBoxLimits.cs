using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Models.SuDoku;
using Puzzles.Core.SuDoku.Extensions;

namespace Puzzles.Core.Tests.SuDoku
{
    [TestFixture]
    public class FindBoxLimits
    {
        /// <summary>
        /// Given a row/column index, which 3x3 box of squares is it in?
        /// </summary>
        [Test]
        [TestCase(0, 0)]
        [TestCase(1, 0)]
        [TestCase(2, 0)]
        [TestCase(3, 3)]
        [TestCase(4, 3)]
        [TestCase(5, 3)]
        [TestCase(6, 6)]
        [TestCase(7, 6)]
        [TestCase(8, 6)]
        public void ConfirmBoxStartPosition(int idx, int expectedStart)
        {
            var grid = new Grid();
            var startPosition = grid.GetBoxStart(idx);
            startPosition.Should().Be(expectedStart);
        }

        [Test]
        [TestCase(0,2)]
        [TestCase(3,5)]
        [TestCase(6,8)]
        public void ConfirmBoxEndPosition(int startIdx, int expectedEndIdx)
        {
            var grid = new Grid();
            var endPosition = grid.GetBoxEnd(startIdx);
            endPosition.Should().Be(expectedEndIdx);
        }
    }
}
