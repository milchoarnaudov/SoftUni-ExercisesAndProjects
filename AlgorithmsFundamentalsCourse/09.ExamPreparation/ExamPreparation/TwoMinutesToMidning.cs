namespace ExamPreparation
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class TwoMinutesToMidning
    {
        private Dictionary<string, long> cache;

        public void Calculate()
        {
            var n = int.Parse(Console.ReadLine());
            int k = int.Parse(Console.ReadLine());

            this.cache = new Dictionary<string, long>();

            var ways = this.GetBinom(n, k);
            Console.WriteLine(ways);
        }

        private long GetBinom(int row, int col)
        {
            var key = $"{row} {col}";

            if (this.cache.ContainsKey(key))
            {
                return this.cache[key];
            }

            if (col == 0 || col == row)
            {
                return 1;
            }

            var result = this.GetBinom(row - 1, col) + this.GetBinom(row - 1, col - 1);
            this.cache.Add(key, result);
            return result;
        }
    }
}
