namespace Lab
{
    using System;

    public class Combinations
    {
        private int[] combinations;
        public void Start()
        {
            var countOfElements = int.Parse(Console.ReadLine());
            var length = int.Parse(Console.ReadLine());
            this.combinations = new int[length];

            this.GenerateCombinationsWithoutRepetitions(0, 1, countOfElements);
        }

        private void GenerateCombinationsWithRepetitions(int currentIndex, int startIndex, int countOfElements)
        {
            if (currentIndex >= this.combinations.Length)
            {
                Console.WriteLine(string.Join(" ", combinations));
                return;
            }

            for (int i = startIndex; i <= countOfElements; i++)
            {
                this.combinations[currentIndex] = i;
                this.GenerateCombinationsWithRepetitions(currentIndex + 1, i, countOfElements);
            }
        }

        private void GenerateCombinationsWithoutRepetitions(int currentIndex, int startIndex, int countOfElements)
        {
            if (currentIndex >= this.combinations.Length)
            {
                Console.WriteLine(string.Join(" ", this.combinations));
                return;
            }

            for (int i = startIndex; i <= countOfElements; i++)
            {
                this.combinations[currentIndex] = i;
                this.GenerateCombinationsWithoutRepetitions(currentIndex + 1, i + 1, countOfElements);
            }
        }
    }
}
