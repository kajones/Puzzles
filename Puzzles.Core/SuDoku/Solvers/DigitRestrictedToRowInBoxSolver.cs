using System.Linq;
using Puzzles.Core.Models.SuDoku;
using Puzzles.Core.SuDoku.Extensions;

namespace Puzzles.Core.SuDoku.Solvers
{
    /// <summary>
    /// If the possible locations for a given digit within a selected box are restricted to a single row
    /// then the digit must appear in that row and can be removed from the other squares in that row
    /// </summary>
    public class DigitRestrictedToRowInBoxSolver : ISolver
    {
        public void Solve(Grid grid)
        {
            var boxCols = grid.GetBoxColumns();
            var boxRows = grid.GetBoxRows();

            for (var digit = 1; digit <= 9; ++digit)
            {
                for (var boxRow = 1; boxRow <= 3; ++boxRow)
                {
                    var topRow = boxRows[boxRow].Item1;
                    var middleRow = boxRows[boxRow].Item1 + 1;
                    var bottomRow = boxRows[boxRow].Item2;

                    var possibleDigitRows = grid.FindThePotentialDigitInTheBoxRows(boxCols, digit, topRow, middleRow,
                        bottomRow);
                    if (possibleDigitRows.Values.All(v => v.Count != 1)) continue;

                    foreach (var possibleDigitRow in possibleDigitRows)
                    {
                        if (possibleDigitRow.Value.Count != 1) continue;
                        var rowToClear = possibleDigitRow.Value[0];

                        for (var otherBoxCol = 1; otherBoxCol <= 3; ++otherBoxCol)
                        {
                            if (otherBoxCol == possibleDigitRow.Key) continue;

                            for (var colIdx = boxCols[otherBoxCol].Item1;
                                colIdx <= boxCols[otherBoxCol].Item2;
                                ++colIdx)
                            {
                                if (grid.Squares[rowToClear, colIdx].IsSolved) continue;
                                grid.Squares[rowToClear, colIdx].RemovePossibleDigit(digit);
                            }
                        }
                    }
                }
            }
        }
    }
}