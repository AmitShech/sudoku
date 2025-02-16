using Sudoku.src.Core.SudokuBoard;

namespace Sudoku.src.Core.Solver.SudokuStrategies
{
    /// <summary>
    /// Interface for a Sudoku rule/technique.
    /// </summary>
    public interface ISudokuRule
    {
        /// <summary>
        /// Applies the rule to the board.
        /// Returns true if the board was modified.
        /// </summary>
        bool Apply(Board board);
    }
}

