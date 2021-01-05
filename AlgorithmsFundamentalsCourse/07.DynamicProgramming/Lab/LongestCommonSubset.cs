namespace Lab
{
    using System;
    using System.Collections.Generic;

    public class LongestCommonSubset
    {
        public void Find()
        {
            var firstSequence = Console.ReadLine();
            var secondSequence = Console.ReadLine();
            var lcs = new int[firstSequence.Length + 1, secondSequence.Length + 1];

            for (int r = 1; r < lcs.GetLength(0); r++)
            {
                for (int c = 1; c < lcs.GetLength(1); c++)
                {
                    if (firstSequence[r - 1] == secondSequence[c - 1])
                    {
                        lcs[r, c] = lcs[r - 1, c - 1] + 1;
                    }
                    else
                    {
                        lcs[r, c] = Math.Max(lcs[r, c - 1], lcs[r - 1, c]);
                    }
                }
            }

            var row = firstSequence.Length;
            var col = secondSequence.Length;
            var list = new List<char>();

            while (row > 0 && col > 0)
            {
                if (firstSequence[row - 1] == secondSequence[col - 1])
                {
                    row -= 1;
                    col -= 1;
                    list.Add(firstSequence[row]);
                }
                else if (lcs[row - 1, col] > lcs[row, col - 1])
                {
                    row -= 1;
                }
                else
                {
                    col -= 1;
                }
            }
            Console.WriteLine(list.Count);
        }
    }
}
