using Sudoku;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public static class Ui
    {

        public static void display()
        {
            Console.WriteLine("Welcome to Sudoku Solver!");
            Console.WriteLine("Please enter a Sudoku board:");
            Console.WriteLine(" - You can enter a string representing the board (e.g., 81 characters for 9x9)");
            Console.WriteLine(" - Or provide a path to a text file (txt) containing the board");

            while (true)
            {
                Console.Write("\nEnter board or file path: ");
                Console.WriteLine("To exit, type 'end'");
                string input = Console.ReadLine()?.Trim();

                if (input.ToLower() == "end")
                    break;

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
                        Console.WriteLine("Error reading file: " + ex.Message);
                        continue;
                    }
                }

                try
                {
                    InputValidator.IsValidInput(input);

                    Board board = new Board(input);
                    SolveBoard solver = new SolveBoard(board);

                    Console.WriteLine("Solving the board...");
                    bool solved = solver.Solve();

                    if (solved)
                    {
                        Console.WriteLine("Sudoku solved successfully! Solution:");
                        board.PrintBoard();
                    }
                    else
                    {
                        Console.WriteLine("No solution exists for the given Sudoku board.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

            }

            Console.WriteLine("\nProgram ended. Thank you and goodbye!");
        }
    }
}
