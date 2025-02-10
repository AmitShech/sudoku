using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.SudokuRules;

namespace Sudoku
{
    public class SudokuSolver
    {
        private readonly Board board;
        private readonly List<ISudokuRule> heuristics;


        /// <summary>
        /// Initializes a new instance of the SudokuSolver class with a given board.
        /// </summary>
        /// <param name="board">The Sudoku board to solve.</param>
        public SudokuSolver(Board board)
        {
            this.board = board;
            
            heuristics = new List<ISudokuRule>
            {
                new NakedSingle(),
                new HiddenSingle(),
                new NakedGeneric()  
            };

        }

        /// <summary>
        /// Attempts to solve the board.
        /// </summary>
        /// <returns>True if the board is solved; otherwise false.</returns>
        public bool Solve()
        {
           
            bool progress = true;
            while (progress)
            {
                progress = ApplyHeuristics();

                if (!progress)
                {
                    return Backtracking();
                }
            }
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
            Cell cell = FindCellWithFewestOptions();
            if (cell == null) return true;

            int originalOptions = cell.possibleOptionsMask;
            var options = cell.GetPossibleOptions();

            var masksBackup = SaveMasks();

            foreach (int value in options)
            {
                try
                {
                    board.SetValue(cell.row, cell.col, value);
                    board.CalculateAllOptions();

                    if (Solve())
                        return true;
                }
                catch (UnsolveableBoard)
                {
                }

                RestoreMasks(masksBackup);
                board.ClearValue(cell.row, cell.col);
                cell.SetOptions(originalOptions);
            }
            return false;
        }

        /// <summary>
        /// Saves the current state of row, column, and cube masks before modifying the board.
        /// </summary>
        /// <returns>A tuple containing masks for rows, columns, and cubes.</returns>
        private (int[] rows, int[] cols, int[] cubes) SaveMasks()
        {
            int[] rowMasks = new int[board.size];
            int[] colMasks = new int[board.size];
            int[] cubeMasks = new int[board.size];

            for (int i = 0; i < board.size; i++)
            {
                rowMasks[i] = board.rows[i].GetMask();
                colMasks[i] = board.cols[i].GetMask();
                cubeMasks[i] = board.cubes[i].GetMask();
            }

            return (rowMasks, colMasks, cubeMasks);
        }


        /// <summary>
        /// Restores the previously saved row, column, and cube masks.
        /// </summary>
        /// <param name="masks">A tuple containing the saved masks.</param>
        private void RestoreMasks((int[] rows, int[] cols, int[] cubes) masks)
        {
            for (int i = 0; i < board.size; i++)
            {
                board.rows[i].SetMask(masks.rows[i]);
                board.cols[i].SetMask(masks.cols[i]);
                board.cubes[i].SetMask(masks.cubes[i]);
            }
        }

        /// <summary>
        /// Finds the empty cell with the fewest possible values to reduce backtracking complexity.
        /// </summary>
        /// <returns>The cell with the fewest possible options, or null if no empty cells remain.</returns>
        /// <exception cref="UnsolveableBoard">Thrown if a cell has no valid options.</exception>
        private Cell FindCellWithFewestOptions()
        {
            Cell bestCell = null;
            int minOptions = board.size + 1;
            foreach (var cell in board.cells)
            {
                if (cell.IsEmpty())
                {
                    int count = cell.GetPossibleOptionsCount();
                    if (count == 0)
                        throw new UnsolveableBoard("A cell has no options left.");
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
