namespace Lab
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Area
    {
        public int Size { get; set; }

        public int Row { get; set; }

        public int Col { get; set; }
    }

    public class ConnectedAreaInMatrix
    {
        public void Start()
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());
            var matrix = this.FillMatrix(rows, cols);
            var visited = new bool[rows, cols];
            var areas = new List<Area>();

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (matrix[r, c] == '*')
                    {
                        continue;
                    }

                    if (visited[r, c])
                    {
                        continue;
                    }

                    var areaSize = this.GetAreaSize(matrix, r, c, visited);
                    var area = new Area { Row = r, Col = c, Size = areaSize };
                    areas.Add(area);
                }
            }

            this.Print(areas);
        }

        private void Print(List<Area> areas)
        {
            areas = areas.OrderByDescending(x => x.Size)
                .ThenBy(x => x.Row)
                .ThenBy(x => x.Col)
                .ToList();

            Console.WriteLine($"Total areas found: {areas.Count}");
            for (int i = 0; i < areas.Count; i++)
            {
                Console.WriteLine($"Area #{i + 1} at ({areas[i].Row}, {areas[i].Col}), size: {areas[i].Size}");
            }
        }

        private int GetAreaSize(char[,] matrix, int row, int col, bool[,] visited)
        {
            if (this.IsOutside(matrix, row, col))
            {
                return 0;
            }

            if (visited[row, col])
            {
                return 0;
            }

            if (matrix[row, col] == '*')
            {
                return 0;
            }

            visited[row, col] = true;

            return 1 + this.GetAreaSize(matrix, row - 1, col, visited) +
                   this.GetAreaSize(matrix, row + 1, col, visited) +
                   this.GetAreaSize(matrix, row, col - 1, visited) +
                   this.GetAreaSize(matrix, row, col + 1, visited);
        }

        private bool IsOutside(char[,] matrix, int row, int col)
        {
            return row < 0 ||
                col < 0 ||
                row >= matrix.GetLength(0) ||
                col >= matrix.GetLength(1);
        }

        private char[,] FillMatrix(int rows, int cols)
        {
            var matrix = new char[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                var input = Console.ReadLine();

                for (int c = 0; c < cols; c++)
                {
                    matrix[r, c] = input[c];
                }
            }

            return matrix;
        }
    }
}
