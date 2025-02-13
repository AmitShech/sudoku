using Sudoku;
using System;

public static class BoardValidator
{
	public static void IsBoardValid(Board board)
	{
        DuplicatesInBoard(board);

    }

    public static void DuplicatesInBoard(Board board)
    {
        int size = board.size;
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

    private static bool HasDuplicates(CellGroup group)
    {
        HashSet<int> seen = new HashSet<int>();

        foreach (var cell in group.GetEmptyCells())
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
