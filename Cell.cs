using System;
using System.Collections.Generic;

namespace Sudoku
{
    public class Cell
    {
        public int Value { get; set; }
        public HashSet<int> PossibleOptions { get; set; } 
        public int Row { get; } 
        public int Column { get; } 

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
            Value = 0; 
            PossibleOptions = new HashSet<int>();   
        }

        public bool IsEmpty()
        {
            return Value == 0;
        }

        public void SetValue(int value)
        {
            Value = value;
            PossibleOptions.Clear();
        }

        public void ClearValue()
        {
            Value = 0;
        }

        public bool IsValidPlacement(int value)
        {
            return PossibleOptions.Contains(value) || value == 0;
        }

        public bool RemoveOption(int value)
        {
            if (!PossibleOptions.Contains(value)) 
            {
                return false;
            }

            PossibleOptions.Remove(value);

            if (PossibleOptions.Count == 0)
            {
                throw new InvalidOperationException($"Cell at ({Row}, {Column}) has no valid options remaining.");
            }

            if (PossibleOptions.Count == 1)
            {
                SetValue(PossibleOptions.First());
                return true;
            }

            return false;
        }

    }
}
