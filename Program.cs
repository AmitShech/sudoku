using Sudoku;
using System;
using System.Diagnostics;

namespace Suduko
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to Sudoku Solver!");
            Console.WriteLine("Please enter a Sudoku Board");
            Console.WriteLine("To exit the program enter 'end'");
            string input = Console.ReadLine();

            while (input != "end")
            {
                try
                {
                    //בדיקת קלט
                    InputValidator.IsValidInput(input);

                    // יצירת אובייקט Board
                    Board board = new Board(input);
                    
                    FillCells.CalculateAllOptions(board);

                    Console.WriteLine("Please choose display type:");
                    Console.WriteLine("enter 1 for graphic display");
                    Console.WriteLine("enter 2 for string display");
                    string displayInput = Console.ReadLine();
                    int choice = (displayInput == "1") ? 1 : 2;

                    if (choice == 1) { board.PrintGraphic(); }
                    if (choice == 2) { board.PrintString(); }

                    // יצירת אובייקט SolveBoard
                    SolveBoard solver = new SolveBoard(board);

                    Stopwatch stopwatch = Stopwatch.StartNew();
                    // ניסיון לפתור את הלוח
                    solver.FillSingleOptionCells();
                    bool solved = solver.Backtracking();

                    stopwatch.Stop();

                    // תוצאה
                    if (solved)
                    {
                        Console.WriteLine("\nSolved Sudoku Board:");
                        if (choice == 1) { board.PrintGraphic(); }
                        if (choice == 2) { board.PrintString(); }
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
                    Console.WriteLine(ex.Message);
                }

                finally
                {
                    Console.WriteLine("Please enter a Sudoku Board");
                    Console.WriteLine("To exit the program enter 'end'");
                    input = Console.ReadLine();
                }
            }

            Console.WriteLine("The program has ended");
        }
    }
}
