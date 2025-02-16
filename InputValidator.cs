using System;

namespace Sudoku
{
    /// <summary>
    /// Provides methods to validate the raw input string for the Sudoku board.
    /// This includes checking for empty input, correct board size, and valid characters.
    /// </summary>
    public static class InputValidator
    {
        /// <summary>
        /// Validates the raw input string that represents a Sudoku board.
        /// Checks for empty input, correct board size (forming a square board and valid cube),
        /// and that the input contains only valid digit characters.
        /// </summary>
        /// <param name="input">The raw input string to validate.</param>
        /// <returns>True if the input is valid; otherwise, false.</returns>
        public static bool IsValidInput(string input)
        {
            try
            {
                EmptyInput(input);
                int length = input.Length;
                int size = (int)Math.Sqrt(length);
                CorrectSize(input, size);
                validChars(input, size);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks that the input string contains only digit characters and that each digit
        /// is within the valid range (0 to the board size).
        /// Throws an InputException if an invalid character or number is found.
        /// </summary>
        /// <param name="input">The input string to check.</param>
        /// <param name="size">The calculated board size (number of rows or columns).</param>
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

        /// <summary>
        /// Validates that the length of the input string forms a square board and that the board's
        /// dimensions can form a valid cube (subgrid).
        /// Throws an InputException if these conditions are not met.
        /// </summary>
        /// <param name="input">The input string representing the board.</param>
        /// <param name="size">The calculated board size (number of rows or columns).</param>

        private static void CorrectSize(string input,int size)
        {
            if (size * size != input.Length)
                throw new InputException("The input length does not form a square board.");

            int cube = (int)Math.Sqrt(size);
            if (cube * cube != size)
                throw new InputException("The board size does not form a valid cube.");
        }

        /// <summary>
        /// Checks that the input string is not null, empty, or composed solely of whitespace.
        /// Throws an InputException if the input is empty.
        /// </summary>
        /// <param name="input">The input string to check.</param>
        private static void EmptyInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new InputException("Input is empty.");
        }

        /// <summary>
        /// Attaches an event handler to the Console.CancelKeyPress event.
        /// This allows the application to gracefully handle Ctrl+C (cancel) events.
        /// </summary>
        public static void CtrlCHandler()
        {
            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true;
                Console.WriteLine("\nProgram ended.");
                Environment.Exit(0);
            };
        }
    }
}
