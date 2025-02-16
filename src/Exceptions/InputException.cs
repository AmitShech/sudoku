namespace Sudoku.src.Exceptions
{
    /// <summary>
    /// Exception thrown when an invalid input is encountered in the Sudoku application.
    /// </summary>
    public class InputException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the InputException class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public InputException(string message) : base(message) { }
    }
}
