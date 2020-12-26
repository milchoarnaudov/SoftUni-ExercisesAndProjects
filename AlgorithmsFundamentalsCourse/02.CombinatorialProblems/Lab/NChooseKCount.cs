namespace Lab
{
    using System;
    using System.Collections.Generic;

    public class NChooseKCount
    {
        public void Calculate()
        {
            var n = int.Parse(Console.ReadLine());
            var k = int.Parse(Console.ReadLine());

            Console.WriteLine(this.Binom(n, k));
        }

        private long Binom(int n, int k)
        {
            if (n <= 1 || k == 0 || k == n)
            {
                return 1;
            }

            return Binom(n - 1, k) + Binom(n - 1, k - 1);
        }
    }
}
