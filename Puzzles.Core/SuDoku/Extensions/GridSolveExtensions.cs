using Puzzles.Core.Models.SuDoku;
using Puzzles.Core.SuDoku.Solvers;

namespace Puzzles.Core.SuDoku.Extensions
{
    public static class GridSolveExtensions
    {
        public static void Solve(this Grid grid, ISolver solver, bool useBruteForceFallback = false)
        {
            Solve(grid, new [] { solver }, useBruteForceFallback);
        }

        public static void Solve(this Grid grid, bool useBruteForceFallback = false)
        {
            var solvers = SolverFactory.GetAll();
            Solve(grid, solvers, useBruteForceFallback);
        }

        private static void Solve(Grid grid, ISolver[] solvers, bool useBruteForceFallback)
        {
            var unsolvedSquares = 81;
            var retryCount = 0;

            // Whilst having an effect on the grid i.e. solving at least one square each iteration
            // - not checking for the effect of removing possibilities so give it a retry
            while (retryCount < 2)
            {
                while (grid.UnsolvedSquareCount > 0 && grid.UnsolvedSquareCount < unsolvedSquares)
                {
                    unsolvedSquares = grid.UnsolvedSquareCount;
                    foreach (var solver in solvers)
                    {
                        solver.Solve(grid);
                        grid.Tidy();
                    }
                }

                retryCount++;
            }

            if (grid.IsSolved) return;

            if (!useBruteForceFallback) return;

            var bruteForceSolver = new BruteForceSolver();
            bruteForceSolver.Solve(grid);
        }
    }
}