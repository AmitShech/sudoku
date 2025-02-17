using Sudoku.src.Core.SudokuBoard;
using Sudoku.src.Exceptions;

/// <summary>
/// Provides methods to validate a Sudoku board by checking for duplicate values
/// in rows, columns, and cubes.
/// </summary>
public static class BoardValidator
{
   
    /// <summary>
    /// Checks if the board is valid by verifying that no duplicates exist in any row, column, or cube.
    /// If any duplicate is found, an error message is printed and the method returns false.
    /// </summary>
    /// <param name="board">The Sudoku board to validate.</param>
    /// <returns>True if the board is valid; otherwise, false.</returns>
    public static bool IsValid(Board board)
    {
        try { DuplicatesInBoard(board); }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            return false;
        }
        return true;
    }

    /// <summary>
    /// Checks if the board is solvable by checking that all cells has options.
    /// If cell with no options is found, the method returns false.
    /// </summary>
    /// <param name="board">The Sudoku board to validate.</param>
    /// <returns>True if the board is solvable; otherwise, false.</returns>
    private static bool IsSolvable(Board board)
    {
        for (int row = 0; row < board.size; row++)
        {
            for (int col = 0; col < board.size; col++)
            {
                if (!board.cells[row, col].IsEmpty())
                    continue;

                if (board.cells[row, col].possibleOptionsMask == 0)
                    return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Checks each row, column, and cube of the board for duplicate numbers.
    /// Throws an InvalidBoardException if any duplicate is detected.
    /// </summary>
    /// <param name="board">The Sudoku board to check for duplicates.</param>
    private static void DuplicatesInBoard(Board board)
    {
        int i = 0;
        foreach (var row in board.rows)
        {
            i++;
            if (HasDuplicates(row))
                throw new InvalidBoardException($"The board is invalid, duplicate number detected in row {i}.");
        }

        i = 0;
        foreach (var col in board.cols)
        {
            i++;
            if (HasDuplicates(col))
                throw new InvalidBoardException($"The board is invalid, duplicate number detected in column {i}.");
        }

        i = 0;
        foreach (var cube in board.cubes)
        {
            i++;
            if (HasDuplicates(cube))
                throw new InvalidBoardException($"The board is invalid, duplicate number detected in cube {i}.");
        }
    }

    /// <summary>
    /// Determines whether a given cell group (row, column, or cube) contains duplicate numbers.
    /// Only non-zero values are considered.
    /// </summary>
    /// <param name="group">The cell group to check.</param>
    /// <returns>True if duplicates are found; otherwise, false.</returns>
    private static bool HasDuplicates(CellGroup group)
    {
        HashSet<int> seen = new HashSet<int>();

        foreach (Cell cell in group.GetCells())
        {
            int value = cell.GetValue();
            if (value != 0)
            {
                if (!seen.Add(value))
                    return true;
            }
        }
        return false;
    }
}
