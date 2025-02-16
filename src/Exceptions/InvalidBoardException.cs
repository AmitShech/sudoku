namespace Sudoku.src.Exceptions
{
    /// <summary>
    /// Exception thrown when an invalid Sudoku board is detected.
    /// </summary>
    public class InvalidBoardException : Exception
    {
        // <summary>
        /// Initializes a new instance of the InvalidBoardException class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public InvalidBoardException(string message) : base(message) { }
    }
}
