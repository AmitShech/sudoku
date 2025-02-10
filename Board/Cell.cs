using System.Collections.Generic;

namespace Sudoku
{
    /// <summary>
    /// class that represents a cell in the sudoku board.
    /// </summary>
    public class Cell
    {
        public int row { get; }
        public int col { get; }
        private int value { get; set; }
        public int possibleOptionsMask { get; private set; }

        public Cell(int row, int col)
        {
            this.row = row;
            this.col = col;
            value = 0;
        }

        public void SetValue(int value)
        {
            this.value = value;
            possibleOptionsMask = 0;
        }

        public int GetValue() => value;
        public bool IsEmpty() => value == 0;
        public void ClearValue() => value = 0;
        public void SetOptions(int mask) => possibleOptionsMask = mask;
        public void RemoveOption(int value) => possibleOptionsMask &= ~(1 << (value - 1));
        public void RemoveOptionWithMask(int mask) => possibleOptionsMask &= ~mask;

        /// <summary>
        /// Returns true if the given value is one of the cell’s options.
        /// </summary>
        public bool HasOption(int value) => (possibleOptionsMask & (1 << (value - 1))) != 0;

        /// <summary>
        /// Returns the amount of the cell’s options.
        /// </summary>
        public int GetPossibleOptionsCount()
        {
            int count = 0;
            int mask = possibleOptionsMask;
            while (mask != 0)
            {
                count += mask & 1;
                mask >>= 1;
            }
            return count;
        }

        /// <summary>
        /// Returns the cell’s options.
        /// </summary>
        public int[] GetPossibleOptions()
        {
            var options = new List<int>();
            int mask = possibleOptionsMask;
            int candidate = 1;
            while (mask != 0)
            {
                if ((mask & 1) != 0)
                    options.Add(candidate);
                mask >>= 1;
                candidate++;
            }
            return options.ToArray();
        }
    }
}
