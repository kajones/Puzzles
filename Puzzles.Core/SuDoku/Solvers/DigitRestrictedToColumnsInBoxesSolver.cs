using System.Linq;
using Puzzles.Core.Models.SuDoku;
using Puzzles.Core.SuDoku.Extensions;

namespace Puzzles.Core.SuDoku.Solvers
{
    /// <summary>
    /// If a given digit can only be in two matching columns in two of the three boxes in a vertical set
    /// then it must be in the third column in the third box
    /// e.g.
    /// 9      8      (67)
    /// 4      2      5
    /// (37)  (36)    1
    /// --------------------
    /// (13)  (135)   4
    /// 6     (157)   8
    /// 2     (57)    9
    /// --------------------
    /// 5     (1346) (367)
    /// (137) (1346) (2367)
    /// 8     9      (26)
    /// </summary>
    public class DigitRestrictedToColumnsInBoxesSolver : ISolver
    {
        public void Solve(Grid grid)
        {
            const int IgnoreColumn = -1;

            var boxRows = grid.GetBoxRows();
            var boxCols = grid.GetBoxColumns();

            for (var digit = 1; digit <= 9; ++digit)
            {
                for (var boxCol = 1; boxCol <= 3; ++boxCol)
                {
                    var leftColumn = boxCols[boxCol].Item1;
                    var middleColumn = boxCols[boxCol].Item1 + 1;
                    var rightColumn = boxCols[boxCol].Item2;

                    var possibleDigitBoxColumns = grid.FindThePotentialDigitInTheBoxColumns(boxRows, digit, leftColumn, middleColumn, rightColumn);
                    var countOfColumnBoxPairs = possibleDigitBoxColumns.Count(pb => pb.Value.Count == 2);
                    if (countOfColumnBoxPairs < 2) continue;

                    foreach (var possibleDigitBoxColumn in possibleDigitBoxColumns)
                    {
                        var firstBox = possibleDigitBoxColumn.Key;
                        if (possibleDigitBoxColumn.Value.Count == 0) continue;
                        if (possibleDigitBoxColumn.Value.Count > 2) continue;
                        var firstColumn = possibleDigitBoxColumn.Value[0];
                        var secondColumn = possibleDigitBoxColumn.Value.Count > 1
                            ? possibleDigitBoxColumn.Value[1]
                            : IgnoreColumn;

                        foreach (var otherDigitBoxColumn in possibleDigitBoxColumns)
                        {
                            if (otherDigitBoxColumn.Key == possibleDigitBoxColumn.Key) continue;
                            if (otherDigitBoxColumn.Value.Count > 2) continue;
                            if (otherDigitBoxColumn.Value.Count == 0) continue;
                            if (!otherDigitBoxColumn.Value.Contains(firstColumn)) continue;
                            if (secondColumn != IgnoreColumn && !otherDigitBoxColumn.Value.Contains(secondColumn)) continue;
                            var secondBox = otherDigitBoxColumn.Key;
                            if (secondColumn == IgnoreColumn && otherDigitBoxColumn.Value.Count > 1)
                            {
                                secondColumn = otherDigitBoxColumn.Value[1];
                            }

                            // Digit is restricted to two particular columns between two boxes
                            // Therefore remove from the third
                            var thirdBox = possibleDigitBoxColumns.Single(pb => pb.Key != firstBox && pb.Key != secondBox);

                            for (var rowIdx = boxRows[thirdBox.Key].Item1;
                                rowIdx <= boxRows[thirdBox.Key].Item2;
                                ++ rowIdx)
                            {
                                grid.Squares[rowIdx, firstColumn].RemovePossibleDigit(digit);
                                if (secondColumn != IgnoreColumn)
                                {
                                    grid.Squares[rowIdx, secondColumn].RemovePossibleDigit(digit);
                                }
                            }

                        }
                    }
                }
            }
        }
    }
}