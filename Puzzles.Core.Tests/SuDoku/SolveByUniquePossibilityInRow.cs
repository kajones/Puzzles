using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.SuDoku;
using Puzzles.Core.SuDoku.Extensions;
using Puzzles.Core.SuDoku.Solvers;

namespace Puzzles.Core.Tests.SuDoku
{
    [TestFixture]
    public class SolveByUniquePossibilityInRow
    {
        private readonly ISolver solver = new UniquePossibilityInRowSolver();

        [Test]
        public void WhenThereIsAnEmptyGrid_ThenNoSquaresCanBeFilled()
        {
            var grid = GridBuilder.BuildEmptyGrid();

            grid.Solve(solver);

            grid.IsSolved.Should().BeFalse();
            grid.UnsolvedSquareCount.Should().Be(81);
        }

        [Test]
        public void WhenNoRowsHaveAUniquePossibility_ThenNoSquaresCanBeFilled()
        {
            var grid = GridBuilder.BuildEmptyGrid();
            grid.Squares[0, 0].SetDigit(1);
            grid.Squares[0, 1].SetDigit(2);
            grid.Squares[0, 2].SetDigit(3);
            grid.Squares[0, 3].SetDigit(4);
            grid.Squares[0, 4].SetDigit(5);
            grid.Squares[0, 5].SetDigit(6);
            grid.Squares[0, 6].SetDigit(7);
            grid.Tidy();
            // Leaving the final two columns to fight for 8 & 9

            grid.Solve(solver);

            grid.IsSolved.Should().BeFalse();
            grid.UnsolvedSquareCount.Should().Be(74);
        }

        [Test]
        public void WhenASquareInARowHasAUniquePossibility_ThenItsValueIsSet()
        {
            var grid = GridBuilder.BuildEmptyGrid();
            grid.Squares[0, 0].SetDigit(1);
            grid.Squares[0, 1].SetDigit(2);
            grid.Squares[0, 2].SetDigit(3);
            grid.Squares[0, 3].SetDigit(4);
            grid.Squares[0, 4].SetDigit(5);
            grid.Squares[0, 5].SetDigit(6);
            grid.Squares[0, 6].SetDigit(7);
            grid.Squares[0, 7].SetDigit(8);
            grid.Tidy();
            // Leaving the final column with 9 as the only possibility

            grid.Solve(solver);

            grid.IsSolved.Should().BeFalse();
            grid.UnsolvedSquareCount.Should().Be(72);
        }
    }
}
