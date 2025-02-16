namespace Sudoku.src.Core.SudokuBoard
{
    /// <summary>
    /// class that represents the sudoku board object.
    /// </summary>
    public class Board
    {
        public Cell[,] cells { get; private set; }
        public CellGroup[] rows { get; private set; }
        public CellGroup[] cols { get; private set; }
        public CellGroup[] cubes { get; private set; }
        public int size { get; private set; }
        public int cubeSize { get; private set; }

        /// <summary>
        /// Expects an input string where each digit represents a cell (0 for empty).
        /// </summary>
        public Board(string input)
        {
            size = (int)Math.Sqrt(input.Length);
            cubeSize = (int)Math.Sqrt(size);
            cells = new Cell[size, size];
            rows = new CellGroup[size];
            cols = new CellGroup[size];
            cubes = new CellGroup[size];
            SetCellGroups();
            SetCells(input);

            CalculateAllOptions();
        }

        private void SetCells(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                int row = i / size;
                int col = i % size;
                int value = input[i] - '0';

                cells[row, col] = new Cell(row, col);
                if (value != 0)
                    cells[row, col].SetValue(value);

                int cubeIndex = GetCubeIndex(row, col);
                rows[row].AddCell(cells[row, col]);
                cols[col].AddCell(cells[row, col]);
                cubes[cubeIndex].AddCell(cells[row, col]);
            }
        }

        private void SetCellGroups()
        {
            for (int i = 0; i < size; i++)
            {
                rows[i] = new CellGroup(size);
                cols[i] = new CellGroup(size);
                cubes[i] = new CellGroup(size);
            }
        }

        public int GetCubeIndex(int row, int col)
        {
            return row / cubeSize * cubeSize + col / cubeSize;
        }

        /// <summary>
        /// For every empty cell, calculates its available options based on the groups.
        /// </summary>
        public void CalculateAllOptions()
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (!cells[row, col].IsEmpty())
                        continue;

                    int rowMask = rows[row].GetMask();
                    int colMask = cols[col].GetMask();
                    int cubeMask = cubes[GetCubeIndex(row, col)].GetMask();
                    int intersection = rowMask & colMask & cubeMask;

                    cells[row, col].SetOptions(intersection);
                }
            }
        }

        /// <summary>
        /// Sets a value in the board and updates the groups.
        /// </summary>
        public void SetValue(int row, int col, int value)
        {
            cells[row, col].SetValue(value);

            rows[row].RemoveOption(value);
            cols[col].RemoveOption(value);
            cubes[GetCubeIndex(row, col)].RemoveOption(value);

            rows[row].RemoveOptionFromCells(value);
            cols[col].RemoveOptionFromCells(value);
            cubes[GetCubeIndex(row, col)].RemoveOptionFromCells(value);
        }

        /// <summary>
        /// Clears a cell’s value and then updates the related groups.
        /// </summary>
        public void ClearValue(int row, int col)
        {
            cells[row, col].ClearValue();

            rows[row].UpdateMask();
            cols[col].UpdateMask();
            cubes[GetCubeIndex(row, col)].UpdateMask();
        }

        public bool IsValid()
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (!cells[row, col].IsEmpty())
                        continue;

                    if (cells[row, col].possibleOptionsMask == 0)
                        return false;
                }
            }
            return true;
        }
    }
}