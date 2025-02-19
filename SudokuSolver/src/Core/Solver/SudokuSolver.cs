using Sudoku.src.Core.Solver.SudokuStrategies;
using Sudoku.src.Core.SudokuBoard;


namespace Sudoku.src.Core.Solver
{
    /// <summary>
    /// The SudokuSolver class is responsible for solving a given Sudoku board.
    /// It applies a set of heuristic rules to simplify the board and, if needed, uses
    /// backtracking to search for a solution.
    /// </summary>
    public class SudokuSolver
    {
        private readonly Board board;
        private readonly List<ISudokuRule> heuristics;
        private readonly NakedGeneric rule;
        public static int RecIterations = 0;

        /// <summary>
        /// Initializes a new instance of the SudokuSolver class with a given board.
        /// </summary>
        /// <param name="board">The Sudoku board to solve.</param>
        public SudokuSolver(Board board)
        {   
            this.board = board;

            rule = new NakedGeneric();

            heuristics = new List<ISudokuRule>
            {
                new NakedSingle(),
                new HiddenSingle(),
                new NakedGeneric(),
            };

        }

        /// <summary>
        /// Attempts to solve the board.
        /// </summary>
        /// <returns>True if the board is solved; otherwise false.</returns>
        public bool Solve()
        {
            bool progress = true;

            while (progress && BoardValidator.IsSolvable(board))
            {
                progress = ApplyHeuristics();
            }
            //rule.Apply(board);

            if (!BoardValidator.IsSolvable(board))
                return false;

            return Backtracking();
        }

        /// <summary>
        /// Repeatedly apply the available rules until no further progress can be made.
        /// </summary>
        /// <summary>
        /// Iterates through each heuristic rule to update the board.
        /// </summary>
        private bool ApplyHeuristics()
        {
            bool madeProgress = false;
            foreach (var rule in heuristics)
            {
                if (rule.Apply(board))
                {
                    madeProgress = true;
                }
            }
            return madeProgress;
        }

        /// <summary>
        /// Backtracking search for a solution.
        /// </summary>
        private bool Backtracking()
        {
            RecIterations++;
            Cell cell = FindCellWithFewestOptions();
            if (cell == null) return true;

            int originalOptions = cell.possibleOptionsMask;
            var options = cell.GetPossibleOptions();

            var stateBackup = SaveState();

            foreach (int value in options)
            {
                board.SetValue(cell.row, cell.col, value);
                board.CalculateAllOptions();

                if (Solve())
                    return true;

                cell.RemoveOption(value);
                RestoreState(stateBackup);
                board.CalculateAllOptions();
            }
            return false;
        }

        /// <summary>
        /// Saves the current state of the board, including:
        /// - The masks for each row, column, and cube,
        /// - The possible options mask and value for each cell.
        /// </summary>
        /// <returns>
        /// A tuple containing:
        /// - An array of row masks,
        /// - An array of column masks,
        /// - An array of cube masks,
        /// - A 2D array of cell options masks,
        /// - A 2D array of cell values (0 if the cell is empty).
        /// </returns>
        private (int[] rowMasks, int[] colMasks, int[] cubeMasks, int[,] cellOptions, int[,] cellValues) SaveState()
        {
            int size = board.size;
            int[] rowMasks = new int[size];
            int[] colMasks = new int[size];
            int[] cubeMasks = new int[size];

            for (int i = 0; i < size; i++)
            {
                rowMasks[i] = board.rows[i].GetMask();
                colMasks[i] = board.cols[i].GetMask();
                cubeMasks[i] = board.cubes[i].GetMask();
            }

            int[,] cellOptions = new int[size, size];
            int[,] cellValues = new int[size, size];
            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    cellOptions[r, c] = board.cells[r, c].possibleOptionsMask;
                    cellValues[r, c] = board.cells[r, c].GetValue();
                }
            }

            return (rowMasks, colMasks, cubeMasks, cellOptions, cellValues);
        }


        /// <summary>
        /// Restores the board state from a previously saved state.
        /// It restores:
        /// - The masks for rows, columns, and cubes,
        /// - The state of each cell: if the saved value is 0, the cell is cleared and its options mask is restored;
        ///   otherwise, the cell's value is set to the saved value.
        /// </summary>
        /// <param name="state">
        /// A tuple containing:
        /// - An array of row masks,
        /// - An array of column masks,
        /// - An array of cube masks,
        /// - A 2D array of cell options masks,
        /// - A 2D array of cell values.
        /// </param>
        private void RestoreState((int[] rowMasks, int[] colMasks, int[] cubeMasks, int[,] cellOptions, int[,] cellValues) state)
        {
            int size = board.size;
            for (int i = 0; i < size; i++)
            {
                board.rows[i].SetMask(state.rowMasks[i]);
                board.cols[i].SetMask(state.colMasks[i]);
                board.cubes[i].SetMask(state.cubeMasks[i]);
            }

            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    if (state.cellValues[r, c] == 0)
                    {
                        board.cells[r, c].ClearValue();
                        board.cells[r, c].SetOptions(state.cellOptions[r, c]);
                    }
                    else
                    {
                        board.cells[r, c].SetValue(state.cellValues[r, c]);
                    }
                }
            }
        }

        /// <summary>
        /// Finds the empty cell with the fewest possible values to reduce backtracking complexity.
        /// </summary>
        /// <returns>The cell with the fewest possible options, or null if no empty cells remain.</returns>
        private Cell FindCellWithFewestOptions()
        {
            Cell bestCell = null;
            int minOptions = board.size + 1;
            foreach (var cell in board.cells)
            {
                if (cell.IsEmpty())
                {
                    int count = cell.GetPossibleOptionsCount();
                    if (count == 1)
                        return cell;
                    if (count < minOptions)
                    {
                        minOptions = count;
                        bestCell = cell;
                    }
                }
            }
            return bestCell;
        }

    }

}
