namespace _02.WordCruncher
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Solution
    {
        private SortedSet<string> results = new SortedSet<string>();
        private List<Node> permutation = new List<Node>();

        public Solution(string[] input, string expected)
        {
            permutation = GeneratePermutations(input.OrderBy(s => s).ToList(), expected);

            foreach (var path in this.GetAllPaths())
            {
                var result = string.Join(" ", path);
                if (!this.results.Contains(result))
                {
                    this.results.Add(result);
                }
            }
        }

        public IEnumerable<string> GetPaths()
        {
            foreach (var result in this.results)
            {
                yield return result;
            }
        }

        public IEnumerable<IEnumerable<string>> GetAllPaths()
        {
            List<string> way = new List<string>();
            foreach (var key in VisitPath(permutation, new List<string>()))
            {
                if (key == null)
                {
                    yield return way;
                    way = new List<string>();
                }
                else
                {
                    way.Add(key);
                }
            }
        }

        private List<Node> GeneratePermutations(List<string> input, string target)
        {
            if (string.IsNullOrEmpty(target) || input.Count() == 0)
            {
                return null;
            }

            List<Node> returnValues = null;

            for (int i = 0; i < input.Count(); i++)
            {
                var key = input[i];
                if (target.StartsWith(key))
                {
                    var node = new Node()
                    {
                        Key = key,
                        Value = GeneratePermutations(input.Where((s, index) => index != i).ToList(),
                        target.Substring(key.Length))
                    };

                    if (node.Value == null && node.Key != target)
                    {
                        continue;
                    }

                    if (returnValues == null)
                        returnValues = new List<Node>();
                    returnValues.Add(node);
                }
            }

            return returnValues;
        }

        private IEnumerable<string> VisitPath(List<Node> permutation, List<string> path)
        {
            if (permutation == null)
            {
                foreach (var pathItem in path)
                {
                    yield return pathItem;
                }

                yield return null;
            }
            else
            {
                foreach (Node node in permutation)
                {
                    path.Add(node.Key);
                    foreach (var item in VisitPath(node.Value, path))
                    {
                        yield return item;
                    }

                    path.RemoveAt(path.Count - 1);
                }
            }
        }
    }
}
