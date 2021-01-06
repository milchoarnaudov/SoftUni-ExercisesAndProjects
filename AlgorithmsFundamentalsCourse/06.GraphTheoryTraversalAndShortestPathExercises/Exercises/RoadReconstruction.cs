namespace Exercises
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Street
    {
        public Street(int firstBuilding, int secondBuilding)
        {
            this.FirstBuilding = firstBuilding;
            this.SecondBuilding = secondBuilding;
        }

        public int FirstBuilding { get; set; }

        public int SecondBuilding { get; set; }

        public override string ToString()
        {
            if (this.FirstBuilding < this.SecondBuilding)
            {
                return $"{FirstBuilding} {SecondBuilding}";
            }
            else
            {
                return $"{SecondBuilding} {FirstBuilding}";
            }
        }
    }

    public class RoadReconstruction
    {
        private Dictionary<int, List<int>> graph;
        private List<Street> streets;

        public void Find()
        {
            var buildingsCount = int.Parse(Console.ReadLine());
            var streetsCount = int.Parse(Console.ReadLine());

            this.streets = new List<Street>();
            this.graph = this.ReadInput(buildingsCount, streetsCount);

            var importantStreets = new List<Street>();
            foreach (var street in this.streets)
            {
                var firstBuilding = street.FirstBuilding;
                var secondBuilding = street.SecondBuilding;

                this.graph[firstBuilding].Remove(secondBuilding);
                this.graph[secondBuilding].Remove(firstBuilding);

                if (this.IsImportant(firstBuilding, secondBuilding))
                {
                    importantStreets.Add(street);
                }

                this.graph[firstBuilding].Add(secondBuilding);
                this.graph[secondBuilding].Add(firstBuilding);
            }

            Console.WriteLine("Important streets:");
            foreach (var currStreet in importantStreets)
            {
                Console.WriteLine(currStreet);
            }
        }

        private Dictionary<int, List<int>> ReadInput(int buildingsCount, int streetsCount)
        {
            var result = new Dictionary<int, List<int>>(buildingsCount);

            for (int i = 0; i < streetsCount; i++)
            {
                var buildings = Console.ReadLine()
                    .Split(" - ");

                var firstBulding = int.Parse(buildings[0]);
                var secondBulding = int.Parse(buildings[1]);

                if (!result.ContainsKey(firstBulding))
                {
                    result[firstBulding] = new List<int>();
                }

                if (!result.ContainsKey(secondBulding))
                {
                    result[secondBulding] = new List<int>();
                }

                result[firstBulding].Add(secondBulding);
                result[secondBulding].Add(firstBulding);

                var street = new Street(firstBulding, secondBulding);
                this.streets.Add(street);
            }

            return result;
        }

        private bool IsImportant(int firstBuilding, int secondBuilding)
        {
            var queue = new Queue<int>();
            queue.Enqueue(firstBuilding);

            var visited = new HashSet<int>
            {
                firstBuilding
            };

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node == secondBuilding)
                {
                    return false;
                }

                foreach (var child in this.graph[node])
                {
                    if (!visited.Contains(child))
                    {
                        visited.Add(child);
                        queue.Enqueue(child);
                    }
                }
            }

            return true;
        }
    }
}
