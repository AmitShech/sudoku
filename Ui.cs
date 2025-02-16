using Sudoku;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    /// <summary>
    /// The Ui class handles all user interactions: reading input from the user and displaying
    /// the board (both as a grid and as a single string) along with the solution result.
    /// </summary>
    public static class Ui
    {

        /// <summary>
        /// Reads a board string from the user or from a file.
        /// </summary>
        /// <returns>A string representing the board or null to exit.</returns>
        public static string GetBoardInput()
        {

            Console.Write("\nEnter board or file path (or type 'end' to exit): ");
            string input = Console.ReadLine();

            if (input.Trim().ToLower() == "end")
            {
                return null;
            }

            if (File.Exists(input))
            {
                try
                {
                    input = File.ReadAllText(input);
                    input = input.Replace("\r", "").Replace("\n", "").Replace(" ", "");
                    Console.WriteLine("File loaded successfully.");
                }
                catch (Exception ex)
                {
                    throw new Exception("Error reading file: " + ex.Message);
                }
            }

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
                Ui.BoardToGrid(board);
                Console.WriteLine($"\nSolution Time: {elapsedTime} ms");

                Console.WriteLine("\nPress 'S' to see the board as a single string, or any other key to continue.");
                if (Console.ReadKey().Key == ConsoleKey.S)
                {
                    Ui.BoardToString(board);
                }
            }
            else
            {
                Console.WriteLine("\nNo solution exists for the given Sudoku board.");
                Console.WriteLine($"\nElapsed Time: {elapsedTime} ms");
            }
        }

        /// <summary>
        /// Converts a numeric value to its ASCII representation.
        /// Zero is represented as an underscore ('_').
        /// </summary>
        /// <param name="value">The cell value to convert.</param>
        /// <returns> An underscore if the value is zero; otherwise, the corresponding numeric character.</returns>
        private static char ValueToAscii(int value)
        {
            if (value == 0) return '_';  
            return (char)('0' + value);  
        }

        /// <summary>
        /// Displays the board as a single string representation.
        /// Each cell value is printed on a new line.
        /// </summary>
        /// <param name="board">The board to display.</param>
        public static void BoardToString(Board board)
        {
            Console.WriteLine("\nString representation:");
            
            for (int row = 0; row < board.size; row++)
            {
                for (int col = 0; col < board.size; col++)
                {
                    Console.WriteLine(ValueToAscii(board.cells[row, col].GetValue()));
                }
            }

        }

        /// <summary>
        /// Displays the board in a grid format with borders.
        /// </summary>
        /// <param name="board">The board to display.</param>
        public static void BoardToGrid(Board board)
        {
            int size = board.size;
            int cubeSize = board.cubeSize; 

            int totalWidth = (cubeSize * 2 + 1) * cubeSize + cubeSize - 1;

            string horizontalLine = new string('═', totalWidth);
            string middleSeparator = new string('─', totalWidth);

            Console.WriteLine("╔" + horizontalLine + "╗");

            for (int row = 0; row < size; row++)
            {
                if (row > 0 && row % cubeSize == 0)
                    Console.WriteLine("╠" + middleSeparator + "╣");

                for (int col = 0; col < size; col++)
                {
                    if (col % cubeSize == 0)
                        Console.Write("║ ");

                    int value = board.cells[row, col].GetValue();
                    string cellValue = (value == 0) ? "_" : ValueToAscii(value).ToString();

                    Console.Write(cellValue.PadLeft(1) + " ");
                }
                Console.WriteLine("║");
            }

            Console.WriteLine("╚" + horizontalLine + "╝");
           
        }


    }
}





