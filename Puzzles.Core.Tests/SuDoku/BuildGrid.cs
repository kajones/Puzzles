using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Models.SuDoku;
using Puzzles.Core.SuDoku;

namespace Puzzles.Core.Tests.SuDoku
{
    [TestFixture]
    public class BuildGrid
    {
        [Test]
        public void CanBuildEmptyGrid()
        {
            var grid = GridBuilder.BuildEmptyGrid();

            grid.Squares.Length.Should().Be(81);
            foreach (var square in grid.Squares)
            {
                square.IsSolved.Should().BeFalse();
                square.PossibleDigits.Count.Should().Be(9);
                square.Digit.Should().Be(0);
            }
        }

        [Test]
        public void CanBuildGridFromDefinition()
        {
            const string definition = @"Grid 00
100000000
020000000
003000000
000400000
000050000
000000006
000000070
000000800
000009000";

            var grid = GridBuilder.BuildFromDefinition(definition);
            grid.IsSolved.Should().BeFalse();

            ConfirmSquareDigit(grid,0,0,1);
            ConfirmSquareDigit(grid,1,1,2);
            ConfirmSquareDigit(grid,2,2,3);
            ConfirmSquareDigit(grid,3,3,4);
            ConfirmSquareDigit(grid,4,4,5);
            ConfirmSquareDigit(grid,5,8,6);
            ConfirmSquareDigit(grid,6,7,7);
            ConfirmSquareDigit(grid,7,6,8);
            ConfirmSquareDigit(grid,8,5,9);
        }

        private static void ConfirmSquareDigit(Grid grid, int rowIdx, int colIdx, int expectedDigit)
        {
            grid.Squares[rowIdx, colIdx].Digit.Should().Be(expectedDigit);
            grid.Squares[rowIdx, colIdx].IsSolved.Should().BeTrue();
            grid.Squares[rowIdx, colIdx].PossibleDigits.Count.Should().Be(0);
        }
    }
}
