using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Puzzles.Core.Helpers;
using Puzzles.Core.SuDoku;
using Puzzles.Core.SuDoku.Extensions;

namespace Puzzles.ProjectEuler.Problems_0001_0100
{
    /// <summary>
    /// Su Doku (Japanese meaning number place) is the name given to a popular puzzle concept. 
    /// Its origin is unclear, but credit must be attributed to Leonhard Euler who invented a similar, and much more difficult, 
    /// puzzle idea called Latin Squares. 
    /// The objective of Su Doku puzzles, however, is to replace the blanks (or zeros) in a 9 by 9 grid 
    /// in such that each row, column, and 3 by 3 box contains each of the digits 1 to 9. 
    /// 
    /// Below is an example of a typical starting puzzle grid and its solution grid.
    ///
    /// Puzzle:
    /// 0 0 3  0 2 0  6 0 0
    /// 9 0 0  3 0 5  0 0 1
    /// 0 0 1  8 0 6  4 0 0	
    ///
    /// 0 0 8  1 0 2  9 0 0
    /// 7 0 0  0 0 0  0 0 8
    /// 0 0 6  7 0 8  2 0 0
    ///
    /// 0 0 2  6 0 9  5 0 0
    /// 8 0 0  2 0 3  0 0 9
    /// 0 0 5  0 1 0  3 0 0
    /// 
    /// Solution:
    /// 4 8 3  9 2 1  6 5 7
    /// 9 6 7  3 4 5  8 2 1
    /// 2 5 1  8 7 6  4 9 3
    /// 
    /// 5 4 8  1 3 2  9 7 6
    /// 7 2 9  5 6 4  1 3 8
    /// 1 3 6  7 9 8  2 4 5
    ///
    /// 3 7 2  6 8 9  5 1 4
    /// 8 1 4  2 5 3  7 6 9
    /// 6 9 5  4 1 7  3 8 2
    ///
    /// A well constructed Su Doku puzzle has a unique solution and can be solved by logic, 
    /// although it may be necessary to employ "guess and test" methods in order to eliminate options (there is much contested opinion over this). The complexity of the search determines the difficulty of the puzzle; the example above is considered easy because it can be solved by straight forward direct deduction.
    ///
    /// The 6K text file, sudoku.txt (right click and 'Save Link/Target As...'), contains fifty different Su Doku puzzles ranging in difficulty, 
    /// but all with unique solutions (the first puzzle in the file is the example above).
    ///
    /// By solving all fifty puzzles find the sum of the 3-digit numbers found in the top left corner of each solution grid; 
    /// for example, 483 is the 3-digit number found in the top left corner of the solution grid above.
    /// </summary>
    [TestFixture]
    public class Problem_0096_SuDoku
    {
        private const string exampleDefinition = @"Grid 01
003020600
900305001
001806400
008102900
700000008
006708200
002609500
800203009
005010300";

        [Test]
        public void ConfirmLoadExample()
        {
            var grid = GridBuilder.BuildFromDefinition(exampleDefinition);

            grid.IsSolved.Should().BeFalse();

            grid.Squares[0, 0].Digit.Should().Be(0);
            grid.Squares[0, 1].Digit.Should().Be(0);
            grid.Squares[0, 2].Digit.Should().Be(3);
            grid.Squares[0, 3].Digit.Should().Be(0);
            grid.Squares[0, 4].Digit.Should().Be(2);
            grid.Squares[0, 5].Digit.Should().Be(0);
            grid.Squares[0, 6].Digit.Should().Be(6);
            grid.Squares[0, 7].Digit.Should().Be(0);
            grid.Squares[0, 8].Digit.Should().Be(0);

            // 005010300
            grid.Squares[8, 0].Digit.Should().Be(0);
            grid.Squares[8, 1].Digit.Should().Be(0);
            grid.Squares[8, 2].Digit.Should().Be(5);
            grid.Squares[8, 3].Digit.Should().Be(0);
            grid.Squares[8, 4].Digit.Should().Be(1);
            grid.Squares[8, 5].Digit.Should().Be(0);
            grid.Squares[8, 6].Digit.Should().Be(3);
            grid.Squares[8, 7].Digit.Should().Be(0);
            grid.Squares[8, 8].Digit.Should().Be(0);

            Console.WriteLine(grid.Summary());
        }

        [Test, Explicit]
        public void SolveGrid01()
        {
            var grid = GridBuilder.BuildFromDefinition(exampleDefinition);
            grid.Tidy();

            grid.Solve();

            Console.WriteLine("Squares unsolved: {0}{1}", grid.UnsolvedSquareCount, Environment.NewLine);
            Console.WriteLine(grid.Summary());
            grid.IsSolved.Should().BeTrue("Should have finished solving the example");
            grid.Key.Should().Be(483);
        }

