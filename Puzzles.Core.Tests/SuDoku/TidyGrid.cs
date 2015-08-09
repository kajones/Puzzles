using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.SuDoku;
using Puzzles.Core.SuDoku.Extensions;

namespace Puzzles.Core.Tests.SuDoku
{
    [TestFixture]
    public class TidyGrid
    {
        [Test]
        public void WhenTheGridIsEmpty_ThenNoTidyingIsPossible()
        {
            var grid = GridBuilder.BuildEmptyGrid();

            grid.Tidy();

            grid.IsSolved.Should().BeFalse();

            foreach (var square in grid.Squares)
            {
                square.IsSolved.Should().BeFalse();
                square.PossibleDigits.Count.Should().Be(9);
                square.Digit.Should().Be(0);
            }
        }

        [Test]
        public void WhenASquareInTheTopLeftBoxIsSolved_ThenTheCorrespondingRowColumnAndBoxAreClearedOfThatDigit()
        {
            const int rowIdxToSet = 0;
            const int colIdxToSet = 2;
            const int digitToSet = 7;

            ConfirmRowColumnAndBoxAreClearedOfTheSetDigit(rowIdxToSet, colIdxToSet, digitToSet);
        }

        [Test]
        public void WhenASquareInTheMiddleRightBoxIsSolved_ThenTheCorrespondingRowColumnAndBoxAreClearedOfThatDigit()
        {
            const int rowIdxToSet = 4;
            const int colIdxToSet = 7;
            const int digitToSet = 6;

            ConfirmRowColumnAndBoxAreClearedOfTheSetDigit(rowIdxToSet, colIdxToSet, digitToSet);
        }

        [Test]
        public void WhenASquareInTheBottomMiddleBoxIsSolved_ThenTheCorrespondingRowColumnAndBoxAreClearedOfThatDigit()
        {
            const int rowIdxToSet = 8;
            const int colIdxToSet = 3;
            const int digitToSet = 5;

            ConfirmRowColumnAndBoxAreClearedOfTheSetDigit(rowIdxToSet, colIdxToSet, digitToSet);
        }

        private static void ConfirmRowColumnAndBoxAreClearedOfTheSetDigit(int rowIdxToSet, int colIdxToSet, int digitToSet)
        {
            var grid = GridBuilder.BuildEmptyGrid();
            grid.Squares[rowIdxToSet, colIdxToSet].SetDigit(digitToSet);

            grid.Tidy();

            grid.IsSolved.Should().BeFalse();

            for (var colIdx = 0; colIdx < 9; ++ colIdx)
            {
                grid.Squares[rowIdxToSet, colIdx].PossibleDigits.Should().NotContain(digitToSet);
            }

            for (var rowIdx = 0; rowIdx < 9; ++rowIdx)
            {
                grid.Squares[rowIdx, colIdxToSet].PossibleDigits.Should().NotContain(digitToSet);
            }

            var startOfBoxRow = grid.GetBoxStart(rowIdxToSet);
            var endOfBoxRow = grid.GetBoxEnd(startOfBoxRow);
            var startOfBoxColumn = grid.GetBoxStart(colIdxToSet);
            var endOfBoxColumn = grid.GetBoxEnd(startOfBoxColumn);

            for (var rowIdx = startOfBoxRow; rowIdx <= endOfBoxRow; ++rowIdx)
            {
                for (var colIdx = startOfBoxColumn; colIdx <= endOfBoxColumn; ++ colIdx)
                {
                    grid.Squares[rowIdx, colIdx].PossibleDigits.Should().NotContain(digitToSet);
                }
            }
        }
    }
}
