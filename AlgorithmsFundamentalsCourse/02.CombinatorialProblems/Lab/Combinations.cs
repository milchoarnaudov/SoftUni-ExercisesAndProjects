namespace Lab
{
    using System;
    using System.Collections.Generic;

    public class Combinations
    {
        private string[] elements;
        private string[] combinations;

        public void GenerateCombinations()
        {
            this.elements = Console.ReadLine().Split(" ");
            var k = int.Parse(Console.ReadLine());
            this.combinations = new string[k];

            this.GenerateCombinationsWithRepetitions(0, 0);
        }

        private void GenerateCombinationsWithoutRepetitions(int index, int startIndex)
        {
            if (index >= this.combinations.Length)
            {
                Console.WriteLine(string.Join(" ", this.combinations));
                return;
            }

            for (int i = startIndex; i < this.elements.Length; i++)
            {
                this.combinations[index] = this.elements[i];
                this.GenerateCombinationsWithoutRepetitions(index + 1, i + 1);
            }
        }

        private void GenerateCombinationsWithRepetitions(int index, int startIndex)
        {
            if (index >= this.combinations.Length)
            {
                Console.WriteLine(string.Join(" ", this.combinations));
                return;
            }

            for (int i = startIndex; i < this.elements.Length; i++)
            {
                this.combinations[index] = this.elements[i];
                this.GenerateCombinationsWithRepetitions(index + 1, i);
            }
        }
    }
}
