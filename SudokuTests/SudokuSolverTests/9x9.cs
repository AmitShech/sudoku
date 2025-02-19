using Xunit;
using System;
using System.Diagnostics;
using Sudoku.src.Core.SudokuBoard; 
using Sudoku.src.Core.Solver;          
using Sudoku.src.Validation;    

namespace SudokuTests
{
    public class EasySudoku9x9Tests
    {
        /// <summary>
        /// A single test (Theory) that checks three 9x9 Sudoku boards,
        /// ensuring each is solved in under one second.
        /// </summary>
        /// <param name="puzzle">An 81-character string representing the Sudoku board.</param>
        [Theory]
        [InlineData("530070000600195000098000060800060003400803001700020006060000280000419005000080079")]
        [InlineData("003020600900305001001806400008102900700000008006908200002609500800203009005010300")]
        [InlineData("800000070006010053040600000000080400003000700020005038000000800004050061900002000")]
        public void TestBoard9x9(string puzzle)
        { 
            Board board = new Board(puzzle);
            SudokuSolver solver = new SudokuSolver(board);

            Stopwatch stopwatch = Stopwatch.StartNew();

            bool solved = solver.Solve();

            stopwatch.Stop();

            Assert.True(solved, "Expected the solver to solve this Sudoku puzzle.");

            Assert.True(stopwatch.Elapsed < TimeSpan.FromSeconds(1),
                $"Solving took {stopwatch.Elapsed.TotalMilliseconds} ms, expected under 1000 ms.");

        }

        /// <summary>
        /// A collection of "hard" 9x9 Sudoku boards.
        /// </summary>
        public static IEnumerable<object[]> Hard9x9TestData => new List<object[]>
        {
            new object[] { "805000002000901000300000000060700400200050000000000600003800000100009000400000007" },
            new object[] { "000040001030600000800000000100900500000000870000200000700000260500094000000000300" },
            new object[] { "000903100607000800200000000500000400000060020010000000800070000003005000040000009" },
            new object[] { "000020040070006000010500000200000008000300700409000000000600103800090000000000500" },
            new object[] { "704000002000801000300000000050600100200040000000000900003700000800006000900000005" }
        };

        /// <summary>
        /// Tests each "hard" 9x9 puzzle from Hard9x9TestData, checking that
        /// the solver finishes under one second and returns 'true' for "solved".
        /// </summary>
        [Theory]
        [MemberData(nameof(Hard9x9TestData))]
        public void TestHardBoards(string puzzle)
        {

            Board board = new Board(puzzle);
            SudokuSolver solver = new SudokuSolver(board);

            Stopwatch stopwatch = Stopwatch.StartNew();
            bool solved = solver.Solve();
            stopwatch.Stop();

            Assert.True(solved, "Expected the solver to solve this Sudoku puzzle.");
            Assert.True(
                stopwatch.Elapsed < TimeSpan.FromSeconds(1),
                $"Solving took {stopwatch.Elapsed.TotalMilliseconds} ms, expected under 1000 ms."
            );
        }

        /// <summary>
        /// Tests an empty 9x9 Sudoku board and check if it solves under one second.
        /// </summary>
        [Fact]
        public void TestEmptyBoard()
        {
            string puzzle = new string('0', 81); 
            Board board = new Board(puzzle);
            SudokuSolver solver = new SudokuSolver(board);

            Stopwatch stopwatch = Stopwatch.StartNew();
            bool solved = solver.Solve();
            stopwatch.Stop();

            Assert.True(solved, "Expected the solver to solve an empty 9x9 Sudoku puzzle.");
            Assert.True(
                stopwatch.Elapsed < TimeSpan.FromSeconds(1),
                $"Solving took {stopwatch.Elapsed.TotalMilliseconds} ms, expected under 1000 ms."
            );
        }
    }
}
