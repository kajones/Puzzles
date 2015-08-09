using System;
using System.Collections.Generic;
using Puzzles.Core.Models.SuDoku;

namespace Puzzles.Core.SuDoku.Extensions
{
    public static class GridDigitInBoxExtensions
    {
        public const int NotFound = -1;

        public static Dictionary<int, int> FindTheDigitWithinEachBoxInTheBoxRow(this Grid grid, Dictionary<int, Tuple<int, int>> boxCols, int digit, int topSquareRow, int middleSquareRow, int bottomSquareRow)
        {
            var rowInBox = new Dictionary<int, int> { { 1, NotFound }, { 2, NotFound }, { 3, NotFound } };

            // Left then middle then right box within the current box row
            for (var boxCol = 1; boxCol <= 3; ++boxCol)
            {
                // Look at the columns within the specified box
                for (var colIdx = boxCols[boxCol].Item1; colIdx <= boxCols[boxCol].Item2; ++colIdx)
                {
                    if (grid.Squares[topSquareRow, colIdx].Digit == digit)
                    {
                        rowInBox[boxCol] = topSquareRow;
                    }
                    if (grid.Squares[middleSquareRow, colIdx].Digit == digit)
                    {
                        rowInBox[boxCol] = middleSquareRow;
                    }
                    if (grid.Squares[bottomSquareRow, colIdx].Digit == digit)
                    {
                        rowInBox[boxCol] = bottomSquareRow;
                    }
                }
            }
            return rowInBox;
        }

        /// <summary>
        /// Looks for the specified digit in each of the three vertical boxes
        /// - identifies which of the column(s) it appears in
        /// - ignoring solved positions, this is about possibilities
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="boxRows"></param>
        /// <param name="digit"></param>
        /// <param name="leftColumn"></param>
        /// <param name="middleColumn"></param>
        /// <param name="rightColumn"></param>
        /// <returns></returns>
        public static Dictionary<int, List<int>> FindThePotentialDigitInTheBoxColumns(this Grid grid,
            Dictionary<int, Tuple<int, int>> boxRows, int digit, int leftColumn, int middleColumn, int rightColumn)
        {
            var digitFoundInBoxColumn = new Dictionary<int, List<int>>
            {
                {1, new List<int>()},
                {2, new List<int>()},
                {3, new List<int>()}
            };

            for (var boxRow = 1; boxRow <= 3; ++boxRow)
            {
                for (var rowIdx = boxRows[boxRow].Item1; rowIdx <= boxRows[boxRow].Item2; ++rowIdx)
                {
                    if (grid.Squares[rowIdx, leftColumn].PossibleDigits.Contains(digit))
                    {
                        if (!digitFoundInBoxColumn[boxRow].Contains(leftColumn))
                            digitFoundInBoxColumn[boxRow].Add(leftColumn);
                    }
                    if (grid.Squares[rowIdx, middleColumn].PossibleDigits.Contains(digit))
                    {
                        if (!digitFoundInBoxColumn[boxRow].Contains(middleColumn))
                            digitFoundInBoxColumn[boxRow].Add(middleColumn);
                    }
                    if (grid.Squares[rowIdx, rightColumn].PossibleDigits.Contains(digit))
                    {
                        if (!digitFoundInBoxColumn[boxRow].Contains(rightColumn))
                            digitFoundInBoxColumn[boxRow].Add(rightColumn);
                    }
                }
            }

            return digitFoundInBoxColumn;
        }

        public static Dictionary<int, List<int>> FindThePotentialDigitInTheBoxRows(this Grid grid,
            Dictionary<int, Tuple<int, int>> boxColumns, int digit, int topRow, int middleRow, int bottomRow)
        {
            var digitFoundInBoxRow = new Dictionary<int, List<int>>
            {
                {1, new List<int>()},
                {2, new List<int>()},
                {3, new List<int>()}
            };

            for (var boxCol = 1; boxCol <= 3; ++boxCol)
            {
                for (var colIdx = boxColumns[boxCol].Item1; colIdx <= boxColumns[boxCol].Item2; ++ colIdx)
                {
                    if (grid.Squares[topRow, colIdx].PossibleDigits.Contains(digit))
                    {
                        if (!digitFoundInBoxRow[boxCol].Contains(topRow))
                            digitFoundInBoxRow[boxCol].Add(topRow);
                    }
                    if (grid.Squares[middleRow, colIdx].PossibleDigits.Contains(digit))
                    {
                        if (!digitFoundInBoxRow[boxCol].Contains(middleRow))
                            digitFoundInBoxRow[boxCol].Add(middleRow);
                    }
                    if (grid.Squares[bottomRow, colIdx].PossibleDigits.Contains(digit))
                    {
                        if (!digitFoundInBoxRow[boxCol].Contains(bottomRow))
                            digitFoundInBoxRow[boxCol].Add(bottomRow);
                    }
                }
            }

            return digitFoundInBoxRow;
        }
    }
}