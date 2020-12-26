namespace Lab
{
    using System;
    using System.Collections.Generic;

    public class Permutations
    {
        private string[] elements;
        private string[] permutations;
        private bool[] used;

        public void GeneratePermutations()
        {
            this.elements = Console.ReadLine().Split(" ");
            this.permutations = new string[elements.Length];
            this.used = new bool[elements.Length];

            this.GeneratePermutationsWithRepetitions(0);
        }

        private void GeneratePermutationsWithoutRepetitions(int index)
        {
            if (index >= this.permutations.Length)
            {
                Console.WriteLine(string.Join(" ", this.permutations));
                return;
            }

            for (int i = 0; i < this.elements.Length; i++)
            {
                if (!this.used[i])
                {
                    this.used[i] = true;
                    this.permutations[index] = this.elements[i];
                    this.GeneratePermutationsWithoutRepetitions(index + 1);
                    this.used[i] = false;
                }
            }
        }

        private void GeneratePermutationsWithRepetitions(int index)
        {
            if (index >= this.elements.Length)
            {
                Console.WriteLine(string.Join(" ", this.elements));
                return;
            }

            this.GeneratePermutationsWithRepetitions(index + 1);
            var swapped = new HashSet<string> { elements[index] };

            for (int i = index + 1; i < elements.Length; i++)
            {
                if (!swapped.Contains(elements[i]))
                {
                    this.Swap(index, i);
                    this.GeneratePermutationsWithRepetitions(index + 1);
                    this.Swap(index, i);
                    swapped.Add(elements[i]);
                }
            }
        }

        private void Swap(int first, int second)
        {
            var temp = this.elements[first];
            elements[first] = elements[second];
            elements[second] = temp;
        }
    }
}
