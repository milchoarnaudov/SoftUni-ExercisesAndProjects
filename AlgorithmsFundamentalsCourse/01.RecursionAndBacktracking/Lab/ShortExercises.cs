namespace Lab
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ShortExercises
    {
        // Recursive Array Sum
        public void RecursiveArraysSumStart()
        {
            var input = Console.ReadLine()
                .Split(" ")
                .Select(int.Parse)
                .ToArray();

            var result = this.RecursiveArraysSum(input, 0);

            Console.WriteLine(result);
        }

        private int RecursiveArraysSum(int[] arr, int index)
        {
            if (index == arr.Length)
            {
                return 0;
            }

            return arr[index] + this.RecursiveArraysSum(arr, index + 1);
        }

        // Recursive Drawing
        public void RecursiveDrawingStart()
        {
            var input = int.Parse(Console.ReadLine());

            this.RecursiveDrawing(input);
        }

        private void RecursiveDrawing(int n)
        {
            if (n == 0)
            {
                return;
            }

            Console.WriteLine(new string('*', n));

            this.RecursiveDrawing(n - 1);

            Console.WriteLine(new string('#', n));
        }

        // Recursive Factorial
        public void RecursiveFactorialStart()
        {
            var input = int.Parse(Console.ReadLine());

            var result = this.RecursiveFactorial(input);

            Console.WriteLine(result);
        }

        private long RecursiveFactorial(int n)
        {
            if (n == 0)
            {
                return 1;
            }

            return n * RecursiveFactorial(n - 1);
        }

        // Recursive Fibonacci
        public void CalculateFibonacci()
        {
            var input = int.Parse(Console.ReadLine());

            var result = this.IterativeFibonnaci(input);

            Console.WriteLine(result);
        }

        private int RecursiveFibonacci(int n)
        {
            if (n <= 2)
            {
                return 1;
            }

            return RecursiveFibonacci(n - 1) + RecursiveFibonacci(n - 2);
        }

        private long IterativeFibonnaci(int n)
        {
            long a = 0;
            long b = 1;

            for (int i = 0; i < n; i++)
            {
                long temp = a;
                a = b;
                b = temp + a;
            }

            return a;
        }

        // Generating 0/1 Vectors
        public void Generate01VectorsStart()
        {
            int input = int.Parse(Console.ReadLine());

            Generate01Vectors(0, new int[input]);
        }

        public void Generate01Vectors(int index, int[] arr)
        {
            if (index == arr.Length)
            {
                Console.WriteLine(string.Join(" ", arr));
            }
            else
            {
                for (int i = 0; i <= 1; i++)
                {
                    arr[index] = i;
                    this.Generate01Vectors(index + 1, arr);
                }
            }
        }

        // Find All Paths in a Labyrinth
        public void FindAllPathsInLabyrinthStart()
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());
            var labyrinth = new char[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                var line = Console.ReadLine();

                for (int y = 0; y < line.Length; y++)
                {
                    labyrinth[i, y] = line[y];
                }
            }

            var directions = new List<char>();

            this.FindAllPaths(labyrinth, 0, 0, directions, ' ');
        }

        private void FindAllPaths(char[,] labyrinth, int row, int col, List<char> directions, char direction)
        {
            if (!this.IsInBounds(labyrinth, row, col))
            {
                return;
            }

            directions.Add(direction);

            if (labyrinth[row, col] == 'e')
            {
                var result = string.Join("", directions).Trim();
                Console.WriteLine(result);
                directions.RemoveAt(directions.Count - 1);
                return;
            }

            labyrinth[row, col] = 'v';

            this.FindAllPaths(labyrinth, row, col + 1, directions, 'R');
            this.FindAllPaths(labyrinth, row - 1, col, directions, 'U');
            this.FindAllPaths(labyrinth, row + 1, col, directions, 'D');
            this.FindAllPaths(labyrinth, row, col - 1, directions, 'L');

            directions.RemoveAt(directions.Count - 1);
            labyrinth[row, col] = '-';
        }

        private bool IsInBounds(char[,] labyrinth, int row, int col)
        {
            return !(this.IsOutside(labyrinth, row, col) || this.IsWall(labyrinth, row, col) || this.IsVisited(labyrinth, row, col));
        }

        private bool IsVisited(char[,] labyrinth, int row, int col)
        {
            return labyrinth[row, col] == 'v';
        }

        private bool IsWall(char[,] labyrinth, int row, int col)
        {
            return labyrinth[row, col] == '*';
        }

        private bool IsOutside(char[,] labyrinth, int row, int col)
        {
            return row < 0 ||
                            row >= labyrinth.GetLength(0) ||
                            col < 0 ||
                            col >= labyrinth.GetLength(1);
        }
    }
}
