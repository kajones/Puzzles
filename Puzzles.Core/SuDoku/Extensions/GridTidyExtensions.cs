using System;
using Puzzles.Core.Models.SuDoku;

namespace Puzzles.Core.SuDoku.Extensions
{
    public static class GridTidyExtensions
    {
        /// <summary>
        /// Given that a row already has digit x no other square in the row can have that digit etc
        /// </summary>
        /// <param name="grid"></param>
        public static void Tidy(this Grid grid)
        {
            for (var rowIdx = 0; rowIdx < 9; ++rowIdx)
            {
                for (var colIdx = 0; colIdx < 9; ++colIdx)
                {
                    var currentSquare = grid.Squares[rowIdx, colIdx];
                    if (!currentSquare.IsSolved) continue;

                    var currentDigit = currentSquare.Digit;
                    TidyForSquare(grid, rowIdx, colIdx, currentDigit);
                }
            }
        }

        public static void TidyForSquare(this Grid grid, int rowIdx, int colIdx, int currentDigit)
        {
            RemoveDigitFromRow(grid, rowIdx, currentDigit);
            RemoveDigitFromColumn(grid, colIdx, currentDigit);
            RemoveDigitFromSquare(grid, rowIdx, colIdx, currentDigit);
        }

        private static void RemoveDigitFromRow(Grid grid, int rowIdx, int currentDigit)
        {
            for (var colIdx = 0; colIdx < 9; ++colIdx)
            {
                if (grid.Squares[rowIdx, colIdx].IsSolved) continue;
                grid.CheckIfTestCase(rowIdx, colIdx, currentDigit);
                grid.Squares[rowIdx, colIdx].RemovePossibleDigit(currentDigit);
            }
        }

        private static void RemoveDigitFromColumn(Grid grid, int colIdx, int currentDigit)
        {
            for (var rowIdx = 0; rowIdx < 9; ++rowIdx)
            {
                if (grid.Squares[rowIdx, colIdx].IsSolved) continue;
                try
                {
                    grid.CheckIfTestCase(rowIdx, colIdx, currentDigit);
                    grid.Squares[rowIdx, colIdx].RemovePossibleDigit(currentDigit);
                }
                catch (Exception)
                {
                    Console.WriteLine("Trying to remove too much: Row {0} Col {1}", rowIdx, colIdx);
                    Console.WriteLine(grid.Summary());
                    throw;
                }
            }
        }

        private static void RemoveDigitFromSquare(Grid grid, int digitRowIdx, int digitColIdx, int currentDigit)
        {
            var firstRowInSquare = grid.GetBoxStart(digitRowIdx);
            var lastRowInSquare = grid.GetBoxEnd(firstRowInSquare);
            var firstColInSquare = grid.GetBoxStart(digitColIdx);
            var lastColInSquare = grid.GetBoxEnd(firstColInSquare);

            for (var sqRowIdx = firstRowInSquare; sqRowIdx <= lastRowInSquare; ++sqRowIdx)
            {
                for (var sqColIdx = firstColInSquare; sqColIdx <= lastColInSquare; ++sqColIdx)
                {
                    if (grid.Squares[sqRowIdx, sqColIdx].IsSolved) continue;
                    grid.CheckIfTestCase(sqRowIdx, sqColIdx, currentDigit);
                    grid.Squares[sqRowIdx, sqColIdx].RemovePossibleDigit(currentDigit);
                }
            }
        }

        public static void CheckIfTestCase(this Grid grid, int rowIdx, int colIdx, int currentDigit)
        {
            return;
            if (rowIdx == 2 && (colIdx == 0 || colIdx == 1))
            {
                Console.WriteLine("Removing {0} from [{1},{2}]", currentDigit, rowIdx, colIdx);
            }
        }
    }
}