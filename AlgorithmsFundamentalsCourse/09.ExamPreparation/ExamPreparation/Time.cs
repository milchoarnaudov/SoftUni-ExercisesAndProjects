namespace ExamPreparation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Time
    {
        public void Find()
        {
            var firstString = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var secondString = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var table = new int[firstString.Length + 1, secondString.Length + 1];

            for (int r = 1; r < table.GetLength(0); r++)
            {
                for (int c = 1; c < table.GetLength(1); c++)
                {
                    if (firstString[r - 1] == secondString[c - 1])
                    {
                        table[r, c] = table[r - 1, c - 1] + 1;
                    }
                    else
                    {
                        table[r, c] = Math.Max(table[r - 1, c], table[r, c - 1]);
                    }
                }
            }

            var row = firstString.Length;
            var col = secondString.Length;
            var lcs = new Stack<int>();

            while (row > 0 && col > 0)
            {
                if (firstString[row - 1] == secondString[col - 1])
                {
                    lcs.Push(firstString[row - 1]);
                    row -= 1;
                    col -= 1;
                }
                else if (table[row, col - 1] >= table[row - 1, col])
                {
                    col -= 1;
                }
                else
                {
                    row -= 1;
                }
            }
            Console.WriteLine(string.Join(" ", lcs));
            Console.WriteLine(table[table.GetLength(0) - 1, table.GetLength(1) - 1]);
        }
    }
}
