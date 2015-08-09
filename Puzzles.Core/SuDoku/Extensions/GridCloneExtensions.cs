using Puzzles.Core.Models.SuDoku;

namespace Puzzles.Core.SuDoku.Extensions
{
    public static class GridCloneExtensions
    {
        public static Grid Clone(this Grid grid)
        {
            var clone = new Grid {Squares = new Square[9, 9]};

            for (var rowIdx = 0; rowIdx < 9; ++rowIdx)
            {
                for (var colIdx = 0; colIdx < 9; ++colIdx)
                {
                    clone.Squares[rowIdx, colIdx] = new Square();
                    //if (grid.Squares[rowIdx, colIdx].IsSolved)
                    {
                        clone.Squares[rowIdx, colIdx].SetDigit(grid.Squares[rowIdx, colIdx].Digit);
                    }
                }
            }
            return clone;
        }
    }
}