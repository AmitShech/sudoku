using Sudoku.src.Core.SudokuBoard;
using Sudoku.src.Validation;

namespace Sudoku.src.UI
{
    /// <summary>
    /// The Ui class handles all user interactions: reading input from the user and displaying
    /// the board (both as a grid and as a single string) along with the solution result.
    /// </summary>
    public static class ConsoleUI
    {

        /// <summary>
        /// Reads a board string from the user or from a file.
        /// </summary>
        /// <returns>A string representing the board or null to exit.</returns>
        public static string GetBoardInput()
        {

            Console.Write("\nEnter board or file path (or type 'end' to exit): ");
            string input = Console.ReadLine();

            if (InputValidator.CtrlZHandler(input))
            {
                return input = " ";
            }

            if (input.ToLower() == "end")
            {
                return "end";
            }

            input = FileHandler.OpenFile(input);

            return input;
        }

        /// <summary>
        /// Displays the solution results.
        /// </summary>
        /// <param name="board">The solved (or attempted) board.</param>
        /// <param name="solved">True if the board was solved.</param>
        /// <param name="elapsedTime">The time it took to solve, in milliseconds.</param>
        public static void DisplayResult(Board board, bool solved, long elapsedTime)
        {
            if (solved)
            {
                Console.WriteLine("\nSolved Sudoku Board:");
                BoardPrinter.PrintBoard(board);
                Console.WriteLine($"\nSolution Time: {elapsedTime} ms");

                Console.WriteLine("\nPress 'S' to see the board as a single string, or any other key to continue.");
                string key = Console.ReadLine();

                if (!InputValidator.CtrlZHandler(key))
                {
                    if (key.ToLower() == "s")
                    {
                        BoardPrinter.BoardToString(board);
                    }
                }
            }
            else
            {
                Console.WriteLine("\nNo solution exists for the given Sudoku board.");
                Console.WriteLine($"\nElapsed Time: {elapsedTime} ms");
            }
        }




    }
}





