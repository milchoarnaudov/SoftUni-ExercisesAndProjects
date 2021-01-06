namespace Exercises
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Node
    {
        public Node(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        public int Row { get; set; }

        public int Col { get; set; }
    }

    public class AreasInMatrix
    {
        private char[,] matrix;
        private bool[,] visited;

        public void Find()
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());

            this.matrix = this.ReadMatrix(rows, cols);
            this.visited = new bool[rows, cols];

            var areas = new SortedDictionary<char, int>();
            var sum = 0;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (visited[r, c])
                    {
                        continue;
                    }

                    this.DFS(r, c);
                    sum += 1;
                    var key = matrix[r, c];
                    if (!areas.ContainsKey(key))
                    {
                        areas.Add(key, 1);
                    }
                    else
                    {
                        areas[key] += 1;
                    }
                }
            }

            Console.WriteLine($"Areas: {sum}");
            foreach (var area in areas)
            {
                Console.WriteLine($"Letter '{area.Key}' -> {area.Value}");
            }
        }

        private void DFS(int row, int col)
        {
            this.visited[row, col] = true;
            var children = this.GetChildren(row, col);

            foreach (var child in children)
            {
                this.DFS(child.Row, child.Col);
            }
        }

        private List<Node> GetChildren(int row, int col)
        {
            var children = new List<Node>();

            if (this.IsInside(row - 1, col) &&
                this.IsChild(row, col, row - 1, col) &&
                !this.IsVisited(row - 1, col))
            {
                children.Add(new Node(row - 1, col));
            }

            if (this.IsInside(row + 1, col) &&
                this.IsChild(row, col, row + 1, col) &&
                !this.IsVisited(row + 1, col))
            {
                children.Add(new Node(row + 1, col));
            }

            if (this.IsInside(row, col - 1) &&
                this.IsChild(row, col, row, col - 1) &&
                !this.IsVisited(row, col - 1))
            {
                children.Add(new Node(row, col - 1));
            }

            if (this.IsInside(row, col + 1) &&
                this.IsChild(row, col, row, col + 1) &&
                !this.IsVisited(row, col + 1))
            {
                children.Add(new Node(row, col + 1));
            }

            return children;
        }

        private bool IsVisited(int row, int col)
        {
            return visited[row, col];
        }

        private bool IsChild(int parentRow, int parentCol, int childRow, int childCol)
        {
            return matrix[parentRow, parentCol] == matrix[childRow, childCol];
        }

        private bool IsInside(int row, int col)
        {
            return row >= 0 &&
                row < this.matrix.GetLength(0) &&
                col >= 0 &&
                col < this.matrix.GetLength(1);
        }

        private char[,] ReadMatrix(int rows, int cols)
        {
            var result = new char[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                var elements = Console.ReadLine();

                for (int c = 0; c < cols; c++)
                {
                    result[r, c] = elements[c];
                }
            }

            return result;
        }
    }
}
