using Xunit;
using System;
using Sudoku.src.Core.SudokuBoard; 
using Sudoku.src.Core.Solver;
using System.Diagnostics;

namespace SudokuTests
{
    public class VariousBoardSizes
    {
        /// <summary>
        /// Tests a 1x1 Sudoku puzzle.
        /// </summary>
        [Fact]
        public void Test1x1Board()
        {
           
            string puzzle = "0";
            Board board = new Board(puzzle);
            SudokuSolver solver = new SudokuSolver(board);

            bool solved = solver.Solve();

            Assert.True(solved, "Expected the solver to fill this single cell Sudoku.");
            Assert.Equal(1, board.cells[0, 0].GetValue());
        }

        /// <summary>
        /// A collection of four 4×4 Sudoku puzzles.
        /// </summary>
        public static IEnumerable<object[]> _4x4TestData => new List<object[]>
        {
            new object[] { "1004030000000002" },
            new object[] { "4003000102000000" },
            new object[] { new string('0', 16) },
            new object[] { "3400100020000000" }
        };

        /// <summary>
        /// Tests each 4×4 puzzle from _4x4TestData, ensuring the solver returns true
        /// and completes in under one second.
        /// </summary>
        [Theory]
        [MemberData(nameof(_4x4TestData))]
        public void Test4x4Board(string puzzle)
        {

            Board board = new Board(puzzle);
            SudokuSolver solver = new SudokuSolver(board);

            Stopwatch stopwatch = Stopwatch.StartNew();

            bool solved = solver.Solve();

            stopwatch.Stop();

            Assert.True(solved, "Expected the solver to solve this 4×4 puzzle.");

            Assert.True(stopwatch.Elapsed < TimeSpan.FromSeconds(1),
                $"Solver took {stopwatch.Elapsed.TotalMilliseconds} ms; expected under 1000 ms.");
        }

        /// <summary>
        /// A collection of four 16×16 puzzles:
        /// </summary>
        public static IEnumerable<object[]> _16x16TestData => new List<object[]>
        {
          
        new object[]
        {
            "00;7030008140@0010:3008=0207004;0<020090?0000008?000:>00;060001000796?000<0:40=0=005<0000036000006@000:0105>00000000;=390000<0@090>0050004@0086:0?4000600782>;0002000800300170?05300?900>00<0100000<>00000030:;00030=000002090<00>0?060@00000000002;000<00?00070"
        },        
        new object[]
        {
            "00020000100000=0000000000000000000=@000<000?00000000000400000000000000000000000900@00000>0?0002000000000000000004;26000=0800000000000000000=000000<0400030000000?90000@006<000050000003009400600000<0000000000000000008000900000000000500000000<0000000000000300"
        },
        new object[]
        {
            "00<04000000000000000?0010070000060000500000000=0000=300000000050000070000000000006>0000000000=0000000?02000000005010>006000000000000000400?000000050:0000=0040000000000000000000000000000;00002=00000>400000000@000000?0000000080080000000;000000009000000000002"
        },
        new object[]
        {
            new string('0', 256)
        }
        };

        /// <summary>
        /// A single test that attempts to solve each puzzle from _16x16TestData
        /// in under one second, asserting that the solver returns true.
        /// </summary>
        [Theory]
        [MemberData(nameof(_16x16TestData))]
        public void Test16x16Board(string puzzle)
        {
            Board board = new Board(puzzle);
            SudokuSolver solver = new SudokuSolver(board);

            Stopwatch stopwatch = Stopwatch.StartNew();

            bool solved = solver.Solve();

            stopwatch.Stop();

            Assert.True(solved, "Expected the solver to solve this 16×16 puzzle.");

            Assert.True(stopwatch.Elapsed < TimeSpan.FromSeconds(1),
                $"Solver took {stopwatch.Elapsed.TotalMilliseconds} ms; expected under 1000 ms.");
        }

        /// <summary>
        /// A collection of three 25×25 Sudoku puzzles.
        /// </summary>
        public static IEnumerable<object[]> _25x25TestData => new List<object[]>
        {
            new object[] { "0E003000000000F000<0000000=00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000600000000000000009000000400000000000000000000000000000000000000000000000000000500000000000000000000000700000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000B000000000000000000000000000000000000000000000000000000000000000000C00000000000000000000000000000000000000000000A000" },

            new object[] { "0000C000F000000000000000000000000000000000000000000000030000000000000000000000000E00000000000000000000H000000000000450000000000000000000000000000000000000F0G000000000000;00:00000000000000000000000000A0000;000000900000000000000000000000000000000000G0000000000?0000000000000000207000000000000G0000000000000000000000?000000000000000000000000000000000000000000000@000000000000030000000000000000000000000000000000000000000000000000@000000000000000000000000>0000000000000000000000000000000<G000000000000000000000000000000000000600000?000000000000000000000000000000000000000000000000000000000009001>00000000000000000000000000000000H" },

            new object[] { new string('0', 625) }
        };

        /// <summary>
        /// Tests each puzzle from _25x25TestData,
        /// expecting the solver to return true.
        /// </summary>
        [Theory]
        [MemberData(nameof(_25x25TestData))]
        public void Test25x25Board(string puzzle)
        {
            Board board = new Board(puzzle);
            SudokuSolver solver = new SudokuSolver(board);

            bool solved = solver.Solve();

            Assert.True(solved, "Expected the solver to solve this 25×25 Sudoku puzzle.");
        }

    }

}

