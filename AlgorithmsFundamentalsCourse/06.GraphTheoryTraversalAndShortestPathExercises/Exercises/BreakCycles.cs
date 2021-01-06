using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercises
{
    public class Edge
    {
        public Edge(string first, string second)
        {
            this.First = first;
            this.Second = second;
        }

        public string First { get; set; }

        public string Second { get; set; }

        public override string ToString()
        {
            return $"{this.First} - {this.Second}";
        }

        public string ToStringReversed()
        {
            return $"{this.Second} - {this.First}";
        }
    }

    public class BreakCycles
    {
        private Dictionary<string, List<string>> graph;
        private List<Edge> edges;

        public void Find()
        {
            var n = int.Parse(Console.ReadLine());

            this.graph = new Dictionary<string, List<string>>();
            this.edges = new List<Edge>();

            this.ReadInput(n);

            this.edges = this.edges
                .OrderBy(e => e.First)
                .ThenBy(e => e.Second)
                .ToList();

            var removedEdges = new List<Edge>();
            var blacklisted = new HashSet<string>();

            foreach (var edge in this.edges)
            {
                var first = edge.First;
                var second = edge.Second;

                this.graph[first].Remove(second);
                this.graph[second].Remove(first);

                if (this.HasPath(first, second))
                {
                    if (blacklisted.Contains(edge.ToString()))
                    {
                        continue;
                    }
                    removedEdges.Add(edge);
                    blacklisted.Add(edge.ToStringReversed());
                }
                else
                {
                    this.graph[first].Add(second);
                    this.graph[second].Add(first);
                }
            }

            Console.WriteLine($"Edges to remove: {removedEdges.Count}");
            foreach (var edge in removedEdges)
            {
                Console.WriteLine(edge.ToString());
            }
        }

        private bool HasPath(string source, string destination)
        {
            var queue = new Queue<string>();
            queue.Enqueue(source);

            var visited = new HashSet<string> { source };

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (node == destination)
                {
                    return true;
                }

                foreach (var child in this.graph[node])
                {
                    if (visited.Contains(child))
                    {
                        continue;
                    }

                    visited.Add(child);
                    queue.Enqueue(child);
                }
            }

            return false;
        }

        private void ReadInput(int n)
        {
            for (int i = 0; i < n; i++)
            {
                var parts = Console.ReadLine().Split(" -> ", StringSplitOptions.RemoveEmptyEntries);
                var node = parts[0];
                var children = parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (!this.graph.ContainsKey(node))
                {
                    this.graph.Add(node, new List<string>());
                }

                foreach (var child in children)
                {
                    this.graph[node].Add(child);
                    this.edges.Add(new Edge(node, child));
                }
            }
        }
    }
}
