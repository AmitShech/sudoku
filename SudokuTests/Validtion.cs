using Xunit;
using Sudoku.src.Validation;
using Sudoku.src.Exceptions;
using System.Collections.Generic;
using Sudoku.src.Core.SudokuBoard;

namespace SudokuTests
{
    public class InputValidatorTests
    {
        /// <summary>
        /// A collection of test cases for the IsValidInput method.
        /// Each object[] element represents: [string input, bool expected, string message].
        /// </summary>
        public static IEnumerable<object[]> IsValidInputTestData =>
            new List<object[]>
            {
                new object[] { "", false, "Empty input should be invalid." },
                new object[] { "   ", false, "Whitespace-only input should be invalid." },
                new object[] { "1234567890", false, "Non-square length should be invalid." },
                new object[] { "1234512345123451234512345", false, "Non-cube board should be invalid." },
                new object[] { new string('0', 81), true, "81 zeros should form a valid 9x9 board." },
                new object[] { "12A4567890", false, "'A' in 10-length string is invalid." },
                new object[]
                {
                    "123456789" + "123456789" + "123456789" +
                    "123456789" + "123456789" + "123456789" +
                    "123456789" + "123456789" + "12345678A",
                    false,
                    "81 chars but includes 'A', invalid for 9x9."
                },
                new object[]
                {
                    new string('0', 255) + ";",
                    true,
                    "Assuming in 16x16 ';' is considered valid."
                }
            };

        /// <summary>
        /// A single test that runs IsValidInput with all the test cases from IsValidInputTestData.
        /// </summary>
        [Theory]
        [MemberData(nameof(IsValidInputTestData))]
        public void IsValidInput_VariousCases(string input, bool expected, string message)
        {
            bool result = InputValidator.IsValidInput(input);

            Assert.Equal(expected, result);
        }

        /// <summary>
        /// A collection of test cases for the IsValid method (BoardValidator).
        /// Each object[] represents: [ string input, bool expected, string description ].
        /// </summary>
        public static IEnumerable<object[]> IsValidBoardTestData =>
            new List<object[]>
            {
                new object[]
                {
                    "553070000600195000098000060800060003400803001700020006060000280000419005000080079",
                    false,
                    "Board has duplicate in row."
                },
                new object[]
                {
                    "530070000660195000098000060800060003400803001700020006060000280000419005000080079",
                    false,
                    "Board has duplicate in column."
                },
                new object[]
                {
                    "536070000600195000098000060800060003400803001700020006060000280000419005000080079",
                    false,
                    "Board has duplicate in cube."
                },
                new object[]
                {
                    "530070000600195000098000060800060003400803001700020006060000280000419005000080079",
                    true,
                    "A valid board should return true."
                }
            };

        /// <summary>
        /// A single test that runs BoardValidator.IsValid with all the test cases from IsValidBoardTestData.
        /// </summary>
        [Theory]
        [MemberData(nameof(IsValidBoardTestData))]
        public void IsValidBoard_DifferentCases(string input, bool expected, string description)
        {
            Board board = new Board(input);

            bool result = BoardValidator.IsValid(board);

            Assert.Equal(expected, result);
        }
    }
}
