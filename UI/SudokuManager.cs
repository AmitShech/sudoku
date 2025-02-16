using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Solver;

namespace Sudoku.UI
{

    /// <summary>
    /// The entry point of the application. This class orchestrates the program flow:
    /// reading input, validating, solving the board, and displaying results.
    /// </summary>

    public static class SudokuManager
    {
        /// <summary>
        /// Main method: repeatedly gets board input from the user, validates it,
        /// creates a board, solves it, and displays the result.
        /// </summary>
        public static void Run()
        {
            InputValidator.CtrlCHandler();

            Console.WriteLine("WELCOME TO SUDOKU SOLVER!");
            Console.WriteLine("Please enter a Sudoku board:");
            Console.WriteLine(" - You can enter a string representing the board");
            Console.WriteLine(" - Or provide a path to a text file (txt) containing the board");

            while (true)
            {

                string input = Ui.GetBoardInput();
                if (input == "end")
                    break;

                if (!InputValidator.IsValidInput(input)) { continue; }

                Board board = new Board(input);

                if (!BoardValidator.IsBoardValid(board)) { continue; }

                Ui.PrintGrid(board);


                Stopwatch stopwatch = Stopwatch.StartNew();

                SudokuSolver solver = new SudokuSolver(board);
                bool solved = solver.Solve();

                stopwatch.Stop();
                long elapsedTime = stopwatch.ElapsedMilliseconds;

                Ui.DisplayResult(board, solved, elapsedTime);
            }

            Console.WriteLine("\nProgram ended. Thank you and goodbye!");
        }
    }
}
