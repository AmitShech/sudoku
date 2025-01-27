using Suduko;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Sudoku
{
    public class Board
    {
        public int Size { get; }
        public int CubeSize { get; }
        public Cell[,] Cells { get; }
        private readonly BoardConstraints Constraints;

        public Board(string input)
        {
            Size = (int)Math.Sqrt(input.Length);
            CubeSize = (int)Math.Sqrt(Size);
            Cells = new Cell[Size, Size];
            Constraints = new BoardConstraints(Size);

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    int value = input[row * Size + col] - '0';
                    Cells[row, col] = new Cell(row, col);

                    if (value != 0)
                    {
                        SetCellValue(row, col, value);
                    }
                }
            }
        }

        public void SetCellValue(int row, int col, int value)
        {
            Cells[row, col].SetValue(value);
            Constraints.SetValue(row, col, value);
        }

        public bool CanPlaceValue(int row, int col, int value)
        {
            return Constraints.CanPlaceValue(row, col, value);
        }

        public void ClearCellValue(int row, int col)
        {
            int value = Cells[row, col].Value;
            if (value == 0) return;

            Cells[row, col].ClearValue();
            Constraints.ClearValue(row, col, value);
        }

        public void CalculateAllOptions()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if (!Cells[row, col].IsEmpty()) continue;

                    for (int value = 1; value <= Size; value++)
                    {
                        if (CanPlaceValue(row, col, value))
                        {
                            Cells[row, col].AddPossibleOption(value);
                        }
                    }
                }
            }
        }

    public void PrintString()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    Console.Write(Cells[row, col].Value+'0');
                }
            }
        }

        public void PrintGraphic()
        {
            for (int row = 0; row < Size; row++)
            {
                if (row % CubeSize == 0 && row != 0)
                {
                    Console.WriteLine(new string('-', Size * 2 + CubeSize - 1));
                }

                for (int col = 0; col < Size; col++)
                {
                    if (col % CubeSize == 0 && col != 0)
                    {
                        Console.Write("| ");
                    }

                    if (Cells[row, col].Value == 0)
                    {
                        Console.Write("_ ");
                    }
                    else
                    {
                        Console.Write(Cells[row, col].Value + '0');
                    }
                }

                Console.WriteLine(); 
            }
        }

    }
}
