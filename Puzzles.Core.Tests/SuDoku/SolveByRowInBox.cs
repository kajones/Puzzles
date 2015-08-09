using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.SuDoku;
using Puzzles.Core.SuDoku.Extensions;
using Puzzles.Core.SuDoku.Solvers;
using Puzzles.Core.Tests.Extensions;

namespace Puzzles.Core.Tests.SuDoku
{
    [TestFixture]
    public class SolveByRowInBox
    {
        private readonly ISolver solver = new RowInBoxSolver();

        [Test]
        public void WhenThereIsAnEmptyGrid_ThenNoSquaresAreSolved()
        {
            var grid = GridBuilder.BuildEmptyGrid();

            grid.Solve(solver);

            grid.IsSolved.Should().BeFalse();
            grid.UnsolvedSquareCount.Should().Be(81);
        }

        [Test]
        public void WhenThereIsNoRowInABoxPreventingADigitPlacement_ThenNoSquaresAreSolved()
        {
            var grid = GridBuilder.BuildEmptyGrid();
            grid.SetRowDigits(0, new[] { 1, 0, 0, 9, 2, 0, 0, 0, 0 });
            grid.SetRowDigits(1, new[] { 5, 2, 4, 0, 1, 7, 0, 0, 9 });
            grid.SetRowDigits(2, new[] { 0, 0, 0, 0, 0, 4, 2, 7, 0 });
            grid.SetRowDigits(3, new[] { 0, 5, 0, 0, 0, 8, 1, 0, 2 });
            grid.SetRowDigits(4, new[] { 0, 0, 0, 1, 0, 2, 0, 0, 0 });
            grid.SetRowDigits(5, new[] { 4, 1, 2, 7, 0, 0, 0, 9, 0 });
            grid.SetRowDigits(6, new[] { 0, 6, 0, 0, 0, 9, 0, 1, 0 });
            grid.SetRowDigits(7, new[] { 0, 0, 1, 0, 3, 6, 9, 4, 5 });
            grid.SetRowDigits(8, new[] { 0, 4, 0, 0, 7, 1, 0, 2, 6 });

            grid.Tidy();

            // Before "solving", confirm the possibilities that exist
            grid.ConfirmPossibilities(0, 1, new[] { 3, 7, 8 });
            grid.ConfirmPossibilities(0, 2, new[] { 3, 6, 7, 8 });
            grid.ConfirmPossibilities(0, 5, new[] { 3, 5 });
            grid.ConfirmPossibilities(0, 6, new[] { 3, 4, 5, 6, 8 });
            grid.ConfirmPossibilities(0, 7, new[] { 3, 5, 6, 8 });
            grid.ConfirmPossibilities(0, 8, new[] { 3, 4, 8 });

            grid.ConfirmPossibilities(2, 0, new[] { 3, 6, 8, 9 });
            grid.ConfirmPossibilities(2, 1, new[] { 3, 8, 9 });
            grid.ConfirmPossibilities(2, 2, new[] { 3, 6, 8, 9 });
            grid.ConfirmPossibilities(2, 3, new[] { 3, 5, 6, 8 });
            grid.ConfirmPossibilities(2, 4, new[] { 5, 6, 8 });
            grid.ConfirmPossibilities(2, 8,new [] { 1,3,8}, "Has not been solved so both top and bottom rows are possible for 5 in both boxes");

            grid.Solve(solver);

            grid.ConfirmPossibilities(0, 1, new[] { 3, 7, 8 });
            grid.ConfirmPossibilities(0, 2, new[] { 3, 6, 7, 8 });
            grid.ConfirmPossibilities(0, 5, new[] { 3, 5 });
            grid.ConfirmPossibilities(0, 6, new[] { 3, 4, 5, 6, 8 });
            grid.ConfirmPossibilities(0, 7, new[] { 3, 5, 6, 8 });
            grid.ConfirmPossibilities(0, 8, new[] { 3, 4, 8 });

            grid.ConfirmPossibilities(2, 0, new[] { 3, 6, 8, 9 });
            grid.ConfirmPossibilities(2, 1, new[] { 3, 8, 9 });
            grid.ConfirmPossibilities(2, 2, new[] { 3, 6, 8, 9 });
            grid.ConfirmPossibilities(2, 3, new[] { 3, 5, 6, 8 });
            grid.ConfirmPossibilities(2, 4, new[] { 5, 6, 8 });
            grid.ConfirmPossibilities(2, 8, new[] { 1, 3, 8 });
        }

