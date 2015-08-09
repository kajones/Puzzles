using System;
using System.Collections.Generic;
using Puzzles.Core.Models.SuDoku;
using Puzzles.Core.SuDoku.Extensions;

namespace Puzzles.Core.SuDoku.Solvers
{
    public class UniquePossibilityInBoxSolver : ISolver
    {
        public void Solve(Grid grid)
        {
            SolveForSquare(grid, 0, 2, 0, 2);
            SolveForSquare(grid, 0, 2, 3, 5);
            SolveForSquare(grid, 0, 2, 6, 8);

            SolveForSquare(grid, 3, 5, 0, 2);
            SolveForSquare(grid, 3, 5, 3, 5);
            SolveForSquare(grid, 3, 5, 6, 8);

            SolveForSquare(grid, 6, 8, 0, 2);
            SolveForSquare(grid, 6, 8, 3, 5);
            SolveForSquare(grid, 6, 8, 6, 8);
        }

        private void SolveForSquare(Grid grid, int firstRow, int lastRow, int firstCol, int lastCol)
        {
            var fixedDigits = new HashSet<int>();
            for (var rowIdx = firstRow; rowIdx <= lastRow; ++rowIdx)
            {
                for (var colIdx = firstCol; colIdx <= lastCol; ++colIdx)
                {
                    if (grid.Squares[rowIdx, colIdx].IsSolved)
                    {
                        fixedDigits.Add(grid.Squares[rowIdx, colIdx].Digit);
                    }
                }
            }

            for (var digitToPlace = 1; digitToPlace <= 9; ++digitToPlace)
            {
                var possibleLocations = new List<Tuple<int, int>>();
                if (fixedDigits.Contains(digitToPlace)) continue;

                for (var rowIdx = firstRow; rowIdx <= lastRow; ++rowIdx)
                {
                    for (var colIdx = firstCol; colIdx <= lastCol; ++colIdx)
                    {
                        if (grid.Squares[rowIdx, colIdx].IsSolved) continue;
                        if (grid.Squares[rowIdx, colIdx].PossibleDigits.Contains(digitToPlace))
                        {
                            possibleLocations.Add(new Tuple<int, int>(rowIdx, colIdx));
                        }
                    }
                }

                if (possibleLocations.Count == 1)
                {
                    var location = possibleLocations[0];
                    grid.SetDigit(location.Item1, location.Item2, digitToPlace);
                }
            }
        }
    }
}