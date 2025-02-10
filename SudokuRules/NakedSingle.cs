using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.SudokuRules
{
    /// <summary>
    /// Implements the "Naked Single" rule for solving Sudoku.
    /// A naked single occurs when a cell has only one possible candidate value.
    /// </summary>
    public class NakedSingle : ISudokuRule
    {
        /// <summary>
        /// Applies the "Naked Single" rule to the given Sudoku board.
        /// </summary>
        /// <param name="board">The Sudoku board to process.</param>
        /// <returns>True if any cell was changed, otherwise false.</returns>
        public bool Apply(Board board)
        {
            bool changed = false;

            foreach (var row in board.rows)
            {
                foreach (var cell in row.GetEmptyCells())
                {
                    if (cell.GetPossibleOptionsCount() == 1)
                    {
                        int value = cell.GetPossibleOptions()[0];
                        board.SetValue(cell.row, cell.col, value);  
                        changed = true;
                    }
                }
            }

            return changed;
        }
    }
}
