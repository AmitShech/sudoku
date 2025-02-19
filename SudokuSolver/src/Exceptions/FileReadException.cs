namespace Sudoku.src.Exceptions
{
    /// <summary>
    /// Exception thrown when an error occurs while reading a file in the Sudoku application.
    /// </summary>
    public class FileReadException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the FileReadException class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public FileReadException(string message)
            : base(message) { }
    }
}
