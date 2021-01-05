namespace Lab
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MoveDownRightSum
    {
        public void Find()
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());
            var numbers = new int[rows, cols];
            var sums = new int[rows, cols];
            this.ReadMatrix(numbers);

            sums[0, 0] = numbers[0, 0];

            for (int r = 1; r < rows; r++)
            {
                sums[r, 0] = sums[r - 1, 0] + numbers[r, 0];
            }

            for (int c = 1; c < cols; c++)
            {
                sums[0, c] = sums[0, c - 1] + numbers[0, c];
            }

            for (int r = 1; r < rows; r++)
            {
                for (int c = 1; c < cols; c++)
                {
                    var result = Math.Max(sums[r - 1, c], sums[r, c - 1]) + numbers[r, c];

                    sums[r, c] = result;
                }
            }

            var paths = new List<string>();
            var currentRow = rows - 1;
            var currentCol = cols - 1;

            paths.Add($"[{currentRow}, {currentCol}]");

            while (currentRow != 0 || currentCol != 0)
            {
                var top = -1;
                if (currentRow - 1 >= 0)
                {
                    top = sums[currentRow - 1, currentCol];
                }

                var left = -1;
                if (currentCol - 1 >= 0)
                {
                    left = sums[currentRow, currentCol - 1];
                }

                if (top > left)
                {
                    paths.Add($"[{currentRow - 1}, {currentCol}]");
                    currentRow -= 1;
                }
                else
                {
                    paths.Add($"[{currentRow}, {currentCol - 1}]");
                    currentCol -= 1;
                }
            }

            paths.Reverse();
            Console.WriteLine(string.Join(' ', paths));
        }

        private void ReadMatrix(int[,] matrix)
        {
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                var input = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    matrix[r, c] = input[c];
                }
            }
        }
    }
}
