using System;
using Puzzles.Core.Models.SuDoku;

namespace Puzzles.Core.SuDoku.Extensions
{
    public static class GridSetDigitExtensions
    {
        public static void SetDigit(this Grid grid, int rowIdx, int colIdx, int digit)
        {
            grid.EnsureValidToSetTheDigit(rowIdx, colIdx, digit);

            grid.Squares[rowIdx, colIdx].SetDigit(digit);
            grid.TidyForSquare(rowIdx, colIdx, digit);
        }

        public static void SetRowDigits(this Grid grid, int rowIdx, int[] digits)
        {
            if (digits.Length != 9) throw new ApplicationException("Expecting 9 digits for the row");

            for (var colIdx = 0; colIdx < 9; ++ colIdx)
            {
                grid.Squares[rowIdx, colIdx].SetDigit(digits[colIdx]);
            }
        }
    }
}
