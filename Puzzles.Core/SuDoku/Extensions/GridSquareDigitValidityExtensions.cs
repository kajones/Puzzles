using System;
using Puzzles.Core.Models.SuDoku;

namespace Puzzles.Core.SuDoku.Extensions
{
    public static class GridSquareDigitValidityExtensions
    {
        public static bool EnsureValidToSetTheDigit(this Grid grid, int rowIdx, int colIdx, int digit, bool throwExceptionIfInvalid = true)
        {
            var digitExistsInRow = DigitExistsInRow(grid, rowIdx, colIdx, digit);
            if (digitExistsInRow)
            {
                if (throwExceptionIfInvalid)
                {
                    throw new ApplicationException("Cannot set digit - duplicate entry in row");
                }
                return false;
            }
            var digitExistsInColumn = DigitExistsInColumn(grid, rowIdx, colIdx, digit);
            if (digitExistsInColumn)
            {
                if (throwExceptionIfInvalid)
                {
                    throw new ApplicationException("Cannot set digit - duplicate entry in columnn");
                }
                return false;
            }
            var digitExistsInBox = DigitExistsInBox(grid, rowIdx, colIdx, digit);
            if (digitExistsInBox)
            {
                if (throwExceptionIfInvalid)
                {
                    throw new ApplicationException("Cannot set digit - duplicate entry in box");
                }
                return false;
            }

            return true;
        }

        private static bool DigitExistsInRow(this Grid grid, int rowIdx, int colToIgnore, int digit)
        {
            for (var colIdx = 0; colIdx < 9; ++colIdx)
            {
                if (colIdx == colToIgnore) continue;
                if (grid.Squares[rowIdx, colIdx] == null) continue;
                if (grid.Squares[rowIdx, colIdx].Digit == digit) return true;
            }

            return false;
        }

        private static bool DigitExistsInColumn(this Grid grid, int rowToIgnore, int colIdx, int digit)
        {
            for (var rowIdx = 0; rowIdx < 9; ++rowIdx)
            {
                if (rowIdx == rowToIgnore) continue;
                if (grid.Squares[rowIdx, colIdx] == null) continue;
                if (grid.Squares[rowIdx, colIdx].Digit == digit) return true;
            }

            return false;
        }

        private static bool DigitExistsInBox(this Grid grid, int rowToIgnore, int colToIgnore, int digit)
        {
            var firstRowInBox = grid.GetBoxStart(rowToIgnore);
            var lastRowInBox = grid.GetBoxEnd(firstRowInBox);

            var firstColInBox = grid.GetBoxStart(colToIgnore);
            var lastColInBox = grid.GetBoxEnd(firstColInBox);

            for (var rowIdx = firstRowInBox; rowIdx <= lastRowInBox; ++rowIdx)
            {
                for (var colIdx = firstColInBox; colIdx <= lastColInBox; ++colIdx)
                {
                    if (rowIdx == rowToIgnore && colIdx == colToIgnore) continue;
                    if (grid.Squares[rowIdx, colIdx] == null) continue;
                    if (grid.Squares[rowIdx, colIdx].Digit == digit) return true;
                }
            }

            return false;
        }

    }
}
