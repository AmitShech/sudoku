using Sudoku;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suduko
{
    internal class SolveBoard
    {
        private Board board;
        public SolveBoard(Board board) { this.board = board; }

        public bool FillSingleOptionCells()
        {
            bool progress = false;

            for (int row = 0; row < board.Size; row++)
            {
                for (int col = 0; col < board.Size; col++)
                {
                    var cell = board.Cells[row, col];
                    if (!cell.IsEmpty()) continue;
                    if (cell.PossibleOptions.Count == 1)
                    {
                        cell.SetValue(cell.PossibleOptions.First());
                        FillCells.UpdateAffectedCells(board,cell);
                        progress = true;
                    }
                }
            }

            return progress;
        }

        public Cell MinOptions()
        {
            Cell bestCell = null;
            int minOptions = int.MaxValue;

            for (int row = 0; row < board.Size; row++)
            {
                for (int col = 0; col < board.Size; col++)
                {
                    var cell = board.Cells[row, col];
                    if (!cell.IsEmpty()) continue;
                    if (cell.PossibleOptions.Count < minOptions)
                    {
                        minOptions = cell.PossibleOptions.Count;
                        bestCell = cell;
                    }
                }
            }
            return bestCell;
        }

        private int FindConstrainingImpact(Cell cell, int value)
        {
            int impact = 0;

            for (int col = 0; col < board.Size; col++)
            {
                if (board.Cells[cell.Row, col].IsEmpty() && board.Cells[cell.Row, col].PossibleOptions.Contains(value))
                {
                    impact++;
                }
            }

            for (int row = 0; row < board.Size; row++)
            {
                if (board.Cells[row, cell.Column].IsEmpty() && board.Cells[row, cell.Column].PossibleOptions.Contains(value))
                {
                    impact++;
                }
            }

            int startRow = (cell.Row / board.CubeSize) * board.CubeSize;
            int startCol = (cell.Column / board.CubeSize) * board.CubeSize;

            for (int r = 0; r < board.CubeSize; r++)
            {
                for (int c = 0; c < board.CubeSize; c++)
                {
                    var cubeCell = board.Cells[startRow + r, startCol + c];
                    if (cubeCell.IsEmpty() && cubeCell.PossibleOptions.Contains(value))
                    {
                        impact++;
                    }
                }
            }

            return impact;
        }

        public bool Backtracking(){
            Cell cell = MinOptions();

            if (cell == null)
            {
                return true;
            }

            foreach (var option in cell.PossibleOptions.OrderByDescending(val => FindConstrainingImpact(cell, val)))
            {
                cell.SetValue(option);
                FillCells.UpdateAffectedCells(board, cell);


                if (Backtracking())
                {
                    return true; 
                }

                    
                cell.ClearValue();
                FillCells.UpdateAffectedCells(board, cell);
            }

            return false; 
        }


    }
}
