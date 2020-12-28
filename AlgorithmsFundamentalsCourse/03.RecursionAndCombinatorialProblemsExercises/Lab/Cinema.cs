namespace Lab
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Cinema
    {
        private List<string> personsList;
        private HashSet<int> lockedSeats;
        private string[] namesPermutations;
        private string[] seats; // Result
        private bool[] usedName;

        public void Start()
        {
            this.personsList = Console.ReadLine().Split(", ").ToList();
            this.seats = new string[personsList.Count];
            this.lockedSeats = new HashSet<int>();

            while (true)
            {
                var input = Console.ReadLine();

                if (input.ToLower() == "generate")
                {
                    break;
                }

                var inputParts = input.Split(" - ");
                var name = inputParts[0];
                var seatIndex = int.Parse(inputParts[1]) - 1; // - 1 because we want to get the actual index

                this.lockedSeats.Add(seatIndex);
                this.seats[seatIndex] = name;
                this.personsList.Remove(name);
            }

            this.namesPermutations = new string[personsList.Count];
            this.usedName = new bool[personsList.Count];
            this.GeneratePermutations(0);
        }

        private void GeneratePermutations(int index)
        {
            if (index >= namesPermutations.Length)
            {
                int permutationsIndexTemp = 0;
                for (int i = 0; i < this.seats.Length; i++)
                {
                    if (this.lockedSeats.Contains(i))
                    {
                        continue;
                    }

                    this.seats[i] = this.namesPermutations[permutationsIndexTemp];
                    permutationsIndexTemp++;
                }

                Console.WriteLine(string.Join(" ", this.seats));
                return;
            }

            for (int i = 0; i < this.personsList.Count; i++)
            {
                if (!this.usedName[i])
                {
                    this.usedName[i] = true;
                    this.namesPermutations[index] = this.personsList[i];
                    this.GeneratePermutations(index + 1);
                    this.usedName[i] = false;
                }
            }
        }
    }
}
