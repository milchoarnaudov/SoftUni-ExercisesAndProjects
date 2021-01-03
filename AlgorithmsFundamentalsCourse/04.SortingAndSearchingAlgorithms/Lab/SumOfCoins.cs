namespace Lab
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class SumOfCoins
    {
        public void Start()
        {
            var input = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
            var sortedCoins = new SortedSet<int>(input);
            var target = int.Parse(Console.ReadLine());
            var result = 0;
            var sb = new StringBuilder();

            while (target > 0 && sortedCoins.Count > 0)
            {
                var maxCoin = sortedCoins.Max;
                sortedCoins.Remove(maxCoin);

                if (maxCoin > target)
                    continue;

                var counter = target / maxCoin;
                result += counter;
                target -= maxCoin * counter;
                sb.AppendLine($"{counter} coin(s) with value {maxCoin}");
            }

            if (target > 0)
            {
                Console.WriteLine("Error");
            }
            else
            {
                Console.WriteLine($"Number of coins to take: {result}");
                Console.WriteLine(sb.ToString());
            }
        }
    }
}
