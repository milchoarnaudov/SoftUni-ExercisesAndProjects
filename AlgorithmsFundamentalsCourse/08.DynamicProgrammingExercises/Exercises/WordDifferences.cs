namespace Exercises
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class WordDifferences
    {
        public void Find()
        {
            var firstString = Console.ReadLine();
            var secondString = Console.ReadLine();

            var table = new int[firstString.Length + 1, secondString.Length + 1];

            for (int r = 1; r < table.GetLength(0); r++)
            {
                table[r, 0] = r;
            }

            for (int c = 1; c < table.GetLength(1); c++)
            {
                table[0, c] = c;
            }

            for (int r = 1; r < table.GetLength(0); r++)
            {
                for (int c = 1; c < table.GetLength(1); c++)
                {
                    if (firstString[r - 1] == secondString[c - 1])
                    {
                        table[r, c] = table[r - 1, c - 1];
                    }
                    else
                    {
                        table[r, c] = Math.Min(table[r, c - 1], table[r - 1, c]) + 1; ;
                    }
                }
            }
            Console.WriteLine($"Deletions and Insertions: {table[firstString.Length, secondString.Length]}");
        }
    }
}
