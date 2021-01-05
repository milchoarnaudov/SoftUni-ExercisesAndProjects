using System;
using System.Collections.Generic;

namespace Lab
{
    public class LongestSubset
    {
        public void Find()
        {
            var numbers = new int[] { 3, 14, 5, 12, 15, 7, 8, 9, 11, 10, 1 };
            var solutions = new int[numbers.Length];
            var prevBestSol = new int[numbers.Length];
            var maxSolution = 0;
            var maxSolutionIndex = 0;

            for (int currentIndex = 0; currentIndex < numbers.Length; currentIndex++)
            {
                // Currently we have only 1 number as a solution and it is the current
                var solution = 1;
                var prevIndex = -1;
                var currentNumber = numbers[currentIndex];

                // We try to find solutions as searching all numbers behind the current
                for (int solIndex = 0; solIndex < currentIndex; solIndex++)
                {
                    var prevNumber = numbers[solIndex];
                    var prevSolution = solutions[solIndex];

                    if (currentNumber > prevNumber && solution <= prevSolution)
                    {
                        solution = prevSolution + 1;
                        prevIndex = solIndex;
                    }
                }

                solutions[currentIndex] = solution;
                prevBestSol[currentIndex] = prevIndex;

                if (solution > maxSolution)
                {
                    maxSolution = solution;
                    maxSolutionIndex = currentIndex;
                }
            }

            Console.WriteLine(maxSolution);

            var index = maxSolutionIndex;
            var result = new List<int>();

            while (index != -1)
            {
                var currentNumber = numbers[index];
                result.Add(currentNumber);
                index = prevBestSol[index];
            }

            result.Reverse();

            Console.WriteLine(string.Join(" ", result));
        }
    }
}
