using Sudoku.src.Core.SudokuBoard;

namespace Sudoku.src.Core.Solver.SudokuStrategies
{
    /// <summary>
    /// A generic implementation of the naked candidates technique.
    /// This rule examines rows, columns, and cubes (groups) and removes candidates when a set of N cells
    /// have exactly N candidate values combined.
    /// </summary>
    public class NakedGeneric : ISudokuRule
    {
        public static int AmountOfIterations = 8;
        /// <summary>
        /// Applies the naked candidates elimination to the entire board.
        /// </summary>
        /// <param name="board">The Sudoku board.</param>
        /// <returns>True if any changes were made to the board; otherwise, false.</returns>
        public bool Apply(Board board)
        {
            bool changed = false;

            if (board.size > 9)
                AmountOfIterations = 2;

            for (int size = 2; size <= Math.Min(board.size, AmountOfIterations); size++)
            {
                foreach (var row in board.rows)
                {
                    changed |= ApplyNakedInGroup(row, size);
                }

                foreach (var col in board.cols)
                {
                    changed |= ApplyNakedInGroup(col, size);
                }

                foreach (var cube in board.cubes)
                {
                    changed |= ApplyNakedInGroup(cube, size);
                }
            }

            return changed;
        }

        /// <summary>
        /// Applies the naked candidate technique to a single cell group.
        /// </summary>
        /// <param name="group">The group (row, column, or cube) to examine.</param>
        /// <param name="size">The candidate set size (e.g., 2 for naked pairs, 3 for triples, etc.).</param>
        /// <returns>True if changes were made; otherwise, false.</returns>
        private bool ApplyNakedInGroup(CellGroup group, int size)
        {
            bool changed = false;
            var emptyCells = group.GetEmptyCells();

            var candidateCells = emptyCells
                                    .Where(c => c.GetPossibleOptionsCount() > 0 && c.GetPossibleOptionsCount() <= size)
                                    .ToList();

            if (candidateCells.Count < size)
                return false;

            foreach (var combination in GetCombinations(candidateCells.Count, size))
            {
                int combinedOptions = 0;
                foreach (int index in combination)
                {
                    combinedOptions |= candidateCells[index].possibleOptionsMask;
                }

                if (CountBits(combinedOptions) == size)
                {
                    var nakedCells = new HashSet<Cell>(combination.Select(i => candidateCells[i]));

                    int remainingOptions = group.GetMask();
                    foreach (var cell in emptyCells)
                    {
                        if (!nakedCells.Contains(cell))
                        {
                            int originalOptions = cell.possibleOptionsMask;
                            cell.RemoveOptionWithMask(combinedOptions);
                            if (cell.possibleOptionsMask != originalOptions)
                            {
                                changed = true;
                            }
                            remainingOptions |= cell.possibleOptionsMask;
                        }
                        else
                        {
                            remainingOptions |= cell.possibleOptionsMask & combinedOptions;
                        }
                    }
                    group.SetMask(remainingOptions);
                }
            }

            return changed;
        }

        /// <summary>
        /// Generates all combinations (as lists of indices) of the given size from a set of total elements.
        /// </summary>
        /// <param name="total">Total number of candidate cells.</param>
        /// <param name="size">Size of the combination.</param>
        /// <returns>An enumerable of combinations, where each combination is a list of indices.</returns>
        private IEnumerable<List<int>> GetCombinations(int total, int size)
        {
            var combinations = new List<List<int>>();

            for (int bitmask = 0; bitmask < 1 << total; bitmask++)
            {
                if (CountBits(bitmask) == size)
                {
                    var combination = new List<int>();
                    for (int i = 0; i < total; i++)
                    {
                        if ((bitmask & 1 << i) != 0)
                        {
                            combination.Add(i);
                        }
                    }
                    combinations.Add(combination);
                }
            }

            return combinations;
        }

        /// <summary>
        /// Counts the number of 1 bits in an integer.
        /// </summary>
        private int CountBits(int n)
        {
            int count = 0;
            while (n != 0)
            {
                count += n & 1;
                n >>= 1;
            }
            return count;
        }
    }
}
