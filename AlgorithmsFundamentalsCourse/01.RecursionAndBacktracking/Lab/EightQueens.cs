using System;
using System.Collections.Generic;

namespace Lab
{
    public class EightQueens
    {
        private HashSet<int> threatenedRows;
        private HashSet<int> threatenedCols;
        private HashSet<int> threatenedLeftDiagonal;
        private HashSet<int> threatenedRightDiagonal;

        public EightQueens()
        {
            this.threatenedRows = new HashSet<int>();
            this.threatenedCols = new HashSet<int>();
            this.threatenedLeftDiagonal = new HashSet<int>();
            this.threatenedRightDiagonal = new HashSet<int>();
        }

        public void EightQueensStart()
        {
            var board = new bool[8, 8];

            PlaceQueen(board, 0);
        }

        private void PlaceQueen(bool[,] board, int row)
        {
            if (row == board.GetLength(0))
            {
                PrintBoard(board);
            }

            for (int col = 0; col < board.GetLength(1); col++)
            {
                if (IsFree(row, col))
                {
                    board[row, col] = true;

                    this.threatenedRows.Add(row);
                    this.threatenedCols.Add(col);
                    this.threatenedLeftDiagonal.Add(row - col);
                    this.threatenedRightDiagonal.Add(row + col);

                    PlaceQueen(board, row + 1);

                    board[row, col] = false;

                    this.threatenedRows.Remove(row);
                    this.threatenedCols.Remove(col);
                    this.threatenedLeftDiagonal.Remove(row - col);
                    this.threatenedRightDiagonal.Remove(row + col);
                }
            }
        }

        private bool IsFree(int row, int col)
        {
            return !(this.threatenedRows.Contains(row) ||
                this.threatenedCols.Contains(col) ||
                this.threatenedLeftDiagonal.Contains(row - col) ||
                this.threatenedRightDiagonal.Contains(row + col));
        }

        private void PrintBoard(bool[,] board)
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col])
                    {
                        Console.Write("* ");
                    }
                    else
                    {
                        Console.Write("- ");
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
