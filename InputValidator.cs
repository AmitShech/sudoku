using System;

namespace Sudoku
{
    public static class InputValidator
    {
        public static void IsValidInput(string input)
        {
            EmptyInput(input);
            int length = input.Length;
            int size = (int)Math.Sqrt(length);
            CorrectSize(input,size);
            validChars(input, size);
        }

        private static void validChars(string input, int size)
        {
            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                    throw new InputException("The input contains non-digit characters.");

                int num = c - '0';
                if (num < 0 || num > size)
                    throw new InputException($"The input contains an invalid number: {num}.");
            }
        }

        private static void CorrectSize(string input,int size)
        {
            if (size * size != input.Length)
                throw new InputException("The input length does not form a square board.");

            int cube = (int)Math.Sqrt(size);
            if (cube * cube != size)
                throw new InputException("The board size does not form a valid cube.");
        }

        private static void EmptyInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new InputException("Input is empty.");
        }

    }
}
