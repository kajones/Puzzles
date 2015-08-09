using System.Collections.Generic;
using System.Linq;
using Puzzles.Core.Models.SuDoku;
using Puzzles.Core.SuDoku.Extensions;

namespace Puzzles.Core.SuDoku.Solvers
{
    /// <summary>
    /// If there are four squares that form a rectangle where they each contain the same remaining possible digit (but it is not necessarily unique)
    /// and for the columns the two squares on each side are the only squares that the digit can go
    /// Then you know that if that digit is still a possibility in any other squares in that row that it can be removed:
    /// 
    /// xxx xxx Dxx
    /// x2x xxx 2xA
    /// xxx xxx xxx
    /// 
    /// xxx xxx xxx
    /// xxx xxx xxx
    /// x2x Bxx 2xx
    /// 
    /// xxx xxx xxx
    /// xxx xxx xxx
    /// xCx xxx xxx
    /// 
    /// i.e. If 2 remains as a possibility at either A or B then it can be removed
    ///      However if 2 is a possibility at either C or D then no action can be taken
    /// </summary>
    public class XWingByColumnSolver : ISolver
    {
        public void Solve(Grid grid)
        {
            // Need to have a column after so only check first 8 cols
            for (var firstColIdx = 0; firstColIdx < 8; ++ firstColIdx)
            {
                var firstPossibleLocations = GetPossibleLocations(grid, firstColIdx);

                // Find digits that have exactly two possible locations - possible rectangle
                var potentialRectangleLocations = firstPossibleLocations.Where(pl => pl.Value.Count == 2);
                foreach (var potentialRectangleLocation in potentialRectangleLocations)
                {
                    var digitToMatch = potentialRectangleLocation.Key;
                    var topRowToMatch = potentialRectangleLocation.Value[0];
                    var bottomRowToMatch = potentialRectangleLocation.Value[1];

                    for (var secondColIdx = firstColIdx + 1; secondColIdx < 9; ++ secondColIdx)
                    {
                        var secondPossibleLocations = GetPossibleLocations(grid, secondColIdx);
                        if (!secondPossibleLocations.ContainsKey(digitToMatch) ||
                            secondPossibleLocations[digitToMatch].Count != 2) continue;
                        if (!secondPossibleLocations[digitToMatch].Contains(topRowToMatch) ||
                            !secondPossibleLocations[digitToMatch].Contains(bottomRowToMatch)) continue;

                        // Have found a rectangle, now remove the digit (if it exists) from any other columns in the top and bottom rows
                        for (var colToRemoveIdx = 0; colToRemoveIdx < 9; ++ colToRemoveIdx)
                        {
                            // Avoid removing it from the rectangle
                            if (colToRemoveIdx == firstColIdx || colToRemoveIdx == secondColIdx) continue;

                            if (!grid.Squares[topRowToMatch, colToRemoveIdx].IsSolved)
                            {
                                grid.CheckIfTestCase(topRowToMatch, colToRemoveIdx, digitToMatch);
                                grid.Squares[topRowToMatch, colToRemoveIdx].RemovePossibleDigit(digitToMatch);
                            }
                            if (!grid.Squares[bottomRowToMatch, colToRemoveIdx].IsSolved)
                            {
                                grid.CheckIfTestCase(bottomRowToMatch, colToRemoveIdx, digitToMatch);
                                grid.Squares[bottomRowToMatch, colToRemoveIdx].RemovePossibleDigit(digitToMatch);
                            }
                        }
                    }
                }
            }
        }

        private static Dictionary<int, List<int>> GetPossibleLocations(Grid grid, int colIdx)
        {
            var possibleLocations = new Dictionary<int, List<int>>();
            for (var rowIdx = 0; rowIdx < 9; ++rowIdx)
            {
                var square = grid.Squares[rowIdx, colIdx];
                if (square.IsSolved) continue;

                foreach (var possibleDigit in square.PossibleDigits)
                {
                    if (!possibleLocations.ContainsKey(possibleDigit))
                    {
                        possibleLocations.Add(possibleDigit, new List<int>());
                    }
                    possibleLocations[possibleDigit].Add(rowIdx);
                }
            }

            return possibleLocations;
        }
    }
}