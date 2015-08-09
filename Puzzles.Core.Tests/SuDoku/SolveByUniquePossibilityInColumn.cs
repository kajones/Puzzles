using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.SuDoku;
using Puzzles.Core.SuDoku.Extensions;
using Puzzles.Core.SuDoku.Solvers;

namespace Puzzles.Core.Tests.SuDoku
{
    [TestFixture]
    public class SolveByUniquePossibilityInColumn
    {
        private readonly ISolver solver = new UniquePossibilityInColumnSolver();

        [Test]
        public void WhenThereIsAnEmptyGrid_ThenNoSquaresCanBeFilled()
        {
            var grid = GridBuilder.BuildEmptyGrid();

            grid.Solve(solver);

            grid.IsSolved.Should().BeFalse();
            grid.UnsolvedSquareCount.Should().Be(81);
        }

        [Test]
        public void WhenNoColumnsHaveAUniquePossibility_ThenNoSquaresCanBeFilled()
        {
            var grid = GridBuilder.BuildEmptyGrid();
            grid.Squares[0, 3].SetDigit(1);
            grid.Squares[1, 3].SetDigit(2);
            grid.Squares[2, 3].SetDigit(3);
            grid.Squares[3, 3].SetDigit(4);
            grid.Squares[4, 3].SetDigit(5);
            grid.Squares[5, 3].SetDigit(6);
            grid.Squares[6, 3].SetDigit(7);
            grid.Tidy();
            // Leaving the final two rows to fight for 8 & 9

            grid.Solve(solver);

            grid.IsSolved.Should().BeFalse();
            grid.UnsolvedSquareCount.Should().Be(74);
        }

        [Test]
        public void WhenASquareInAColumnHasAUniquePossibility_ThenItsValueIsSet()
        {
            var grid = GridBuilder.BuildEmptyGrid();
            grid.Squares[0, 3].SetDigit(1);
            grid.Squares[1, 3].SetDigit(2);
            grid.Squares[2, 3].SetDigit(3);
            grid.Squares[3, 3].SetDigit(4);
            grid.Squares[4, 3].SetDigit(5);
            grid.Squares[5, 3].SetDigit(6);
            grid.Squares[6, 3].SetDigit(7);
            grid.Squares[7, 3].SetDigit(8);
            grid.Tidy();
            // Leaving the final row with 9 as the only possibility

            grid.Solve(solver);

            grid.IsSolved.Should().BeFalse();
            grid.UnsolvedSquareCount.Should().Be(72);
        }

    }
}
