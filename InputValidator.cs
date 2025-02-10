using System;

namespace Sudoku
{
    public static class InputValidator
    {
        public static bool IsValidInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new InputException("Input is empty.");

            int length = input.Length;
            int size = (int)Math.Sqrt(length);

            if (size * size != length)
                throw new InputException("The input length does not form a square board.");

            int cube = (int)Math.Sqrt(size);
            if (cube * cube != size)
                throw new InputException("The board size does not form a valid square region (cube).");

            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                    throw new InputException("The input contains non-digit characters.");

                int num = c - '0';
                if (num < 0 || num > size)
                    throw new InputException($"The input contains an invalid number: {num}.");
            }
            return true;
        }

        public static bool 
    }
}
