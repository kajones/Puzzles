using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.SuDoku;
using Puzzles.Core.SuDoku.Extensions;
using Puzzles.Core.SuDoku.Solvers;

namespace Puzzles.Core.Tests.SuDoku
{
    [TestFixture]
    public class SolveByUniquePossibilityInBox
    {
        private readonly ISolver solver = new UniquePossibilityInBoxSolver();

        [Test]
        public void WhenThereIsAnEmptyGrid_ThenNoSquaresAreSolved()
        {
            var grid = GridBuilder.BuildEmptyGrid();

            grid.Solve(solver);

            grid.IsSolved.Should().BeFalse();
            grid.UnsolvedSquareCount.Should().Be(81);
        }

        [Test]
        public void WhenNoBoxHasAUniquePossibility_ThenNoSquaresAreSolved()
        {
            var grid = GridBuilder.BuildEmptyGrid();
            grid.Squares[1,2].SetDigit(7);
            grid.Tidy();

            grid.Solve(solver);

            grid.IsSolved.Should().BeFalse();
            grid.UnsolvedSquareCount.Should().Be(80);
        }

        [Test]
        public void WhenTheTopLeftBoxHasAUniquePossibility_ThenTheSquareIsSolved()
        {
            var grid = GridBuilder.BuildEmptyGrid();
            grid.Squares[0, 0].SetDigit(9);
            grid.Squares[0, 1].SetDigit(8);
            grid.Squares[0, 2].SetDigit(7);
            grid.Squares[1, 0].SetDigit(6);
            grid.Squares[1, 1].SetDigit(5);
            grid.Squares[1, 2].SetDigit(4);
            grid.Squares[2, 0].SetDigit(3);
            grid.Squares[2, 1].SetDigit(2);
            grid.Tidy();
            // Leaving the final square with 1 as the only possibility

            grid.Solve(solver);

            grid.IsSolved.Should().BeFalse();
            grid.UnsolvedSquareCount.Should().Be(72);
        }

        [Test]
        public void WhenTheMiddleRightBoxHasAUniquePossibility_ThenTheSquareIsSolved()
        {
            var grid = GridBuilder.BuildEmptyGrid();
            grid.Squares[3, 6].SetDigit(9);
            grid.Squares[3, 7].SetDigit(8);
            grid.Squares[3, 8].SetDigit(7);
            grid.Squares[4, 6].SetDigit(6);
            grid.Squares[4, 7].SetDigit(5);
            grid.Squares[4, 8].SetDigit(4);
            grid.Squares[5, 6].SetDigit(3);
            grid.Squares[5, 7].SetDigit(2);
            grid.Tidy();
            // Leaving the final square with 1 as the only possibility

            grid.Solve(solver);

            grid.IsSolved.Should().BeFalse();
            grid.UnsolvedSquareCount.Should().Be(72);
        }

        [Test]
        public void WhenTheBottomMiddleBoxHasAUniquePossibility_ThenTheSquareIsSolved()
        {
            var grid = GridBuilder.BuildEmptyGrid();
            grid.Squares[6, 3].SetDigit(9);
            grid.Squares[6, 4].SetDigit(8);
            grid.Squares[6, 5].SetDigit(7);
            grid.Squares[7, 3].SetDigit(6);
            grid.Squares[7, 4].SetDigit(5);
            grid.Squares[7, 5].SetDigit(4);
            grid.Squares[8, 3].SetDigit(3);
            grid.Squares[8, 4].SetDigit(2);
            grid.Tidy();
            // Leaving the final square with 1 as the only possibility

            grid.Solve(solver);

            grid.IsSolved.Should().BeFalse();
            grid.UnsolvedSquareCount.Should().Be(72);
        }
    }
}
