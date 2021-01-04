namespace Lab
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ConnectedComponents
    {
        private List<int>[] graph;
        private bool[] visited;

        public void Start()
        {
            var graphSize = int.Parse(Console.ReadLine());
            this.graph = new List<int>[graphSize];
            this.visited = new bool[graphSize];

            ReadGraph();

            for (int i = 0; i < graph.Length; i++)
            {
                if (!this.visited[i])
                {
                    Console.Write("Connected component: ");
                    this.DFS(i);
                    Console.WriteLine();
                }
            }
        }

        private void ReadGraph()
        {
            for (int i = 0; i < this.graph.Length; i++)
            {
                var input = Console.ReadLine();
                var edges = new List<int>();

                if (!(string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input)))
                {
                    edges = input.Split(" ").Select(int.Parse).ToList();
                }
                
                this.graph[i] = edges;
            }
        }

        private void DFS(int node)
        {
            if (!visited[node])
            {
                visited[node] = true;
                foreach (var child in graph[node])
                {
                    DFS(child);
                }
                Console.Write($"{node} ");
            }
        }
    }
}
