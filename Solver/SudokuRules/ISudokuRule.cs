using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Solver.SudokuRules
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

