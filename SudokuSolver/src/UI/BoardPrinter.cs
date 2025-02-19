using Sudoku.src.Core.SudokuBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.src.UI
{
    public static class BoardPrinter
    {
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
                    Console.Write(ValueToAscii(board.cells[row, col].GetValue()));
                }
            }
        }

        /// <summary>
        /// Displays the board in a grid format with borders.
        /// </summary>
        /// <param name="board">The board to display.</param>
        public static void PrintBoard(Board board)
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
                    string cellValue = value == 0 ? "_" : ValueToAscii(value).ToString();

                    Console.Write(cellValue.PadLeft(1) + " ");
                }
                Console.WriteLine("║");
            }

            Console.WriteLine("╚" + horizontalLine + "╝");

        }
    }
}
