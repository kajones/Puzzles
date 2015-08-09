using System;
using System.Collections.Generic;
using Puzzles.Core.Models.SuDoku;

namespace Puzzles.Core.SuDoku.Extensions
{
    public static class GridBoxLimitExtensions
    {
        public static int GetBoxStart(this Grid grid, int idx)
        {
            // Integer division so rounds down
            return (idx / 3) * 3;
        }

        public static int GetBoxEnd(this Grid grid, int startIdx)
        {
            return startIdx + 2;
        }

        public static Dictionary<int, Tuple<int, int>> GetBoxRows(this Grid grid)
        {
            var firstRowTopBox = grid.GetBoxStart(0);
            var lastRowTopBox = grid.GetBoxEnd(firstRowTopBox);
            var firstRowMiddleBox = grid.GetBoxStart(3);
            var lastRowMiddleBox = grid.GetBoxEnd(firstRowMiddleBox);
            var firstRowBottomBox = grid.GetBoxStart(6);
            var lastRowBottomBox = grid.GetBoxEnd(firstRowBottomBox);

            var boxRows = new Dictionary<int, Tuple<int, int>>
            {
                {1, new Tuple<int, int>(firstRowTopBox, lastRowTopBox)},
                {2, new Tuple<int, int>(firstRowMiddleBox, lastRowMiddleBox)},
                {3, new Tuple<int, int>(firstRowBottomBox, lastRowBottomBox)}
            };
            return boxRows;
        }

        public static Dictionary<int, Tuple<int, int>> GetBoxColumns(this Grid grid)
        {
            var firstColLeftBox = grid.GetBoxStart(0);
            var lastColLeftBox = grid.GetBoxEnd(firstColLeftBox);
            var firstColMiddleBox = grid.GetBoxStart(3);
            var lastColMiddleBox = grid.GetBoxEnd(firstColMiddleBox);
            var firstColRightBox = grid.GetBoxStart(6);
            var lastColRightBox = grid.GetBoxEnd(firstColRightBox);

            var boxCols = new Dictionary<int, Tuple<int, int>>
            {
                {1, new Tuple<int, int>(firstColLeftBox, lastColLeftBox)},
                {2, new Tuple<int, int>(firstColMiddleBox, lastColMiddleBox)},
                {3, new Tuple<int, int>(firstColRightBox, lastColRightBox)}
            };
            return boxCols;
        }
    }
}