using System;
using System.Collections.Generic;
using System.Numerics;

namespace Sudoku
{
    public class Cell
    {
        public int Value { get; private set; }
        public int PossibleOptionsMask { get; private set; }
        public int Row { get; }
        public int Column { get; }

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
            Value = 0;
            PossibleOptionsMask = 0;
        }

        public bool IsEmpty() => Value == 0;

        public void SetValue(int value)
        {
            Value = value;
            PossibleOptionsMask = 0;
        }

        public void ClearValue()
        {
            Value = 0;
            PossibleOptionsMask = 0;
        }

        public bool HasPossibleOption(int option) =>
            (PossibleOptionsMask & (1 << (option - 1))) != 0;

        public void AddPossibleOption(int option)
        {
            PossibleOptionsMask |= (1 << (option - 1));
        }

        public void RemovePossibleOption(int option)
        {
            PossibleOptionsMask &= ~(1 << (option - 1));
        }

        public int GetPossibleOptionsCount()
        {
            return BitOperations.PopCount((uint)PossibleOptionsMask);
        }

        public int GetFirstPossibleOption()
        {
            return BitOperations.TrailingZeroCount((uint)PossibleOptionsMask) + 1;
        }
    }
}
