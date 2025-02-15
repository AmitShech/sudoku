using Sudoku;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public static class Ui
    {

        public static void display()
        {
            Console.WriteLine(" WELCOME TO SUDOKU SOLVER!");
            Console.WriteLine("Please enter a Sudoku board:");
            Console.WriteLine(" - You can enter a string representing the board ");
            Console.WriteLine(" - Or provide a path to a text file (txt) containing the board");

            while (true)
            {
                Console.Write("\nEnter board or file path: ");
                Console.WriteLine("To exit, type 'end'");

                 
                string input = Console.ReadLine();

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

                Board board = new Board(input);
                bool printString=false;

                BoardToGrid(board);
                
                //טיפול בקלט ctrl z ןc

                try
                {
                    //בדיקת תקינות לוח
                    InputValidator.IsValidInput(input);
                    BoardValidator.IsBoardValid(board);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    continue;
                }
                //קריאה לפיתרון
                SudokuSolver solver = new SudokuSolver(board);
                Stopwatch stopwatch = Stopwatch.StartNew();
                bool solved = solver.Solve();
                stopwatch.Stop();

                //הדפסת פיתרון
                if (solved)
                {
                    Console.WriteLine("\nSolved Sudoku Board:");
                    BoardToGrid(board);
                    Console.WriteLine($"\nSolution Time: {stopwatch.ElapsedMilliseconds} ms");

                    Console.WriteLine("\nPress 'S' to see the board as a single string, or any other key to continue.");
                    if (Console.ReadKey().Key == ConsoleKey.S)
                    {
                        printString = true;
                    }

                    if (printString)
                    {
                        BoardToString(board);
                    }
                }
                else
                {
                    Console.WriteLine("\nNo solution exists for the given Sudoku board.");
                    Console.WriteLine($"\nElapsed Time: {stopwatch.ElapsedMilliseconds} ms");
                }

            }

            Console.WriteLine("\nProgram ended. Thank you and goodbye!");
        }

        private static char ValueToAscii(int value)
        {
            if (value == 0) return '_';  
            return (char)('0' + value);  
        }

        private static void BoardToString(Board board)
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


//try
//{
//    InputValidator.IsValidInput(input);


//    SolveBoard solver = new SolveBoard(board);

//    Console.WriteLine("Solving the board...");
//    bool solved = solver.Solve();

//    if (solved)
//    {
//        Console.WriteLine("Sudoku solved successfully! Solution:");
//        board.PrintBoard();
//    }
//    else
//    {
//        Console.WriteLine("No solution exists for the given Sudoku board.");
//    }
//}
//catch (Exception ex)
//{
//    Console.WriteLine("Error: " + ex.Message);
//}



