namespace Exercises
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class SumWithUnlimitedAmountOfCoins
    {
        public void Find()
        {
            var coins = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var target = int.Parse(Console.ReadLine());
            var count = this.GetCount(coins, target);
            Console.WriteLine(count);
        }

        private int GetCount(int[] coins, int target)
        {
            var sums = new int[target + 1];
            sums[0] = 1;

            foreach (var coin in coins)
            {
                for (int sum = coin; sum < sums.Length; sum++)
                {
                    sums[sum] += sums[sum - coin];
                }
            }

            return sums[target];
        }
    }
}
