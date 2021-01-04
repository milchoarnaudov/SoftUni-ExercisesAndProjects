namespace Lab
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class TopologicalSorting
    {
        private Dictionary<string, List<string>> graph;
        private Dictionary<string, int> dependencies;

        public void Start()
        {
            var graphSize = int.Parse(Console.ReadLine());

            this.graph = this.ReadGraph(graphSize);
            this.dependencies = this.ExtractDependencies();

            var sortedGraph = this.TopologicalSort();
            if (sortedGraph == null)
            {
                Console.WriteLine("Invalid topological sorting");
            }
            else
            {
                Console.WriteLine($"Topological sorting: {string.Join(", ", sortedGraph)}");
            }
        }

        private Dictionary<string, List<string>> ReadGraph(int graphSize)
        {
            var graphResult = new Dictionary<string, List<string>>();

            for (int i = 0; i < graphSize; i++)
            {
                var parts = Console.ReadLine()
                    .Split("->", StringSplitOptions.RemoveEmptyEntries);
                var key = parts[0].Trim();

                if (parts.Length == 1)
                {
                    graphResult[key] = new List<string>();
                }
                else
                {
                    var children = parts[1]
                        .Trim()
                        .Split(", ")
                        .ToList();

                    graphResult[key] = children;
                }
            }

            return graphResult;
        }

        private List<string> TopologicalSort()
        {
            var sortedGraph = new List<string>();

            while (dependencies.Count > 0)
            {
                var nodeToRemove = this.dependencies
                    .FirstOrDefault(x => x.Value == 0);

                if (string.IsNullOrEmpty(nodeToRemove.Key))
                {
                    break;
                }

                var node = nodeToRemove.Key;
                var children = this.graph[node];

                sortedGraph.Add(node);

                foreach (var child in children)
                {
                    dependencies[child] -= 1;
                }

                dependencies.Remove(nodeToRemove.Key);
            }

            if (dependencies.Count > 0)
            {
                return null;
            }

            return sortedGraph;
        }

        private Dictionary<string, int> ExtractDependencies()
        {
            var result = new Dictionary<string, int>();

            foreach (var kvp in this.graph)
            {
                var node = kvp.Key;
                var children = kvp.Value;

                if (!result.ContainsKey(node))
                {
                    result.Add(node, 0);
                }

                foreach (var child in children)
                {
                    if (!result.ContainsKey(child))
                    {
                        result.Add(child, 1);
                    }
                    else
                    {
                        result[child] += 1;
                    }
                }
            }

            return result;
        }
    }
}
