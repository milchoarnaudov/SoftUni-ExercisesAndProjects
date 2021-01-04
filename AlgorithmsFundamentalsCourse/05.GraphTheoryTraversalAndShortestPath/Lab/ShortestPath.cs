namespace Lab
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ShortestPath
    {
        private List<int>[] graph;
        private bool[] visited;
        private int[] parents;

        public void Start()
        {
            var n = int.Parse(Console.ReadLine());
            var e = int.Parse(Console.ReadLine());

            this.graph = this.ReadGraph(n, e);
            this.visited = new bool[this.graph.Length];
            this.parents = new int[this.graph.Length];

            Array.Fill(this.parents, -1);

            var source = int.Parse(Console.ReadLine());
            var destination = int.Parse(Console.ReadLine());

            this.BFS(source, destination);
        }

        private List<int>[] ReadGraph(int n, int e)
        {
            var result = new List<int>[n + 1];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new List<int>();
            }

            for (int i = 0; i < e; i++)
            {
                var edges = Console.ReadLine()
                    .Split(" ")
                    .Select(int.Parse)
                    .ToList();

                var from = edges[0];
                var to = edges[1];

                if (result[from] == null)
                {
                    result[from] = new List<int>();
                }

                result[from].Add(to);
            }

            return result;
        }

        private void BFS(int node, int destination)
        {
            if (this.visited[node])
            {
                return;
            }

            var queue = new Queue<int>();
            queue.Enqueue(node);
            this.visited[node] = true;

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current == destination)
                {
                    var path = this.ReconstructPath(destination);

                    Console.WriteLine($"Shortest path length is: {path.Count - 1}");
                    Console.WriteLine(string.Join(" ", path));

                    return;
                }

                foreach (var child in this.graph[current])
                {
                    if (!this.visited[child])
                    {
                        parents[child] = current;
                        queue.Enqueue(child);
                        this.visited[child] = true;
                    }
                }
            }
        }

        private Stack<int> ReconstructPath(int destination)
        {
            var path = new Stack<int>();
            var index = destination;

            while (index != -1)
            {
                path.Push(index);
                index = parents[index];
            }

            return path;
        }
    }
}
