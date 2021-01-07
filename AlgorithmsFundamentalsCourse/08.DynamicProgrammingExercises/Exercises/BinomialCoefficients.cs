namespace Exercises
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BinomialCoefficients
    {
        private Dictionary<string, long> cache;

        public BinomialCoefficients()
        {
            this.cache = new Dictionary<string, long>();
        }

        public void Calculate()
        {
            var row = int.Parse(Console.ReadLine());
            var col = int.Parse(Console.ReadLine());

            Console.WriteLine(this.GetBinom(row, col));
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
