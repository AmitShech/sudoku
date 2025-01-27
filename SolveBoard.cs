using Sudoku;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suduko
{
    public class SolveBoard
    {
        private Board board;

        public SolveBoard(Board board)
        {
            this.board = board;
        }


        public bool Solve()
        {
            return true;
        }

        public bool Backtracking()
        {
            Cell cell = FindCellWithMinOptions();

            if (cell == null)
            {
                return true;
            }

            foreach (int option in GetOrderedPossibleOptions(cell))
            {
                cell.SetValue(option);
                board.SetCellValue(cell.Row, cell.Column, option);

                if (Backtracking())
                {
                    return true;
                }

                board.ClearCellValue(cell.Row, cell.Column);
                cell.ClearValue();
            }

            return false;
        }

        private Cell FindCellWithMinOptions()
        {
            Cell bestCell = null;
            int minOptions = int.MaxValue;

            for (int row = 0; row < board.Size; row++)
            {
                for (int col = 0; col < board.Size; col++)
                {
                    var cell = board.Cells[row, col];
                    if (!cell.IsEmpty()) continue;

                    int optionsCount = cell.GetPossibleOptionsCount();
                    if (optionsCount < minOptions)
                    {
                        minOptions = optionsCount;
                        bestCell = cell;
                    }
                }
            }
            return bestCell;
        }

        private IEnumerable<int> GetOrderedPossibleOptions(Cell cell)
        {
            return Enumerable.Range(1, board.Size)
                .Where(cell.HasPossibleOption)
                .OrderByDescending(option => CalculateConstrainingImpact(cell, option));
        }

        private int CalculateConstrainingImpact(Cell cell, int value)
        {
            int impact = 0;

            for (int i = 0; i < board.Size; i++)
            {
                if (board.Cells[cell.Row, i].IsEmpty() && board.CanPlaceValue(cell.Row, i, value))
                    impact++;

                if (board.Cells[i, cell.Column].IsEmpty() && board.CanPlaceValue(i, cell.Column, value))
                    impact++;
            }

            return impact;
        }
    }
}
