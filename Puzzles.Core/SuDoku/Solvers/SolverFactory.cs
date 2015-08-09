namespace Puzzles.Core.SuDoku.Solvers
{
    public static class SolverFactory
    {
        private static readonly ISolver[] solvers;

        static SolverFactory()
        {
            solvers = new ISolver[]
            {
                new UniqueRemainingPossibilityForSquareSolver(), 
                new UniquePossibilityInRowSolver(), 
                new UniquePossibilityInColumnSolver(),
                new UniquePossibilityInBoxSolver(),
                new XWingByColumnSolver(),
                new RowInBoxSolver(),
                new DigitRestrictedToColumnInBoxSolver(), 
                new DigitRestrictedToRowInBoxSolver(), 
                new DigitRestrictedToColumnsInBoxesSolver(), 
                new NakedPairSolver(), 
            };
        }

        public static ISolver[] GetAll()
        {
            return solvers;
        }

        //private void SolveForMatchingPossiblePairInRow()
        //{
        //    //for (var rowIdx = 0; rowIdx < 9; ++rowIdx)
        //    //{
        //    //    var digitPossibilities = new Dictionary<int, List<int>>();
        //    //    for (var colIdx = 0; colIdx < 9; ++colIdx)
        //    //    {
        //    //        if (Squares[rowIdx, colIdx].IsSolved) continue;
        //    //        foreach (var digit in Squares[rowIdx, colIdx].PossibleDigits)
        //    //        {
        //    //            if (!digitPossibilities.ContainsKey(digit))
        //    //            {
        //    //                digitPossibilities.Add(digit, new List<int>());
        //    //            }
        //    //            digitPossibilities[digit].Add(colIdx);
        //    //        }
        //    //    }

        //    //    for (var firstDigit = 1; firstDigit <= 9; ++firstDigit)
        //    //    {
        //    //        if (!digitPossibilities.ContainsKey(firstDigit)) continue;
        //    //        if (digitPossibilities[firstDigit].Count != 2) continue;
        //    //        for (var secondDigit = (firstDigit + 1); secondDigit <= 9; ++secondDigit)
        //    //        {
        //    //            if (!digitPossibilities.ContainsKey(secondDigit)) continue;
        //    //            if (digitPossibilities[secondDigit].Count != 2) continue;
        //    //            var firstLocation = digitPossibilities[firstDigit][0];
        //    //            var secondLocation = digitPossibilities[firstDigit][1];
        //    //            if (digitPossibilities[secondDigit].Contains(firstLocation) &&
        //    //                digitPossibilities[secondDigit].Contains(secondLocation))
        //    //            {
        //    //                // Two digits share the only two same locations -> if they exist as possibilities
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //}

        //private void SolveByLimitedPossibilityWithinSquare()
        //{
        //    //TODO
        //    // If within a large square a given number (although not yet fixed in position)
        //    // can only appear in one row/column then it cannot appear in that row/column
        //    // in the related two large squares
        //}

        //private void SolveByMatchingPossibilityList()
        //{
        //    //TODO: Simple case of two squares with matching candidate list first
        //    // See http://www.paulspages.co.uk/sudoku/howtosolve/ for further approaches
        //    // If within a given row/column/large square there are squares with the same remaining possibilities
        //    // then no other square in the set can use those possibilities
        //    // e.g. for two squares if they both have potential values x & y only then x & y must be placed in those two squares
        //    //      and they can be removed from any other potential list in the same set
        //    // e.g. for three squares then the candidate list of digits must be three in total but not every
        //    //      square has to have all three as remaining possibilities
        //    //      i.e. 1) x,y,z  2) y,z  3) x,z means that between those three squares x,y & z must be placed there
        //    //SolveForMatchingPossiblePairInRow();
        //}

    }
}