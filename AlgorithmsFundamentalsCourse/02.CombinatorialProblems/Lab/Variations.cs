namespace Lab
{
    using System;
    using System.Collections.Generic;

    public class Variations
    {
        private string[] elements;
        private string[] variations;
        private bool[] used;

        public void GenerateVariations()
        {
            this.elements = Console.ReadLine().Split(" ");
            var k = int.Parse(Console.ReadLine());
            this.variations = new string[k];
            this.used = new bool[elements.Length];

            this.GenerateVariationsWithRepetitions(0);
        }

        private void GenerateVariationsWithoutRepetitions(int index)
        {
            if (index >= this.variations.Length)
            {
                Console.WriteLine(string.Join(" ", this.variations));
                return;
            }

            for (int i = 0; i < this.elements.Length; i++)
            {
                if (!this.used[i])
                {
                    this.used[i] = true;
                    this.variations[index] = this.elements[i];
                    this.GenerateVariationsWithoutRepetitions(index + 1);
                    this.used[i] = false;
                }
            }
        }

        private void GenerateVariationsWithRepetitions(int index)
        {
            if (index >= this.variations.Length)
            {
                Console.WriteLine(string.Join(" ", this.variations));
                return;
            }

            for (int i = 0; i < this.elements.Length; i++)
            {
                this.variations[index] = this.elements[i];
                this.GenerateVariationsWithRepetitions(index + 1);
            }
        }
    }
}
