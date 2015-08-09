using System.Linq;
using Puzzles.Core.Models.SuDoku;
using Puzzles.Core.SuDoku.Extensions;

namespace Puzzles.Core.SuDoku.Solvers
{
    /// <summary>
    /// If you haven't solved a digit within a given box but you know it must be in two/three squares within a single
    /// column in that box then you can remove it from that same column from any other boxes
    /// e.g. if there is a box:
    /// 
    /// (135) (135) 4
    ///   6   (157) 8
    ///   2   (57)  9
    /// 
    /// then the digit 7 can only appear in the middle column and therefore can be removed from the vertical boxes next to it 
    /// for the same column (i.e. in the other boxes it will be in the left/right column)
    /// </summary>
    public class DigitRestrictedToColumnInBoxSolver : ISolver
    {
        public void Solve(Grid grid)
        {
            var boxCols = grid.GetBoxColumns();
            var boxRows = grid.GetBoxRows();

            for (var digit = 1; digit <= 9; ++digit)
            {
                for (var boxCol = 1; boxCol <= 3; ++boxCol)
                {
                    var leftColumn = boxCols[boxCol].Item1;
                    var middleColumn = boxCols[boxCol].Item1 + 1;
                    var rightColumn = boxCols[boxCol].Item2;

                    var possibleDigitBoxColumns = grid.FindThePotentialDigitInTheBoxColumns(boxRows, digit, leftColumn, middleColumn, rightColumn);
                    if (possibleDigitBoxColumns.Values.All(v => v.Count != 1)) continue;

                    // Where there is only one possible column for a digit within the box, remove it from that column for other boxes
                    foreach (var possibleDigitBoxColumn in possibleDigitBoxColumns)
                    {
                        if (possibleDigitBoxColumn.Value.Count != 1) continue;
                        var columnToClear = possibleDigitBoxColumn.Value[0];

                        for (var otherBoxRow = 1; otherBoxRow <= 3; ++otherBoxRow)
                        {
                            if (otherBoxRow == possibleDigitBoxColumn.Key) continue;

                            for (var rowIdx = boxRows[otherBoxRow].Item1;
                                rowIdx <= boxRows[otherBoxRow].Item2;
                                ++rowIdx)
                            {
                                if (grid.Squares[rowIdx, columnToClear].IsSolved) continue;
                                grid.Squares[rowIdx, columnToClear].RemovePossibleDigit(digit);
                            }
                        }
                    }
                }
            }
        }
    }
}