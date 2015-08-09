using System;
using System.Collections.Generic;
using System.Linq;

namespace Puzzles.Core.Helpers
{
    public class AStarPathHelper
    {
        private readonly long finalRow;
        private readonly long finalCol;

        private readonly long[][] matrix;

        private readonly List<Cell> openCells = new List<Cell>();
        private readonly List<Cell> closedCells = new List<Cell>();

        private readonly long minCostPerSquare;

        public AStarPathHelper(long[][] matrix)
        {
            this.matrix = matrix;

            long numRows = matrix.GetLength(0);
            long numCols = matrix[0].GetLength(0);

            finalRow = numRows - 1;
            finalCol = numCols - 1;

            minCostPerSquare = long.MaxValue;
            foreach (var row in matrix)
            {
                foreach (var col in row)
                {
                    if (col < minCostPerSquare)
                        minCostPerSquare = col;
                }
            }
        }

        /// <summary>
        /// Start bottom right and get to top left using each element a maximum of once and use the minimum value
        /// 
        /// http://www.policyalmanac.org/games/aStarTutorial.htm
        /// </summary>
        /// <returns></returns>
        public long TopLeftBottomRight()
        {
            long currentRow = 0;
            long currentCol = 0;
            openCells.Add(new Cell(currentRow, currentCol) { OriginalValue = matrix[currentRow][currentCol], CostFromStart = matrix[currentRow][currentCol] });

            while (!(closedCells.Any(cell => cell.Row == finalRow && cell.Col == finalCol)))
            {
                var currentCell = openCells.FirstOrDefault(cell => cell.Row == currentRow && cell.Col == currentCol);

                // Find cells that you can move to adjacent to the current position
                var canMoveUpCell = CanMove(currentCell, -1, 0);
                if (canMoveUpCell != null)
                {
                    openCells.Add(canMoveUpCell);
                }
                var canMoveRightCell = CanMove(currentCell, 0, 1);
                if (canMoveRightCell != null)
                {
                    openCells.Add(canMoveRightCell);
                }
                var canMoveDownCell = CanMove(currentCell, 1, 0);
                if (canMoveDownCell != null)
                {
                    openCells.Add(canMoveDownCell);
                }
                var canMoveLeftCell = CanMove(currentCell, 0, -1);
                if (canMoveLeftCell != null)
                {
                    openCells.Add(canMoveLeftCell);
                }

                // Move the parent cell to the closed list
                if (currentCell != null)
                {
                    openCells.Remove(currentCell);
                    closedCells.Add(currentCell);

                    if (currentCell.Row == finalRow && currentCell.Col == finalCol) break;
                }

                if (openCells.Count == 0)
                {
                    Console.WriteLine("ran out of cells");
                    break;
                }

                // Move to the open cell with the lowest F score
                var nextCellScore = openCells.Min(cell => cell.CombinedCost);
                var nextCell = openCells.FirstOrDefault(cell => cell.CombinedCost == nextCellScore);
                if (nextCell == null)
                {
                    Console.WriteLine("failed to find next cell");
                    break;
                }

                currentRow = nextCell.Row;
                currentCol = nextCell.Col;
            }

            // Having found a route work back from the end to use the minimum path
            currentRow = finalRow;
            currentCol = finalCol;
            long path = 0;

            Console.WriteLine("Route: ");
            while (!(currentRow == 0 && currentCol == 0))
            {
                path += matrix[currentRow][currentCol];
                Console.WriteLine("  {0},{1}: {2}", currentRow, currentCol, matrix[currentRow][currentCol]);

                var currentCell = closedCells.FirstOrDefault(cell => cell.Row == currentRow && cell.Col == currentCol);
                if (currentCell == null)
                {
                    Console.WriteLine("Failed to find {0} {1}", currentRow, currentCol);
                    break;
                }

                currentRow = currentCell.ParentRow;
                currentCol = currentCell.ParentCol;
            }

            path += matrix[0][0];
            return path;
        }

        private Cell CanMove(Cell currentCell, int moveRow, int moveCol)
        {
            var proposedNewRow = currentCell.Row + moveRow;
            var proposedNewCol = currentCell.Col + moveCol;

            if (proposedNewRow < 0 || proposedNewRow > finalRow) return null;
            if (proposedNewCol < 0 || proposedNewCol > finalCol) return null;

            if (closedCells.Any(c => c.Row == proposedNewRow && c.Col == proposedNewCol))
                return null;

            var cell = new Cell(proposedNewRow, proposedNewCol) { ParentRow = currentCell.Row, ParentCol = currentCell.Col };
            var value = matrix[proposedNewRow][proposedNewCol];
            cell.OriginalValue = value;
            cell.CostFromStart = currentCell.CostFromStart + value;
            //cell.EstimatedCostToEnd = (finalRow - proposedNewRow) + (finalCol - proposedNewCol); 
            cell.EstimatedCostToEnd = EstimatedCostToBottomRight(proposedNewRow, proposedNewCol);

            var inOpenList = openCells.FirstOrDefault(c => c.Row == proposedNewRow && c.Col == proposedNewCol);
            if (inOpenList != null)
            {
                // Cost for getting here less so use this parent... but don't add twice
                if (cell.CombinedCost < inOpenList.CombinedCost)
                {
                    inOpenList.ParentRow = currentCell.Row;
                    inOpenList.ParentCol = currentCell.Col;
                    inOpenList.OriginalValue = cell.CostFromStart;
                    inOpenList.EstimatedCostToEnd = cell.EstimatedCostToEnd;
                }
                return null;
            }

            return cell;
        }

        private long EstimatedCostToBottomRight(long originalRow, long originalCol)
        {
            var widthToTravel = finalCol - originalCol + 1;
            var heightToTravel = finalRow - originalRow + 1;

            var estimatedTravelCost = ((widthToTravel + heightToTravel) * minCostPerSquare);
            return estimatedTravelCost;
        }
    }

    public class Cell
    {
        public long Row { get; private set; }
        public long Col { get; private set; }

        public long ParentRow { get; set; }
        public long ParentCol { get; set; }

        public long OriginalValue { get; set; }

        /// <summary>
        /// F
        /// </summary>
        public long CombinedCost
        {
            get { return CostFromStart + EstimatedCostToEnd; }
        }

        /// <summary>
        /// G
        /// </summary>
        public long CostFromStart { get; set; }

        /// <summary>
        /// H
        /// </summary>
        public long EstimatedCostToEnd { get; set; }

        public Cell(long row, long col)
        {
            Row = row;
            Col = col;

            ParentRow = -1;
            ParentCol = -1;
        }

        public override string ToString()
        {
            return string.Format("{0},{1} ({2})", Row, Col, OriginalValue);
        }
    }
}