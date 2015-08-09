using System.Collections.Generic;
using System.Linq;
using Puzzles.Core.Models.SuDoku;

namespace Puzzles.Core.SuDoku.Solvers
{
    /// <summary>
    /// If there are two squares that both have the same two possibilities (and no other possibilities) in the same row/square/box
    /// then even though you don't know which square has which digit, you know that you can remove those two digits
    /// from all the other squares in that row/square/box
    /// 
    /// e.g. (1,7) (6,7,9) (1,6,7,9) (1,7) (1,4,6) (2,3,6,7) (3,4,6,8,9) (2,3,4,6,8) 5
    /// 
    /// - the pair (1,7) exists twice so therefore no other square can contain 1 or 7
    /// </summary>
    public class NakedPairSolver : ISolver
    {
        public void Solve(Grid grid)
        {
            NakedPairByRow(grid);
        }

        private static void NakedPairByRow(Grid grid)
        {
            for (var rowIdx = 0; rowIdx < 9; ++rowIdx)
            {
                var possibilitiesInRow = new Dictionary<int, List<int>>();
                for (var colIdx = 0; colIdx < 9; ++colIdx)
                {
                    if (grid.Squares[rowIdx, colIdx].IsSolved) continue;
                    var listOfPossibilitiesForSquare = grid.Squares[rowIdx, colIdx].PossibleDigits.ToList();
                    if (listOfPossibilitiesForSquare.Count != 2) continue;

                    possibilitiesInRow.Add(colIdx, listOfPossibilitiesForSquare);
                }

                if (possibilitiesInRow.Count < 2) continue;
                foreach (var squareWithPairOfPossibilities in possibilitiesInRow)
                {
                    var firstDigit = squareWithPairOfPossibilities.Value[0];
                    var secondDigit = squareWithPairOfPossibilities.Value[1];

                    foreach (var anotherSquareWithPairOfPossibilities in possibilitiesInRow)
                    {
                        if (anotherSquareWithPairOfPossibilities.Key == squareWithPairOfPossibilities.Key) continue;
                        if (!anotherSquareWithPairOfPossibilities.Value.Contains(firstDigit)) continue;
                        if (!anotherSquareWithPairOfPossibilities.Value.Contains(secondDigit)) continue;

                        // Found two squares that share the same two possibilities - so remove from other squares in the row
                        for (var colIdx = 0; colIdx < 9; ++ colIdx)
                        {
                            if (colIdx == squareWithPairOfPossibilities.Key) continue;
                            if (colIdx == anotherSquareWithPairOfPossibilities.Key) continue;

                            if (grid.Squares[rowIdx, colIdx].IsSolved) continue;
                            grid.Squares[rowIdx, colIdx].RemovePossibleDigit(firstDigit);
                            grid.Squares[rowIdx, colIdx].RemovePossibleDigit(secondDigit);
                        }
                    }
                }
            }

            
        }
    }
}