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

       
        public Board(string input)
        {
            
            Size = (int)Math.Sqrt(input.Length);
            CubeSize = (int)Math.Sqrt(Size);
            Cells = new Cell[Size, Size];

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    int value = input[row * Size + col] - '0'; 
                    Cells[row, col] = new Cell(row, col);
                    Cells[row, col].SetValue(value);
                }
            }
        }

        public HashSet<int> GetRow(int row)
        {
            HashSet<int> rowSeen = new(Size);
            int value=0;
            for (int c = 0; c < Size; c++)
            {
                value = Cells[row, c].Value;
                if (value == 0) { continue; }
                if (rowSeen.Contains(value))
                    throw new UnvalidBoard($"the number {value} repeats itself in row {row}");
                rowSeen.Add(value);
            }
            return rowSeen;
        }

        public HashSet<int> GetColumn(int col)
        {
            HashSet<int> columnSeen = new(Size);
            for (int row = 0; row < Size; row++)
            {
                int value = Cells[row, col].Value;
                if (value == 0) continue;
                if (columnSeen.Contains(value))
                    throw new UnvalidBoard($"The number {value} repeats itself in column {col}.");
                columnSeen.Add(value);
            }
            return columnSeen;
        }

        public HashSet<int> GetCube(int row, int col)
        {
            HashSet<int> cubeSeen = new(Size);
            int startRow = (row / CubeSize) * CubeSize;
            int startCol = (col / CubeSize) * CubeSize;

            for (int r = 0; r < CubeSize; r++)
            {
                for (int c = 0; c < CubeSize; c++)
                {
                    int value = Cells[startRow + r, startCol + c].Value;
                    if (value == 0) continue;
                    if (cubeSeen.Contains(value))
                        throw new UnvalidBoard($"The number {value} repeats itself in the cube starting at ({startRow}, {startCol}).");
                    cubeSeen.Add(value);
                }
            }
            return cubeSeen;
        }
        

        public void PrintString()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    Console.Write(Cells[row, col].Value + " ");
                }
                Console.WriteLine();
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
                        Console.Write(Cells[row, col].Value + " ");
                    }
                }

                Console.WriteLine(); 
            }
        }

    }
}
