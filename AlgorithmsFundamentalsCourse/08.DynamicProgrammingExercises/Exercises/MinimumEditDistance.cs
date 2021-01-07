namespace Exercises
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class MinimumEditDistance
    {
        public void Find()
        {
            var replaceCost = int.Parse(Console.ReadLine());
            var insertCost = int.Parse(Console.ReadLine());
            var deleteCost = int.Parse(Console.ReadLine());
            var firstString = Console.ReadLine();
            var secondString = Console.ReadLine();

            var table = new int[firstString.Length + 1, secondString.Length + 1];

            for (int r = 1; r < table.GetLength(0); r++)
            {
                table[r, 0] = r * deleteCost;
            }

            for (int c = 1; c < table.GetLength(1); c++)
            {
                table[0, c] = c * insertCost;
            }

            for (int r = 1; r < table.GetLength(0); r++)
            {
                for (int c = 1; c < table.GetLength(1); c++)
                {
                    var cost = firstString[r - 1] == secondString[c - 1] ? 0 : replaceCost;

                    var del = table[r - 1, c] + deleteCost;
                    var rep = table[r - 1, c - 1] + cost;
                    var ins = table[r, c - 1] + insertCost;

                    table[r, c] = Math.Min(Math.Min(del, ins), rep);
                }
            }
            Console.WriteLine($"Minimum edit distance: {table[firstString.Length, secondString.Length]}");
        }
    }
}
