using System;
using Puzzles.Core.Models.SuDoku;

namespace Puzzles.Core.SuDoku
{
    public static class GridBuilder 
    {
        private const string emptyDefinition = @"Grid 00
000000000
000000000
000000000
000000000
000000000
000000000
000000000
000000000
000000000";

        public static Grid BuildEmptyGrid()
        {
            return BuildFromDefinition(emptyDefinition);
        }

        public static Grid BuildFromDefinition(string definition)
        {
            var grid = new Grid {Squares = new Square[9, 9]};

            var rows = definition.Split(new[] {@"
"}, StringSplitOptions.RemoveEmptyEntries);
            if (rows.Length != 10)
            {
                var errorMessage = string.Format("Definition should contain 10 rows: 1 title then 9 rows of squares.{0}Found {1} rows in {2}", Environment.NewLine, rows.Length, definition);
                throw new ApplicationException(errorMessage);
            }

            for (var rowIdx = 1; rowIdx < rows.Length; ++rowIdx)
            {
                for (var colIdx = 0; colIdx < rows[rowIdx].Length; ++colIdx)
                {
                    var digit = (int)Char.GetNumericValue(rows[rowIdx][colIdx]);
                    var square = new Square();
                    //grid.SetDigit(rowIdx, colIdx, digit);
                    square.SetDigit(digit);
                    grid.Squares[rowIdx - 1, colIdx] = square;
                }
            }

            return grid;
        }
    }
}