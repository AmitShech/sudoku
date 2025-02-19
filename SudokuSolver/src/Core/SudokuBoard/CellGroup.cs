namespace Sudoku.src.Core.SudokuBoard
{
    /// <summary>
    /// class that represents the rows,columns and cubes of the sudoku board.
    /// </summary>
    public class CellGroup
    {
        private List<Cell> cells { get; }
        private int size { get; }
        private int possibleOptionsMask { get; set; }

        public CellGroup(int size)
        {
            this.size = size;
            cells = new List<Cell>();
            possibleOptionsMask = (1 << this.size) - 1;
        }

        public List<Cell> GetCells() { return cells; }
        public int GetMask() => possibleOptionsMask;
        public void SetMask(int mask) => possibleOptionsMask = mask;

        /// <summary>
        /// Add the cell to the board group and update the group mask.
        /// </summary>
        public void AddCell(Cell cell)
        {
            if (!cells.Contains(cell))
            {
                cells.Add(cell);
            }
            if (!cell.IsEmpty())
            {
                RemoveOption(cell.GetValue());
            }
        }

        /// <summary>
        /// Return the empty cells in the group.
        /// </summary>
        public List<Cell> GetEmptyCells()
        {
            return cells.FindAll(cell => cell.IsEmpty());
        }

        /// <summary>
        /// Recalculates the group mask based on the filled cells.
        /// </summary>
        public void UpdateMask()
        {
            possibleOptionsMask = (1 << size) - 1;  // reset
            foreach (var cell in cells)
            {
                if (!cell.IsEmpty())
                    possibleOptionsMask &= ~(1 << cell.GetValue() - 1);
            }
        }

        /// <summary>
        /// delete the option from all the group's cells.
        /// </summary>
        public void RemoveOptionFromCells(int value)
        {
            foreach (var cell in cells)
            {
                if (cell.IsEmpty())
                    cell.RemoveOption(value);
            }
        }

        /// <summary>
        /// delete the option from all the group.
        /// </summary>
        public void RemoveOption(int value)
        {
            possibleOptionsMask &= ~(1 << value - 1);
        }
    }
}