        [Test]
        public void WhenThereIsARowThatPreventsADigitPlacement_ThenTheOtherBoxPossibilitiesCanBeRemoved()
        {
            var grid = GridBuilder.BuildEmptyGrid();
            grid.SetRowDigits(0, new[] { 1, 0, 0, 9, 2, 0, 0, 0, 0 });
            grid.SetRowDigits(1, new[] { 5, 2, 4, 0, 1, 7, 0, 0, 9 });
            grid.SetRowDigits(2, new[] { 0, 0, 0, 0, 0, 4, 2, 7, 1 });
            grid.SetRowDigits(3, new[] { 0, 5, 0, 0, 0, 8, 1, 0, 2 });
            grid.SetRowDigits(4, new[] { 0, 0, 0, 1, 0, 2, 0, 0, 0 });
            grid.SetRowDigits(5, new[] { 4, 1, 2, 7, 0, 0, 0, 9, 0 });
            grid.SetRowDigits(6, new[] { 0, 6, 0, 0, 0, 9, 0, 1, 0 });
            grid.SetRowDigits(7, new[] { 0, 0, 1, 0, 3, 6, 9, 4, 5 });
            grid.SetRowDigits(8, new[] { 0, 4, 0, 0, 7, 1, 0, 2, 6 });

            grid.Tidy();

            // Before "solving", confirm the possibilities that exist
            grid.ConfirmPossibilities(0, 1, new[] { 3, 7, 8 });
            grid.ConfirmPossibilities(0, 2, new[] { 3, 6, 7, 8 });
            grid.ConfirmPossibilities(0, 5, new[] { 3, 5 }, "Currently is possible for 5");
            grid.ConfirmPossibilities(0, 6, new[] { 3, 4, 5, 6, 8 });
            grid.ConfirmPossibilities(0, 7, new[] { 3, 5, 6, 8 });
            grid.ConfirmPossibilities(0, 8, new[] { 3, 4, 8 });

            grid.ConfirmPossibilities(2,0, new [] { 3,6,8,9});
            grid.ConfirmPossibilities(2,1,new [] { 3,8,9});
            grid.ConfirmPossibilities(2,2,new [] { 3,6,8,9});
            grid.ConfirmPossibilities(2,3,new [] { 3,5,6,8});
            grid.ConfirmPossibilities(2,4,new [] { 5,6,8});

            grid.Solve(solver);

            // Now that the grid has been "solved" confirm that the top row is no longer an option for digit 5 for the middle box

            grid.ConfirmPossibilities(0, 1, new[] { 3, 7, 8 });
            grid.ConfirmPossibilities(0, 2, new[] { 3, 6, 7, 8 });
            grid.ConfirmPossibilities(0, 5, new[] { 3 }, "Removed 5");
            grid.ConfirmPossibilities(0, 6, new[] { 3, 4, 5, 6, 8 });
            grid.ConfirmPossibilities(0, 7, new[] { 3, 5, 6, 8 });
            grid.ConfirmPossibilities(0, 8, new[] { 3, 4, 8 });

            grid.ConfirmPossibilities(2, 0, new[] { 3, 6, 8, 9 });
            grid.ConfirmPossibilities(2, 1, new[] { 3, 8, 9 });
            grid.ConfirmPossibilities(2, 2, new[] { 3, 6, 8, 9 });
            grid.ConfirmPossibilities(2, 3, new[] { 3, 5, 6, 8 });
            grid.ConfirmPossibilities(2, 4, new[] { 5, 6, 8 });
        }

    }
}
