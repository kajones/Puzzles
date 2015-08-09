using System;
using System.Collections.Generic;
using System.Linq;
using Puzzles.Core.Models.SuDoku;
using Puzzles.Core.SuDoku.Extensions;

namespace Puzzles.Core.SuDoku.Solvers
{
    public class RowInBoxSolver : ISolver
    {
        /// <summary>
        /// Given that you need to place a certain digit in two of the three boxes in a row (i.e. it has already been placed in one box)
        /// Can you eliminate one of the rows because all three squares within a particular box have been filled with other digits?
        /// 
        /// e.g.
        /// 100 920 000
        /// 524 017 009
        /// 000 004 271
        /// 
        /// - in this the digit 5 has only been positioned once (the middle row).  So theoretically in the other two boxes
        ///   it will appear in either the top or the bottom row.  However it can't appear in the bottom row in the third box
        ///   because all three squares have been taken.  Therefore despite not knowing which of the top row squares in the third box 
        ///   it will appear in, you know that it can't appear in the top row in the middle box and therefore it must be in 
        ///   the bottom row for the middle box
        ///   i.e. the only remaining possibilities for 5 are marked as ? and it can be removed as a possibility from the square X
        ///    100 92X ???
        ///    524 017 009
        ///    000 ??4 271
        /// </summary>
        /// <param name="grid"></param>
        public void Solve(Grid grid)
        {
            var boxCols = grid.GetBoxColumns();
            var boxRows = grid.GetBoxRows();

            for (var digit = 1; digit <= 9; ++digit)
            {
                // Top box row then middle box row then bottom box row (of 3x3 squares)
                for (var boxRow = 1; boxRow <= 3; ++boxRow)
                {
                    // Square rows within the box row
                    var topSquareRow = boxRows[boxRow].Item1;
                    var middleSquareRow = boxRows[boxRow].Item1 + 1;
                    var bottomSquareRow = boxRows[boxRow].Item2;

                    // Key is the box number (i.e. left, middle, right) and value is the row
                    // in which the digit is found (if at all)
                    var rowInBox = grid.FindTheDigitWithinEachBoxInTheBoxRow(boxCols, digit, topSquareRow, middleSquareRow, bottomSquareRow);

                    // If the current digit has been placed in exactly one box in the current box row
                    // then there is potential to eliminate it as a possibility from one of the square rows
                    // in the remaining two boxes
                    var numberOfBoxesPlaced = rowInBox.Count(kvp => kvp.Value != GridDigitInBoxExtensions.NotFound);
                    if (numberOfBoxesPlaced != 1) continue;

                    var digitPlacedBoxCombo = rowInBox.First(kvp => kvp.Value != GridDigitInBoxExtensions.NotFound);
                    var boxWhereDigitPlaced = digitPlacedBoxCombo.Key;
                    var digitPlacedInRow = digitPlacedBoxCombo.Value;
                    int firstRowToCheck;
                    int secondRowToCheck;
                    if (digitPlacedInRow == topSquareRow)
                    {
                        firstRowToCheck = middleSquareRow;
                        secondRowToCheck = bottomSquareRow;
                    }
                    else if (digitPlacedInRow == middleSquareRow)
                    {
                        firstRowToCheck = topSquareRow;
                        secondRowToCheck = bottomSquareRow;
                    }
                    else
                    {
                        firstRowToCheck = topSquareRow;
                        secondRowToCheck = middleSquareRow;
                    }

                    // Key is box number, value is whether or not all squares in that
                    var completeFirstRowInBox = new Dictionary<int, int> {{1, 0}, {2, 0}, {3, 0}};
                    var completeSecondRowInBox = new Dictionary<int, int> { { 1, 0 }, { 2, 0 }, { 3, 0 } };

                    for (var boxCol = 1; boxCol <= 3; ++boxCol)
                    {
                        if (rowInBox[boxCol] != GridDigitInBoxExtensions.NotFound)
                        {
                            continue;
                        }

                        var firstRowSolvedSquares = 0;
                        var secondRowSolvedSquares = 0;
                        for (var colIdx = boxCols[boxCol].Item1; colIdx <= boxCols[boxCol].Item2; ++colIdx)
                        {
                            if (grid.Squares[firstRowToCheck, colIdx].IsSolved)
                            {
                                firstRowSolvedSquares++;
                            }
                            if (grid.Squares[secondRowToCheck, colIdx].IsSolved)
                            {
                                secondRowSolvedSquares++;
                            }
                        }
                        if (firstRowSolvedSquares == 3)
                        {
                            completeFirstRowInBox[boxCol] = boxCol;
                        }
                        if (secondRowSolvedSquares == 3)
                        {
                            completeSecondRowInBox[boxCol] = boxCol;
                        }
                    }

                    var boxToClear = 0;
                    var rowToClear = 0;

                    // If there is any row within a box (unsolved for the digit) that is complete
                    // then there is no room for the digit and then for that row it *must* be in the third box
                    if (completeFirstRowInBox.Any(kvp => kvp.Value != 0))
                    {
                        rowToClear = secondRowToCheck;

                        var boxWhereComplete = completeFirstRowInBox.First(kvp => kvp.Value != 0).Key;
                        boxToClear = FindBoxToClear(boxWhereDigitPlaced, boxWhereComplete);
                    }

                    if (completeSecondRowInBox.Any(kvp => kvp.Value != 0))
                    {
                        rowToClear = firstRowToCheck;

                        var boxWhereComplete = completeSecondRowInBox.First(kvp => kvp.Value != 0).Key;
                        boxToClear = FindBoxToClear(boxWhereDigitPlaced, boxWhereComplete);
                    }

                    if (boxToClear != 0)
                    {
                        for (var colIdx = boxCols[boxToClear].Item1; colIdx <= boxCols[boxToClear].Item2; ++colIdx)
                        {
                            grid.CheckIfTestCase(rowToClear, colIdx, digit);
                            grid.Squares[rowToClear, colIdx].RemovePossibleDigit(digit);
                        }
                    }
                }
            }
        }


        private static int FindBoxToClear(int boxWhereDigitPlaced, int boxWhereComplete)
        {
            switch (boxWhereDigitPlaced)
            {
                case 1:
                    return (boxWhereComplete == 2) ? 3 : 2;
                case 2:
                    return (boxWhereComplete == 1) ? 3 : 1;
                case 3:
                    return (boxWhereComplete == 1) ? 2 : 1;
                default:
                    throw new ApplicationException("Failed to find correct box to clear");
            }    
        }
    }
}