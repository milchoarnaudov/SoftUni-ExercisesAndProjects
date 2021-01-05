namespace Lab
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class FibonacciNumbers
    {
        private readonly int[] cache;

        public FibonacciNumbers()
        {
            this.cache = new int[100];
        }

        public void Calculate(int n)
        {
            Console.WriteLine(this.IterativeFibonnaci(n));
        }

        private int RecursiveFibonacci(int n)
        {
            if (n <= 2)
            {
                return 1;
            }

            return this.RecursiveFibonacci(n - 1) + this.RecursiveFibonacci(n - 2);
        }

        private int RecursiveFibonacciWithMemoization(int n)
        {
            if (this.cache[n] != 0)
            {
                return this.cache[n];
            }

            if (n <= 2)
            {
                return 1;
            }

            var result = this.RecursiveFibonacci(n - 1) + this.RecursiveFibonacci(n - 2);
            this.cache[n] = result;

            return result;
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
    }
}
