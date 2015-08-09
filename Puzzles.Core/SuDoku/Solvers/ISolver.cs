using Puzzles.Core.Models.SuDoku;

namespace Puzzles.Core.SuDoku.Solvers
{
    public interface ISolver
    {
        void Solve(Grid grid);
    }
}