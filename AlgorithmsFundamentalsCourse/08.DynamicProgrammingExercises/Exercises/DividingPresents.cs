namespace Exercises
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DividingPresents
    {
        public void Find()
        {
            var presents = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var sums = this.CalcSum(presents);

            var presentsTotalSum = presents.Sum();
            var bobSum = this.GetBobSum(sums, presentsTotalSum);

            var alanSum = presentsTotalSum - bobSum;
            var alanPresents = this.GetPresents(sums, alanSum);

            Console.WriteLine($"Difference: {bobSum - alanSum}");
            Console.WriteLine($"Alan:{alanSum} Bob:{bobSum}");
            Console.WriteLine($"Alan takes: {string.Join(" ", alanPresents)}");
            Console.WriteLine("Bob takes the rest.");
        }

        private List<int> GetPresents(Dictionary<int, int> sums, int target)
        {
            var result = new List<int>();

            while (target != 0)
            {
                var present = sums[target];
                result.Add(present);

                target -= present;
            }

            return result;
        }

        private int GetBobSum(Dictionary<int, int> sums, int totalSum)
        {
            var bobSum = (int)Math.Ceiling(totalSum / 2.0);

            while (!sums.ContainsKey(bobSum))
            {
                bobSum++;
            }

            return bobSum;
        }

        private Dictionary<int, int> CalcSum(int[] numbers)
        {
            var result = new Dictionary<int, int> { { 0, 0 } };

            foreach (var number in numbers)
            {
                var sums = result.Keys.ToArray();

                foreach (var sum in sums)
                {
                    var newSum = sum + number;

                    if (!result.ContainsKey(newSum))
                    {
                        result.Add(newSum, number);
                    }
                }

            }

            return result;
        }
    }
}
