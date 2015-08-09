using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.SuDoku;
using Puzzles.Core.SuDoku.Extensions;
using Puzzles.Core.SuDoku.Solvers;

namespace Puzzles.Core.Tests.SuDoku
{
    [TestFixture]
    public class SolveByUniqueRemainingPossibilityForSquare
    {
        private readonly ISolver solver = new UniqueRemainingPossibilityForSquareSolver();

        [Test]
        public void WhenTheGridIsEmpty_ThenNoSquaresAreSolved()
        {
            var grid = GridBuilder.BuildEmptyGrid();
            grid.Solve(solver);

            grid.IsSolved.Should().BeFalse();
            grid.UnsolvedSquareCount.Should().Be(81);
        }

        [Test]
        public void WhenNoSquaresHaveASingleRemainingPossibility_ThenNoSquaresAreSolved()
        {
            var grid = GridBuilder.BuildEmptyGrid();
            grid.Squares[0,0].SetDigit(1);
            grid.Tidy();
            
            grid.Solve(solver);

            grid.IsSolved.Should().BeFalse();
            grid.UnsolvedSquareCount.Should().Be(80);
        }

        [Test]
        public void WhenASquareHasASingleRemainingPossibility_ThenItIsSolved()
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

            grid.Solve(solver);

            grid.Squares[0, 8].IsSolved.Should().BeTrue();
            grid.Squares[0, 8].Digit.Should().Be(9);
            grid.Squares[0, 8].PossibleDigits.Count.Should().Be(0);
            grid.IsSolved.Should().BeFalse();
            grid.UnsolvedSquareCount.Should().Be(72);
        }
    }
}
