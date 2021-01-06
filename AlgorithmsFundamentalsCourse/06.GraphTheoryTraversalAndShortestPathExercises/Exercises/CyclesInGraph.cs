namespace Exercises
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CyclesInGraph
    {
        private Dictionary<string, List<string>> graph;
        private HashSet<string> visited;
        private HashSet<string> cycles;
        private bool result;

        public void PrintIsCyclic()
        {
            this.graph = new Dictionary<string, List<string>>();
            this.visited = new HashSet<string>();
            this.cycles = new HashSet<string>();

            while (true)
            {
                var input = Console.ReadLine();

                if (input.ToLower() == "end")
                {
                    break;
                }

                var parts = input.Split("-");
                var node = parts[0];
                var edge = parts[1];

                if (!graph.ContainsKey(node))
                {
                    graph.Add(node, new List<string>());
                }

                if (!graph.ContainsKey(edge))
                {
                    graph.Add(edge, new List<string>());
                }

                graph[node].Add(edge);
            }

            foreach (var kvp in graph)
            {
                this.DFS(kvp.Key);
            }

            var isAcyclic = this.result ? "No" : "Yes";

            Console.WriteLine($"Acyclic: {isAcyclic}");
        }

        private void DFS(string node)
        {
            if (cycles.Contains(node))
            {
                this.result = true;
                return;
            }

            if (this.visited.Contains(node))
            {
                return;
            }

            this.visited.Add(node);
            this.cycles.Add(node);

            foreach (var child in this.graph[node])
            {
                this.DFS(child);
            }

            this.cycles.Remove(node);
        }
    }
}
