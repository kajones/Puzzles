using System;

namespace Puzzles.Core.Helpers
{
    public static class PathHelper
    {
        public static long GetMaximumSum(int[][] triangle)
        {
            for (var row = triangle.GetLength(0) - 2; row >= 0; --row)
            {
                for (var col = triangle[row].Length - 1; col >= 0; --col)
                {
                    var nextRow = row + 1;
                    var nextStepMax = Math.Max(triangle[nextRow][col], triangle[nextRow][col + 1]);
                    triangle[row][col] += nextStepMax;
                }
            }

            return triangle[0][0];
        }

        public static long GetMinimumSumRightDown(int[][] matrix)
        {
            var numRows = matrix.GetLength(0);
            var numCols = matrix[0].GetLength(0);

            for (var row = numRows - 1; row >= 0; --row)
            {
                for (var col = numCols - 1; col >= 0; -- col)
                {
                    var nextRow = row + 1;
                    var nextCol = col + 1;

                    int nextStepMin;

                    if (row == numRows - 1)
                    {
                        if (col == numCols - 1)
                            nextStepMin = 0;
                        else
                        {
                            nextStepMin = matrix[row][nextCol];
                        }
                    }
                    else
                    {
                        if (col == numCols - 1)
                        {
                            nextStepMin = matrix[nextRow][col];
                        }
                        else
                        {
                            nextStepMin = Math.Min(matrix[nextRow][col], matrix[row][nextCol]);
                        }
                    }

                    matrix[row][col] += nextStepMin;
                }
            }

            return matrix[0][0];
        }

        /// <summary>
        /// Up, down and right are only poss movements
        /// Start anywhere on left
        /// Finish anywhere on right
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static long LeftToRight(int[][] matrix)
        {
            const int tooBig = 1000000;

            var numRows = matrix.GetLength(0);
            var numCols = matrix[0].GetLength(0);

            for (var col = numCols - 2; col >= 0; --col)
            {
                var nextCol = col + 1;

                var origColValues = new int[numRows];
                for (var row = 0; row < numRows; ++row)
                {
                    origColValues[row] = matrix[row][col];
                }

                // Evaluate from top to bottom looking at the possibility of going up (to the cell just evaluated)
                // or right (to the cell evaluated in the previous loop)
                // Then evaluate from bottom to top looking at the possibility of going down (to the cell just evaluated)
                // and take the better value from the first or second evaluation

                for (var row = 0; row <= numRows - 1; ++row)
                {
                    var cellAbove = (row > 0) ? matrix[row - 1][col] : tooBig;

                    var cellToRight = (col < numCols - 1) ? matrix[row][nextCol] : tooBig;

                    var minMove = (int) Math.Min(cellAbove, cellToRight);

                    matrix[row][col] += minMove;
                }

                for (var row = numRows - 1; row >= 0; --row)
                {
                    var cellBelow = (row < numRows - 1) ? matrix[row + 1][col] : tooBig;

                    var goingDown = origColValues[row] + cellBelow;

                    matrix[row][col] = Math.Min(goingDown, matrix[row][col]);
                }
            }

            var minLeftCol = long.MaxValue;
            for (var row = 0; row < numRows; ++row)
            {
                if (matrix[row][0] < minLeftCol)
                    minLeftCol = matrix[row][0];
            }

            return minLeftCol;
        }

        public static long OldTopLeftBottomRight(long[][] matrix)
        {
            const long cannotGo = 1000000000;

            var numRows = matrix.GetLength(0);
            var numCols = matrix[0].GetLength(0);

            var usedFlags = new bool[numRows][];
            for (var row = 0; row < numRows; ++row)
            {
                usedFlags[row] = new bool[numCols];
            }

            var currentRow = numRows - 1;
            var currentCol = numCols - 1;

            var step = 1;

            long path = matrix[currentRow][currentCol];
            usedFlags[currentRow][currentCol] = true;
            matrix[currentRow][currentCol] = step;

            while (! (currentRow == 0 && currentCol == 0))
            {
                var previousRow = currentRow - 1;
                var nextRow = currentRow + 1;
                var previousCol = currentCol - 1;
                var nextCol = currentCol + 1;

                var canGoUp = ((currentRow > 0) && (!usedFlags[previousRow][currentCol]));
                var moveUp = canGoUp ? matrix[previousRow][currentCol] : cannotGo;

                var canGoLeft = ((currentCol > 0) && (!usedFlags[currentRow][previousCol]));
                var moveLeft = canGoLeft ? matrix[currentRow][previousCol] : cannotGo;

                var canGoDown = ((currentRow < numRows - 1) && (!usedFlags[nextRow][currentCol]));
                var moveDown = canGoDown ? matrix[nextRow][currentCol] : cannotGo;

                var canGoRight = ((currentCol < numCols - 1) && (!usedFlags[currentRow][nextCol]));
                var moveRight = canGoRight ? matrix[currentRow][nextCol] : cannotGo;

                if (!canGoUp && !canGoLeft && !canGoDown && !canGoRight)
                {
                    Console.WriteLine("In a dead end!");
                    break;
                }

                var minMove = Math.Min(moveUp, Math.Min(moveLeft, Math.Min(moveDown, moveRight)));
                if (minMove == cannotGo)
                {
                    Console.WriteLine("Likely error - blocked");
                    break;
                }

                step++;
                matrix[currentRow][currentCol] = step;
                if (minMove == moveLeft)
                {
                    currentCol = previousCol;
                }
                else if (minMove == moveUp)
                {
                    currentRow = previousRow;
                }
                else if (minMove == moveRight)
                {
                    currentCol = nextCol;
                }
                else
                {
                    currentRow = nextRow;
                }

                usedFlags[currentRow][currentCol] = true;
                path += matrix[currentRow][currentCol];
            }

            return path;
        }
    }
}
