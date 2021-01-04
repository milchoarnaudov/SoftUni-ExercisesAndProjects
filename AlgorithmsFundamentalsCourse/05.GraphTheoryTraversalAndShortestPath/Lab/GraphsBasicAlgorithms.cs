namespace Lab
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class GraphsBasicAlgorithms
    {
        private bool[] visited;
        private List<int>[] undirectedGraph;
        private List<int>[] directedGraph;

        private HashSet<int> GetNodesWithIncomingEdges()
        {
            var nodeWithIncomingEdges = new HashSet<int>();

            this.directedGraph
                .SelectMany(x => x)
                .ToList()
                .ForEach(x => nodeWithIncomingEdges.Add(x));

            return nodeWithIncomingEdges;
        }

        public void Start()
        {
            #region Undirected Graph
            this.undirectedGraph = new List<int>[]
            {
                new List<int>{6, 3},
                new List<int>{2, 3, 4, 5, 6},
                new List<int>{1, 4, 5},
                new List<int>{0, 1, 5},
                new List<int>{1, 2, 6},
                new List<int>{1, 2, 3},
                new List<int>{0, 1, 4},
                new List<int>{8}, // This is the second connected component in this graph
                new List<int>{7},
            };
            this.visited = new bool[this.undirectedGraph.Length];
            var counter = 0;
            for (int i = 0; i < this.undirectedGraph.Length; i++)
            {
                if (!this.visited[i])
                {
                    Console.Write($"Connected Component {counter++}: ");
                    this.DFS(i);
                    Console.WriteLine();
                }
            }
            #endregion

            #region Directed graph
            this.directedGraph = new List<int>[]
            {
                new List<int>{1, 2},
                new List<int>{4, 3},
                new List<int>{5},
                new List<int>{2, 5},
                new List<int>{3},
                new List<int>{},
            };

            var result = new List<int>();
            var nodesWithoutEdges = new HashSet<int>();
            var nodeWithIncomingEdges = this.GetNodesWithIncomingEdges();

            for (int i = 0; i < this.directedGraph.Length; i++)
            {
                if (!nodeWithIncomingEdges.Contains(i))
                {
                    nodesWithoutEdges.Add(i);
                }
            }

            while (nodesWithoutEdges.Count > 0)
            {
                var currentNode = nodesWithoutEdges.First();
                nodesWithoutEdges.Remove(currentNode);

                result.Add(currentNode);

                var children = this.directedGraph[currentNode].ToList();
                this.directedGraph[currentNode] = new List<int>();
                var leftNodeswithIncomingEdges = this.GetNodesWithIncomingEdges();

                foreach (var child in children)
                {
                    if (!leftNodeswithIncomingEdges.Contains(child))
                    {
                        nodesWithoutEdges.Add(child);
                    }
                }
            }

            if (this.directedGraph.SelectMany(x => x).Any())
            {
                Console.WriteLine("Error: Graph has at least one cycle");
            }
            else
            {
                Console.WriteLine(string.Join(" ", result));
            }
            #endregion
        }

        private void DFS(int node)
        {
            if (!this.visited[node])
            {
                this.visited[node] = true;
                foreach (var child in this.undirectedGraph[node])
                {
                    this.DFS(child);
                }
                Console.Write($"{node} ");
            }
        }

        private void BFS(int node)
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

                Console.Write($"{current} ");

                foreach (var child in this.undirectedGraph[current])
                {
                    if (!this.visited[child])
                    {
                        queue.Enqueue(child);
                        this.visited[child] = true;
                    }
                }
            }
        }
    }
}
