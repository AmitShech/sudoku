using System;

namespace Sudoku.Exceptions
{
    public class InputException : Exception
    {
        public InputException(string message) : base(message) { }
    }

    public class InvalidBoardException : Exception
    {
        public InvalidBoardException(string message) : base(message) { }
    }

    public class UnsolveableBoard : Exception
    {
        public UnsolveableBoard(string message) : base(message) { }
    }
}
