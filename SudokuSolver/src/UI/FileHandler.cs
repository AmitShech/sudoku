using Sudoku.src.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.src.UI
{
    public static class FileHandler
    {
        public static string OpenFile(string input)
        {
            if (File.Exists(input))
            {
                try
                {
                    input = File.ReadAllText(input);
                    input = input.Replace("\r", "").Replace("\n", "").Replace(" ", "");
                    Console.WriteLine("File loaded successfully.");
                }
                catch (FileReadException ex)
                {
                    throw new FileReadException("Error reading file: " + ex.Message);
                }
            }

            return input;
        }
    }
}
