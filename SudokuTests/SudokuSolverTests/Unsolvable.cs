using Xunit;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using Sudoku.src.Core.SudokuBoard; 
using Sudoku.src.Core.Solver;
using Sudoku.src.Validation;


namespace SudokuTests
{
    public class UnsolvableSudokuTests
    {
        /// <summary>
        /// A collection of unsolvable 9×9 Sudoku puzzles.
        /// </summary>
        public static IEnumerable<object[]> Unsolvable9x9TestData =>
            new List<object[]>
            {
                new object[]
                {
                    "900100004014030800003000900007080018000300000000000300021000070009040500500016003"
                },
                new object[]
                {
                    "100006080000700000090050000000560030300000000000003801500001060000020400802005010"
                },
                new object[]
                {
                    "009028700806004005003000004600000000020713450000000002300000500900400807001250300"
                },
                new object[]
                {
                    "023000009400000100090030040200910004000007800900040002300090001060000000000500000"
                },
                new object[]
                {
                    "900100004014030800003000090000708001800003000000000030021000070009040500500016003"
                }
            };

        /// <summary>
        /// Tests each puzzle from Unsolvable9x9TestData, expecting solver to return false.
        /// Also checks the solver completes in under one second.
        /// </summary>
        /// <param name="puzzle">An Sudoku puzzle string.</param>
        [Theory]
        [MemberData(nameof(Unsolvable9x9TestData))]
        public void TestUnsolvableSudoku9x9_ReturnsFalse(string puzzle)
        {
            Board board = new Board(puzzle);
            SudokuSolver solver = new SudokuSolver(board);

            Stopwatch stopwatch = Stopwatch.StartNew();

            bool solved = solver.Solve();

            stopwatch.Stop();

            Assert.False(solved, "Expected the solver to fail on an unsolvable puzzle.");

            Assert.True(stopwatch.Elapsed < TimeSpan.FromSeconds(1),
                $"Solver took {stopwatch.Elapsed.TotalMilliseconds} ms; expected under 1000 ms.");
        }
    }
}
