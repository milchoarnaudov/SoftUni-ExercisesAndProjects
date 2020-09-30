using System;
using Wintellect.PowerCollections;

namespace _04.CookiesProblem
{
    public class CookiesProblem
    {
        public int Solve(int k, int[] cookies)
        {
            OrderedBag<int> bag = new OrderedBag<int>();

            foreach (var cookie in cookies)
            {
                bag.Add(cookie);
            }

            int currentMinSweetness = bag.GetFirst();
            int operationsCount = 0;

            while (currentMinSweetness < k && bag.Count > 1)
            {
                int leastSweetCookie = bag.RemoveFirst();
                int secondLeastSweetCookie = bag.RemoveFirst();

                int combined = leastSweetCookie + (2 * secondLeastSweetCookie);

                bag.Add(combined);

                currentMinSweetness = bag.GetFirst();
                operationsCount++;
            }

            return currentMinSweetness < k ? -1 : operationsCount;
        }
    }
}
