namespace Exercises
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DistanceBetweenVertices
    {
        private Dictionary<int, List<int>> graph;

        public void Find()
        {
            int n = int.Parse(Console.ReadLine());
            int p = int.Parse(Console.ReadLine());

            this.graph = new Dictionary<int, List<int>>();
            this.FillGraph(n);
            this.FindShortestDistance(p);
        }

        private void FillGraph(int numberOfNodes)
        {
            for (int i = 0; i < numberOfNodes; i++)
            {
                var input = Console.ReadLine().Split(":").ToArray();
                var currentNode = int.Parse(input[0]);
                var edges = new List<int>();

                if (!(string.IsNullOrEmpty(input[1]) || string.IsNullOrWhiteSpace(input[1])))
                {
                    edges = input[1].Split(" ").Select(int.Parse).ToList();
                }

                graph[currentNode] = edges;
            }
        }

        private void FindShortestDistance(int numberOfPairs)
        {
            var shortedDistanceList = new List<string>();
            for (int i = 0; i < numberOfPairs; i++)
            {
                var input = Console.ReadLine().Split("-").ToArray();

                var source = int.Parse(input[0]);
                var destination = int.Parse(input[1]);

                var steps = this.GetShortestPathSteps(source, destination);
                shortedDistanceList.Add($"{{{source}, {destination}}} -> {steps}");
            }

            Console.WriteLine(string.Join(Environment.NewLine, shortedDistanceList));
        }

        private int GetShortestPathSteps(int source, int destination)
        {
            var result = -1;
            var visited = new HashSet<int>();
            var queue = new Queue<int>();
            var steps = new Dictionary<int, int>
            {
                { source, 0 }
            };

            queue.Enqueue(source);
            visited.Add(source);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current == destination)
                {
                    return steps[current];
                }

                foreach (var child in this.graph[current])
                {
                    if (!visited.Contains(child))
                    {
                        visited.Add(child);
                        queue.Enqueue(child);
                        steps[child] = steps[current] + 1;
                    }
                }
            }

            return result;
        }
    }
}
