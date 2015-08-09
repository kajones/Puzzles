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
    public class SolveByXWingByColumn
    {
        private readonly ISolver solver = new XWingByColumnSolver();

        [Test]
        public void WhenThereIsAnEmptyGrid_ThenNoSquaresAreSolved()
        {
            var grid = GridBuilder.BuildEmptyGrid();

            grid.Solve(solver);

            grid.IsSolved.Should().BeFalse();
            grid.UnsolvedSquareCount.Should().Be(81);
        }

        [Test]
        public void WhenTheRectangleAlsoHasAnAdditionalPossibilityForTheDigit_ThenNoSquaresAreSolved()
        {
            var grid = GridBuilder.BuildEmptyGrid();
            grid.Squares[1, 5].SetDigit(7);
            grid.Squares[2, 7].SetDigit(7);
            grid.Squares[5, 3].SetDigit(7);
            grid.Squares[8, 4].SetDigit(7);
            grid.Tidy();

            grid.Squares[3, 6].PossibleDigits.Should().Contain(7, "Mid Right Row 1 Col 1");
            grid.Squares[4, 6].PossibleDigits.Should().Contain(7, "Mid Right Row 2 Col 1");
            grid.Squares[3, 8].PossibleDigits.Should().Contain(7, "Mid Right Row 1 Col 3");
            grid.Squares[4, 8].PossibleDigits.Should().Contain(7, "Mid Right Row 2 Col 3");

            grid.Squares[6, 6].PossibleDigits.Should().Contain(7, "Bottom Right Row 1 Col 1");
            grid.Squares[6, 8].PossibleDigits.Should().Contain(7, "Bottom Right Row 1 Col 3");

            grid.Solve(solver);

            grid.IsSolved.Should().BeFalse();
            grid.UnsolvedSquareCount.Should().Be(77);
        }

        [Test]
        public void WhenThereIsAnXWing_ThenTheOtherPossibilitiesInTheTopAndBottomRowsAreRemoved()
        {
            var grid = GridBuilder.BuildEmptyGrid();
            grid.SetRowDigits(0, new [] { 1,0,0,9,2,0,0,0,0});
            grid.SetRowDigits(1, new [] { 5,2,4,0,1,7,0,0,9});
            grid.SetRowDigits(2, new [] { 0,0,0,0,0,4,2,7,1});
            grid.SetRowDigits(3, new [] { 0,5,0,0,0,8,1,0,2});
            grid.SetRowDigits(4, new [] { 0,0,0,1,0,2,0,0,0});
            grid.SetRowDigits(5, new [] { 4,1,2,7,0,0,0,9,0});
            grid.SetRowDigits(6, new [] { 0,6,0,0,0,9,0,1,0});
            grid.SetRowDigits(7, new [] { 0,0,1,0,3,6,9,4,5});
            grid.SetRowDigits(8, new [] { 0,4,0,0,7,1,0,2,6});

            grid.Tidy();

            // Before "solving", confirm the possibilities that exist
            grid.ConfirmPossibilities(0, 1, new[] { 3, 7, 8 });
            grid.ConfirmPossibilities(0, 2, new[] { 3, 6, 7, 8 });
            grid.ConfirmPossibilities(0, 5, new[] { 3, 5 });
            grid.ConfirmPossibilities(0, 6, new[] { 3, 4, 5, 6, 8 }, "XWing 4");
            grid.ConfirmPossibilities(0, 7, new[] { 3, 5, 6, 8 });
            grid.ConfirmPossibilities(0, 8, new[] { 3, 4, 8 }, "XWing 4");

            grid.ConfirmPossibilities(4, 0, new[] { 3, 6, 7, 8, 9 });
            grid.ConfirmPossibilities(4, 1, new[] { 3, 7, 8, 9 });
            grid.ConfirmPossibilities(4, 2, new[] { 3, 6, 7, 8, 9 });
            grid.ConfirmPossibilities(4, 4, new[] { 4, 5, 6, 9 });
            grid.ConfirmPossibilities(4, 6, new[] { 3, 4, 5, 6, 7, 8 }, "XWing 4 and 7");
            grid.ConfirmPossibilities(4, 7, new[] { 3, 5, 6, 8 });
            grid.ConfirmPossibilities(4, 8, new[] { 3, 4, 7, 8 }, "XWing 4 and 7");

            grid.ConfirmPossibilities(6, 0, new[] { 2, 3, 7, 8 }, "XWing 2");
            grid.ConfirmPossibilities(6, 2, new[] { 3, 5, 7, 8 });
            grid.ConfirmPossibilities(6, 3, new[] { 2, 4, 5, 8 }, "XWing 2");
            grid.ConfirmPossibilities(6, 4, new[] { 4, 5, 8 });
            grid.ConfirmPossibilities(6, 6, new[] { 3, 7, 8 }, "XWing 7");
            grid.ConfirmPossibilities(6, 8, new[] { 3, 7, 8 }, "XWing 7");

            grid.ConfirmPossibilities(7, 0, new[] { 2,7,8}, "XWing 2");
            grid.ConfirmPossibilities(7, 1, new [] { 7,8});
            grid.ConfirmPossibilities(7,3,new [] { 2,8}, "XWing 2");

            grid.Solve(solver);

            
            Console.WriteLine(grid.Summary());

            // Prove no additional squares solved
            grid.ConfirmRowDigits(0, new[] { 1, 0, 0, 9, 2, 0, 0, 0, 0 });
            grid.ConfirmRowDigits(1, new[] { 5, 2, 4, 0, 1, 7, 0, 0, 9 });
            grid.ConfirmRowDigits(2, new[] { 0, 0, 0, 0, 0, 4, 2, 7, 1 });
            grid.ConfirmRowDigits(3, new[] { 0, 5, 0, 0, 0, 8, 1, 0, 2 });
            grid.ConfirmRowDigits(4, new[] { 0, 0, 0, 1, 0, 2, 0, 0, 0 });
            grid.ConfirmRowDigits(5, new[] { 4, 1, 2, 7, 0, 0, 0, 9, 0 });
            grid.ConfirmRowDigits(6, new[] { 0, 6, 0, 0, 0, 9, 0, 1, 0 });
            grid.ConfirmRowDigits(7, new[] { 0, 0, 1, 0, 3, 6, 9, 4, 5 });
            grid.ConfirmRowDigits(8, new[] { 0, 4, 0, 0, 7, 1, 0, 2, 6 });

            // Confirm remaining possibilities for each square in the key rows
            grid.ConfirmPossibilities(0, 1, new[] {3, 7, 8});
            grid.ConfirmPossibilities(0, 2, new [] { 3,6,7,8});
            grid.ConfirmPossibilities(0,5,new [] { 3,5});
            grid.ConfirmPossibilities(0,6, new [] { 3,4,5,6,8}, "XWing 4");
            grid.ConfirmPossibilities(0,7,new [] { 3,5,6,8});
            grid.ConfirmPossibilities(0,8, new [] { 3,4,8}, "XWing 4");

            grid.ConfirmPossibilities(4,0, new [] { 3,6,8,9}, "Removed 7");
            grid.ConfirmPossibilities(4,1, new [] { 3,8,9}, "Removed 7");
            grid.ConfirmPossibilities(4,2,new [] { 3,6,8,9}, "Removed 7");
            grid.ConfirmPossibilities(4,4, new [] { 5,6,9}, "Removed 4");
            grid.ConfirmPossibilities(4,6,new [] { 3,4,5,6,7,8}, "XWing 4 and 7");
            grid.ConfirmPossibilities(4,7, new [] { 3,5,6,8});
            grid.ConfirmPossibilities(4,8, new [] { 3,4,7,8}, "XWing 4 and 7");

            grid.ConfirmPossibilities(6,0, new [] { 2,3,8}, "Removed 7, XWing 2");
            grid.ConfirmPossibilities(6,2,new [] { 3,5,8}, "Removed 7");
            grid.ConfirmPossibilities(6, 3, new[] { 2, 4, 5, 8 }, "XWing 2");
            grid.ConfirmPossibilities(6,4, new [] { 4,5,8});
            grid.ConfirmPossibilities(6,6,new [] { 3,7,8}, "XWing 7");
            grid.ConfirmPossibilities(6,8,new [] { 3,7,8}, "XWing 7");

            grid.ConfirmPossibilities(7, 0, new[] { 2, 7, 8 }, "XWing 2");
            grid.ConfirmPossibilities(7, 1, new[] { 7, 8 });
            grid.ConfirmPossibilities(7, 3, new[] { 2, 8 }, "XWing 2");
        }
    }
}
