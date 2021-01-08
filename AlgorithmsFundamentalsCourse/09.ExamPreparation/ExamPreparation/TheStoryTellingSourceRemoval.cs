namespace ExamPreparation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class TheStoryTellingSourceRemoval
    {
        private Dictionary<string, List<string>> graph;
        public void Find()
        {
            this.graph = new Dictionary<string, List<string>>();

            this.ReadGraph();
            var dependencies = this.ExtractDependencies();
            var output = new List<string>();

            while (dependencies.Count > 0)
            {
                var nodeToRemove = dependencies.FirstOrDefault(x => x.Value == 0).Key;

                dependencies.Remove(nodeToRemove);

                foreach (var child in graph[nodeToRemove])
                {
                    dependencies[child] -= 1;
                }

                output.Add(nodeToRemove);
            }

            Console.WriteLine(string.Join(" ", output));
        }

        private Dictionary<string, int> ExtractDependencies()
        {
            var dependencies = new Dictionary<string, int>();

            foreach (var node in this.graph.Keys)
            {
                if (!dependencies.ContainsKey(node))
                {
                    dependencies.Add(node, 0);
                }

                foreach (var child in this.graph[node])
                {
                    if (!dependencies.ContainsKey(child))
                    {
                        dependencies.Add(child, 1);
                    }
                    else
                    {
                        dependencies[child] += 1;
                    }
                }
            }

            return dependencies;
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

                if (!this.graph.ContainsKey(preStory))
                {
                    this.graph.Add(preStory, new List<string>());
                }

                if (parts.Length == 1)
                {
                    continue;
                }

                var postStories = parts[1].Trim().Split();
                this.graph[preStory].AddRange(postStories);
            }
        }
    }
}
