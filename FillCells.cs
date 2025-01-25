namespace Sudoku
{
    internal static class FillCells
    {
        public static void CalculateAllOptions(Board board)
        {
            for (int row = 0; row < board.Size; row++)
            {
                for (int col = 0; col < board.Size; col++)
                {
                    Cell cell = board.Cells[row, col];

                    if (!cell.IsEmpty()) continue;

                    var rowSeen = board.GetRow(row);
                    var columnSeen = board.GetColumn(col);
                    var cubeSeen = board.GetCube(row, col);

                    var possibleOptions = new HashSet<int>(Enumerable.Range(1, board.Size));
                    possibleOptions.ExceptWith(rowSeen);
                    possibleOptions.ExceptWith(columnSeen);
                    possibleOptions.ExceptWith(cubeSeen);

                    cell.PossibleOptions = possibleOptions;
                }
            }
        }

        public static void UpdateAffectedCells(Board board, Cell cell)
        {
            var cellsToUpdate = new Queue<Cell>();
            var processedCells = new HashSet<Cell>();
            cellsToUpdate.Enqueue(cell);

            while (cellsToUpdate.Count > 0)
            {
                var currentCell = cellsToUpdate.Dequeue();
                if (processedCells.Contains(currentCell)) continue;
                processedCells.Add(currentCell);

                UpdateRow(board, currentCell, cellsToUpdate);
                UpdateColumn(board, currentCell, cellsToUpdate);
                UpdateCube(board, currentCell, cellsToUpdate);
            }
        }

        private static void UpdateRow(Board board, Cell cell, Queue<Cell> cellsToUpdate)
        {
            for (int col = 0; col < board.Size; col++)
            {
                Cell rowCell = board.Cells[cell.Row, col];
                if (rowCell != cell && rowCell.RemoveOption(cell.Value))
                {
                    cellsToUpdate.Enqueue(rowCell);
                }
            }
        }
        private static void UpdateColumn(Board board, Cell cell, Queue<Cell> cellsToUpdate)
        {
            for (int row = 0; row < board.Size; row++)
            {
                Cell columnCell = board.Cells[row, cell.Column];
                if (columnCell != cell && columnCell.RemoveOption(cell.Value))
                {
                    cellsToUpdate.Enqueue(columnCell);
                }
            }
        }

        private static void UpdateCube(Board board, Cell cell, Queue<Cell> cellsToUpdate)
        {
            int startRow = (cell.Row / board.CubeSize) * board.CubeSize;
            int startCol = (cell.Column / board.CubeSize) * board.CubeSize;

            for (int r = 0; r < board.CubeSize; r++)
            {
                for (int c = 0; c < board.CubeSize; c++)
                {
                    Cell cubeCell = board.Cells[startRow + r, startCol + c];
                    if (cubeCell != cell && cubeCell.RemoveOption(cell.Value))
                    {
                        cellsToUpdate.Enqueue(cubeCell);
                    }
                }
            }
        }
    }
}
