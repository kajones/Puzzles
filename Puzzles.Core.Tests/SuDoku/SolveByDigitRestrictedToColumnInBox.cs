using System;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.SuDoku;
using Puzzles.Core.SuDoku.Extensions;
using Puzzles.Core.SuDoku.Solvers;
using Puzzles.Core.Tests.Extensions;

namespace Puzzles.Core.Tests.SuDoku
{
    [TestFixture]
    public class SolveByDigitRestrictedToColumnInBox
    {
        private const string exampleGrid = @"Grid
043980250
600425000
200001094
900004070
300608000
410209003
820500000
000000005
534890710
";

        private readonly ISolver solver = new DigitRestrictedToColumnInBoxSolver();

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            var grid = GridBuilder.BuildFromDefinition(exampleGrid);
            grid.Tidy();

            grid.ConfirmPossibilities(0, 0, new [] {1,7});
            grid.ConfirmPossibilities(0,5,new [] {6,7});
            grid.ConfirmPossibilities(0,8, new [] { 1,6,7});

            grid.ConfirmPossibilities(1,1,new [] { 7,8,9});
            grid.ConfirmPossibilities(1,2,new [] { 1,7,8,9});
            grid.ConfirmPossibilities(1,6,new [] { 1,3,8});
            grid.ConfirmPossibilities(1,7,new [] { 3,8});
            grid.ConfirmPossibilities(1,8,new [] { 1,7,8});

            grid.ConfirmPossibilities(2,1,new [] { 5,7,8});
            grid.ConfirmPossibilities(2,2,new [] { 5,7,8});
            grid.ConfirmPossibilities(2,3,new [] { 3,7});
            grid.ConfirmPossibilities(2,4,new [] { 3,6,7});
            grid.ConfirmPossibilities(2,6,new [] { 3,6,8});

            grid.ConfirmPossibilities(3,1,new [] { 5,6,8});
            grid.ConfirmPossibilities(3,2,new [] { 2,5,6,8});
            grid.ConfirmPossibilities(3,3,new [] { 1,3});
            grid.ConfirmPossibilities(3,4,new [] { 1,3,5});
            grid.ConfirmPossibilities(3,6,new [] { 1,5,6,8});
            grid.ConfirmPossibilities(3,8,new [] { 1,2,6,8});

            grid.ConfirmPossibilities(4,1,new [] { 5,7});
            grid.ConfirmPossibilities(4,2,new [] { 2,5,7});
            grid.ConfirmPossibilities(4,4,new [] { 1,5,7});
            grid.ConfirmPossibilities(4,6,new [] { 1,4,5,9});
            grid.ConfirmPossibilities(4,7,new [] { 2,4});
            grid.ConfirmPossibilities(4,8,new [] { 1,2,9});

            grid.ConfirmPossibilities(5,2,new [] { 5,6,7,8});
            grid.ConfirmPossibilities(5,4,new [] { 5,7});
            grid.ConfirmPossibilities(5,6,new [] { 5,6,8});
            grid.ConfirmPossibilities(5,7,new [] { 6,8});

            grid.ConfirmPossibilities(6,2,new [] { 1,6,7,9});
            grid.ConfirmPossibilities(6,4,new [] { 1,3,4,6,7});
            grid.ConfirmPossibilities(6,5,new [] { 3,6,7});
            grid.ConfirmPossibilities(6,6,new [] { 3,4,6,9});
            grid.ConfirmPossibilities(6,7,new [] { 3,4,6});
            grid.ConfirmPossibilities(6,8,new [] { 6,9});

            grid.ConfirmPossibilities(7,0,new [] { 1,7});
            grid.ConfirmPossibilities(7,1,new [] { 6,7,9});
            grid.ConfirmPossibilities(7,2,new [] { 1,6,7,9});
            grid.ConfirmPossibilities(7,3,new [] { 1,3,7});
            grid.ConfirmPossibilities(7,4,new [] { 1,3,4,6,7});
            grid.ConfirmPossibilities(7,5,new [] { 2,3,6,7});
            grid.ConfirmPossibilities(7,6,new [] { 3,4,6,8,9});
            grid.ConfirmPossibilities(7,7,new [] { 2,3,4,6,8});

            grid.ConfirmPossibilities(8,5,new [] { 2,6});
            grid.ConfirmPossibilities(8,8,new [] { 2,6});
        }

        [Test]
        public void WhenThereIsAnEmptyGrid_ThenNoSquaresAreSolved()
        {
            var grid = GridBuilder.BuildEmptyGrid();

            grid.Solve(solver);

            grid.IsSolved.Should().BeFalse();
            grid.UnsolvedSquareCount.Should().Be(81);
        }

        [Test]
        public void WhenThereIsADigitRestrictedToAGivenColumnWithinABox_ThenTheDigitIsRemovedFromTheColumnInOtherBoxes()
        {
            var grid = GridBuilder.BuildFromDefinition(exampleGrid);
            grid.Tidy();
            Console.WriteLine(grid.Summary());

            grid.Solve(solver);

            grid.ConfirmPossibilities(2,4,new [] { 3,6}, "Removed 7 from top box");

            grid.ConfirmPossibilities(3,4,new [] { 1,3,5});
            grid.ConfirmPossibilities(4,4,new [] { 1,5,7});
            grid.ConfirmPossibilities(5,4,new [] { 5,7});

            grid.ConfirmPossibilities(6,4,new [] { 1,3,4,6}, "Removed 7 from bottom box");
            grid.ConfirmPossibilities(7, 4, new[] { 1, 3, 4, 6 }, "Removed 7 from bottom box");
        }
    }
}
