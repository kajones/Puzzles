using System.Collections.Generic;
using Puzzles.Core.Models.SuDoku;
using Puzzles.Core.SuDoku.Extensions;

namespace Puzzles.Core.SuDoku.Solvers
{
    public class UniquePossibilityInColumnSolver : ISolver
    {
        public void Solve(Grid grid)
        {
            for (var colIdx = 0; colIdx < 9; ++colIdx)
            {
                var fixedDigits = new HashSet<int>();

                for (var rowIdx = 0; rowIdx < 9; ++rowIdx)
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

                    for (var rowIdx = 0; rowIdx < 9; ++rowIdx)
                    {
                        if (grid.Squares[rowIdx, colIdx].IsSolved) continue;
                        if (grid.Squares[rowIdx, colIdx].PossibleDigits.Contains(digitToPlace))
                        {
                            possibleLocations.Add(rowIdx);
                        }
                    }

                    if (possibleLocations.Count == 1)
                    {
                        grid.SetDigit(possibleLocations[0], colIdx, digitToPlace);
                    }
                }
            }
        }
    }
}