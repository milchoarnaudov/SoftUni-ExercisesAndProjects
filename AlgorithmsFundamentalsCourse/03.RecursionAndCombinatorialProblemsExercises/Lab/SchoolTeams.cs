namespace Lab
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class SchoolTeams
    {
        private List<string[]> girlsCombinationsList;
        private List<string[]> boysCombinationsList;

        public void Start()
        {
            var girls = Console.ReadLine().Split(", ");
            var boys = Console.ReadLine().Split(", ");

            this.girlsCombinationsList = new List<string[]>();
            this.boysCombinationsList = new List<string[]>();

            this.GenerateCombinations(0, 0, new string[3], girls, this.girlsCombinationsList);
            this.GenerateCombinations(0, 0, new string[2], boys, this.boysCombinationsList);

            foreach (var currentGirlsCombination in girlsCombinationsList)
            {
                foreach (var currentBoysCombination in boysCombinationsList)
                {
                    Console.WriteLine($"{string.Join(", ", currentGirlsCombination)}, {string.Join(", ", currentBoysCombination)}");
                }
            }
        }

        private void GenerateCombinations(int combinationsIndex,
            int elementsIndex, string[] combinations, string[] elements, List<string[]> combinationsList)
        {
            if (combinationsIndex >= combinations.Length)
            {
                combinationsList.Add(combinations.ToArray());
                return;
            }

            for (int i = elementsIndex; i < elements.Length; i++)
            {
                combinations[combinationsIndex] = elements[i];
                this.GenerateCombinations(combinationsIndex + 1, i + 1, combinations, elements, combinationsList);
            }
        }
    }
}
