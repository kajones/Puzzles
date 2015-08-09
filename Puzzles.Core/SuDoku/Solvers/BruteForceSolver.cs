using System.Collections.Generic;
using System.Linq;
using Puzzles.Core.Models.SuDoku;
using Puzzles.Core.SuDoku.Extensions;

namespace Puzzles.Core.SuDoku.Solvers
{
    public class BruteForceSolver : ISolver
    {
        /// <summary>
        /// Given a partially solved grid, try each remaining possibility in turn and backtrack if necessary
        /// </summary>
        /// <param name="grid"></param>
        public void Solve(Grid grid)
        {
            var workingGrid = grid.Clone();
            var unsolvedSquares = GetUnsolvedSquarePossibilities(grid);

            for (var remainingSquareIdx = 0; remainingSquareIdx < unsolvedSquares.Count; ++remainingSquareIdx)
            {
                var foundSquareSolution = false;
                var currentUnsolvedSquare = unsolvedSquares[remainingSquareIdx];
                foreach (var possibleDigit in currentUnsolvedSquare.PossibleDigits)
                {
                    if (possibleDigit >
                        workingGrid.Squares[currentUnsolvedSquare.RowIdx, currentUnsolvedSquare.ColIdx].Digit)
                    {
                        var isValidToSet = workingGrid.EnsureValidToSetTheDigit(currentUnsolvedSquare.RowIdx,
                            currentUnsolvedSquare.ColIdx, possibleDigit, false);
                        if (isValidToSet)
                        {
                            workingGrid.Squares[currentUnsolvedSquare.RowIdx, currentUnsolvedSquare.ColIdx].SetDigit(possibleDigit);
                            foundSquareSolution = true;
                        }
                    }

                    if (foundSquareSolution) break;
                }

                // If have a working solution for the current unsolved square, move to the next
                if (foundSquareSolution) continue;

                // Otherwise roll back
                workingGrid.Squares[currentUnsolvedSquare.RowIdx, currentUnsolvedSquare.ColIdx].ClearDigit();
                remainingSquareIdx = remainingSquareIdx - 2; // Move to the next possibility for the previous unsolved square (for loop moves forward one again)
                if (remainingSquareIdx < -1) break;
            }

            if (!workingGrid.IsSolved) return;

            // Copy the newly solved squares over
            for (var rowIdx = 0; rowIdx < 9; ++rowIdx)
            {
                for (var colIdx = 0; colIdx < 9; ++colIdx)
                {
                    if (!grid.Squares[rowIdx, colIdx].IsSolved)
                    {
                        grid.SetDigit(rowIdx, colIdx, workingGrid.Squares[rowIdx, colIdx].Digit);
                    }
                }
            }
        }

        private static IList<Possibility> GetUnsolvedSquarePossibilities(Grid grid)
        {
            var list = new List<Possibility>();

            for (var rowIdx = 0; rowIdx < 9; ++rowIdx)
            {
                for (var colIdx = 0; colIdx < 9; ++ colIdx)
                {
                    if (grid.Squares[rowIdx, colIdx].IsSolved) continue;

                    list.Add(new Possibility(rowIdx, colIdx, grid.Squares[rowIdx, colIdx].PossibleDigits.ToList()));
                }
            }

            return list;
        }

        public class Possibility
        {
            public int RowIdx { get; private set; }
            public int ColIdx { get; private set; }
            public List<int> PossibleDigits { get; private set; }

            public Possibility(int rowIdx, int colIdx, List<int> possibleDigits)
            {
                RowIdx = rowIdx;
                ColIdx = colIdx;
                PossibleDigits = possibleDigits;
            }
        }
    }
}