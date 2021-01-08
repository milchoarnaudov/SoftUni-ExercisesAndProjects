namespace ExamPreparation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TheStoryTellingDfs
    {
        private Dictionary<string, List<string>> graph;
        private HashSet<string> visited;
        private Stack<string> result;

        public void Find()
        {
            this.graph = new Dictionary<string, List<string>>();
            this.visited = new HashSet<string>();
            this.result = new Stack<string>();

            this.ReadGraph();

            foreach (var node in this.graph.Keys)
            {
                this.DFS(node);
            }

            Console.WriteLine(string.Join(" ", result));
        }

        private void DFS(string node)
        {
            if (this.visited.Contains(node))
            {
                return;
            }

            visited.Add(node);

            foreach (var child in this.graph[node])
            {
                this.DFS(child);
            }

            this.result.Push(node);
        }

        private void ReadGraph()
        {
            while (true)
            {
                var line = Console.ReadLine();

                if (line.ToLower() == "end")
                {
                    break;
                }

                var parts = line.Split("->", StringSplitOptions.RemoveEmptyEntries);
                var preStory = parts[0].Trim();

                if (!graph.ContainsKey(preStory))
                {
                    graph.Add(preStory, new List<string>());
                }

                if (parts.Length == 1)
                {
                    continue;
                }

                var postStories = parts[1].Trim().Split();
                graph[preStory].AddRange(postStories);
            }
        }
    }
}