        [Test, Explicit]
        public void SolveGrid02()
        {
            const string definition = @"Grid 02
200080300
060070084
030500209
000105408
000000000
402706000
301007040
720040060
004010003";

            var grid = GridBuilder.BuildFromDefinition(definition);
            Console.WriteLine(grid.Summary());
            grid.Tidy();

            grid.Solve();

            Console.WriteLine("Squares unsolved: {0}{1}", grid.UnsolvedSquareCount, Environment.NewLine);
            Console.WriteLine(grid.Summary());
            grid.IsSolved.Should().BeTrue("Should have finished solving the example");
            grid.Key.Should().Be(245);
        }

        [Test, Explicit]
        public void SolveGrid06()
        {
            const string definition = @"Grid 06
100920000
524010000
000000070
050008102
000000000
402700090
060000000
000030945
000071006
";

            var grid = GridBuilder.BuildFromDefinition(definition);
            Console.WriteLine(grid.Summary());
            grid.Tidy();

            grid.Solve();

            Console.WriteLine("Squares unsolved: {0}{1}", grid.UnsolvedSquareCount, Environment.NewLine);
            Console.WriteLine(grid.Summary());
            grid.IsSolved.Should().BeTrue("Should have finished solving the example");
            grid.Key.Should().Be(176);
        }

        [Test, Explicit]
        public void SolveGrid07()
        {
            const string definition = @"Grid 07
043080250
600000000
000001094
900004070
000608000
010200003
820500000
000000005
034090710
";

            var grid = GridBuilder.BuildFromDefinition(definition);
            Console.WriteLine(grid.Summary());
            grid.Tidy();

            grid.Solve();
            //var solver = new BruteForceSolver();
            //solver.Solve(grid);

            Console.WriteLine("Squares unsolved: {0}{1}", grid.UnsolvedSquareCount, Environment.NewLine);
            Console.WriteLine(grid.Summary());
            grid.IsSolved.Should().BeTrue("Should have finished solving the example");
            grid.Key.Should().Be(143);
        }

        [Test, Explicit]
        public void SolveGrid42()
        {
            // Partially solved grid 42
            const string definition = @"Grid 42
380000920
006439785
009020300
060090000
800302009
900040070
001970508
495286137
008000092
";

            var grid = GridBuilder.BuildFromDefinition(definition);
            grid.Tidy();
            Console.WriteLine(grid.Summary());


            grid.Solve();

            Console.WriteLine("Squares unsolved: {0}{1}", grid.UnsolvedSquareCount, Environment.NewLine);
            Console.WriteLine(grid.Summary());
            grid.IsSolved.Should().BeTrue("Should have finished solving the example");
            grid.Key.Should().Be(384);
        }

        /// <summary>
        /// 24702
        /// </summary>
        [Test, Explicit]
        public void SolveAllGridsInFile()
        {
            var gridDefinitions = GetGridDefinitionsFromFile();

            gridDefinitions.Count.Should().Be(50);
            gridDefinitions[0].Should().Be(exampleDefinition);

            var total = 0;

            for (var idx = 0; idx < gridDefinitions.Count; ++idx)
            {
                var grid = GridBuilder.BuildFromDefinition(gridDefinitions[idx]);
                grid.Number = idx + 1;
                grid.Solve(true);

                if (!grid.IsSolved)
                {
                    Console.WriteLine("Failed to solve grid: {0}", grid.Number);
                    Console.WriteLine("Progress so far: {0} unsolved squares", grid.UnsolvedSquareCount);
                    Console.WriteLine(grid.Summary());
                    Assert.Fail("Failed to solve grid {0}", grid.Number);
                }

                total += grid.Key;
            }

            Console.WriteLine("Total of grid keys: {0}", total);
            total.Should().Be(24702);
        }

        private static List<string> GetGridDefinitionsFromFile()
        {
            var fileContents = FileHelper.GetEmbeddedResourceContent("Puzzles.ProjectEuler.DataFiles.Problem_0096_sudoku.txt");
            fileContents.Should().NotBeNullOrEmpty();

            var gridDefinitions = new List<string>();
            var gridStartPos = 0;
            while (gridStartPos > -1)
            {
                var nextStartPos = fileContents.IndexOf("Grid", (gridStartPos + 1), StringComparison.Ordinal);
                gridDefinitions.Add((nextStartPos > -1)
                    ? fileContents.Substring(gridStartPos, (nextStartPos - gridStartPos - 1))
                    : fileContents.Substring(gridStartPos));

                gridStartPos = nextStartPos;
            }
            return gridDefinitions;
        }
    }
}
