using Sudoku.src.Core.SudokuBoard;

namespace Sudoku.src.Core.Solver.SudokuStrategies
{
    /// <summary>
    /// Class that implements the "Hidden Single" rule for solving Sudoku.
    /// A hidden single is a digit that appears as a possibility in only one cell of a row, column, or box.
    /// </summary>
    public class HiddenSingle : ISudokuRule
    {

        /// <summary>
        /// Applies the "Hidden Single" rule to the given Sudoku board.
        /// </summary>
        /// <param name="board">The Sudoku board to process.</param>
        /// <returns>True if any cell was changed, otherwise false.</returns>
        public bool Apply(Board board)
        {
            bool changed = false;

            for (int value = 1; value <= board.size; value++)
            {
                foreach (var group in board.rows)
                {
                    if (ApplyHiddenSingleInGroup(board, group, value))
                        changed = true;
                }
                foreach (var group in board.cols)
                {
                    if (ApplyHiddenSingleInGroup(board, group, value))
                        changed = true;
                }
                foreach (var group in board.cubes)
                {
                    if (ApplyHiddenSingleInGroup(board, group, value))
                        changed = true;
                }
            }
            return changed;
        }

        /// <summary>
        /// Identifies and applies the "Hidden Single" rule within a specific group (row, column, or box).
        /// If a number has only one possible cell where it can be placed, it is assigned to that cell.
        /// </summary>
        /// <param name="board">The Sudoku board being solved.</param>
        /// <param name="group">A group of cells (row, column, or box).</param>
        /// <param name="value">The value being checked.</param>
        /// <returns>True if a value was placed, otherwise false.</returns>
        private bool ApplyHiddenSingleInGroup(Board board, CellGroup group, int value)
        {
            int count = 0;
            Cell targetCell = null;

            foreach (var cell in group.GetEmptyCells())
            {
                if (cell.HasOption(value))
                {
                    count++;
                    targetCell = cell;
                }
            }

            if (count == 1 && targetCell != null)
            {
                board.SetValue(targetCell.row, targetCell.col, value);
                return true;
            }
            return false;
        }
    }
}
