namespace Exercises
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SumWithLimitedAmountOfCoins
    {
        public void Find()
        {
            var numbers = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var target = int.Parse(Console.ReadLine());

            var sum = this.CalcSums(numbers);
            Console.WriteLine(sum[target]);
        }

        private Dictionary<int, int> CalcSums(int[] numbers)
        {
            var result = new Dictionary<int, int>() { { 0, 1 } };

            foreach (var number in numbers)
            {
                var sums = result.Keys.ToArray();

                foreach (var sum in sums)
                {
                    var newSum = sum + number;

                    if (!result.ContainsKey(newSum))
                    {
                        result.Add(newSum, 1);
                    }
                    else
                    {
                        result[newSum] += 1;
                    }
                }
            }

            return result;
        }
    }
}
