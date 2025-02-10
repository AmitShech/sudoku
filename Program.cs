using Sudoku;
using System;
using System.Diagnostics;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Sudoku Solver!");
            Console.WriteLine("Please enter a Sudoku Board (use 0 for empty cells).");
            Console.WriteLine("Enter 'end' to exit.");

            string input = Console.ReadLine();
            while (input != "end")
            {
                try
                {
                    InputValidator.IsValidInput(input);
                    Board board = new Board(input);

                    var solver = new SudokuSolver(board);
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    bool solved = solver.Solve();
                    stopwatch.Stop();

                    if (solved)
                    {
                        Console.WriteLine("\nSolved Sudoku Board:");
                        Console.WriteLine(board.GetBoardString());
                        Console.WriteLine($"\nSolution Time: {stopwatch.ElapsedMilliseconds} ms");
                    }
                    else
                    {
                        Console.WriteLine("\nNo solution exists for the given Sudoku board.");
                        Console.WriteLine($"\nElapsed Time: {stopwatch.ElapsedMilliseconds} ms");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                Console.WriteLine("\nPlease enter a Sudoku Board (or 'end' to exit):");
                input = Console.ReadLine();
            }
            Console.WriteLine("The program has ended.");
        }
    }
}
