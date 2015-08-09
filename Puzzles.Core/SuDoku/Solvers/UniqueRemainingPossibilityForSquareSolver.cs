using System.Linq;
using Puzzles.Core.Models.SuDoku;
using Puzzles.Core.SuDoku.Extensions;

namespace Puzzles.Core.SuDoku.Solvers
{
    public class UniqueRemainingPossibilityForSquareSolver : ISolver
    {
        public void Solve(Grid grid)
        {
            for (var rowIdx = 0; rowIdx < 9; ++rowIdx)
            {
                for (var colIdx = 0; colIdx < 9; ++colIdx)
                {
                    if (grid.Squares[rowIdx, colIdx].IsSolved) continue;
                    if (grid.Squares[rowIdx, colIdx].PossibleDigits.Count != 1) continue;

                    var digitToSet = grid.Squares[rowIdx, colIdx].PossibleDigits.First();
                    grid.SetDigit(rowIdx, colIdx, digitToSet);
                }
            }
        }
    }
}