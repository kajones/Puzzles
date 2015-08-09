using System.Collections.Generic;
using Puzzles.Core.Models.SuDoku;
using Puzzles.Core.SuDoku.Extensions;

namespace Puzzles.Core.SuDoku.Solvers
{
    public class UniquePossibilityInRowSolver : ISolver
    {
        public void Solve(Grid grid)
        {
            for (var rowIdx = 0; rowIdx < 9; ++rowIdx)
            {
                var fixedDigits = new HashSet<int>();

                for (var colIdx = 0; colIdx < 9; ++colIdx)
                {
                    if (grid.Squares[rowIdx, colIdx].IsSolved)
                    {
                        fixedDigits.Add(grid.Squares[rowIdx, colIdx].Digit);
                    }
                }

                for (var digitToPlace = 1; digitToPlace <= 9; ++digitToPlace)
                {
                    if (fixedDigits.Contains(digitToPlace)) continue;
                    var possibleLocations = new List<int>();
                    for (var colIdx = 0; colIdx < 9; ++colIdx)
                    {
                        if (grid.Squares[rowIdx, colIdx].IsSolved) continue;
                        if (grid.Squares[rowIdx, colIdx].PossibleDigits.Contains(digitToPlace))
                        {
                            possibleLocations.Add(colIdx);
                        }
                    }
                    if (possibleLocations.Count == 1)
                    {
                        grid.SetDigit(rowIdx, possibleLocations[0], digitToPlace);
                    }
                }
            }
        }
    }
}