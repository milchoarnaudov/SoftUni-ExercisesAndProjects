namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> _children;

        public Tree(T key, params Tree<T>[] children)
        {
            this.Key = key;
            this._children = children.ToList();
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }


        public IReadOnlyCollection<Tree<T>> Children
            => this._children.AsReadOnly();

        public void AddChild(Tree<T> child)
        {
            this._children.Add(child);
        }

        public void AddParent(Tree<T> parent)
        {
            this.Parent = parent;
        }

        public string GetAsString()
        {
            StringBuilder sb = new StringBuilder();
            this.GetAsStringDfs(this, sb, 0);

            return sb.ToString().Trim();
        }

        public Tree<T> GetDeepestLeftomostNode()
        {
            var leafNodes = this.BfsTraversalNodes()
                .Where(node => this.IsLeaf(node));

            int deepestNodeDepth = 0;

            Tree<T> deepestNode = null;

            foreach (var node in leafNodes)
            {
                int currentDepth = this.GetDepthFromLeafToParent(node);
                if (currentDepth > deepestNodeDepth)
                {
                    deepestNodeDepth = currentDepth;
                    deepestNode = node;
                }
            }

            return deepestNode;
        }

      

        public List<T> GetLeafKeys()
        {
            Func<Tree<T>, bool> leafKeyPredicate =
                (node) => this.IsLeaf(node);

            return this.BfsTraversal(leafKeyPredicate);
        }

        public List<T> GetMiddleKeys()
        {
            Func<Tree<T>, bool> middleKeysPredicate =
                (node) => this.IsMiddle(node);

            return this.BfsTraversal(middleKeysPredicate);
        }

        public List<T> GetLongestPath()
        {
            var deepestNode = this.GetDeepestLeftomostNode();

            var currentNode = deepestNode;
            var resultPath = new Stack<T>();

            while (currentNode != null)
            {
                resultPath.Push(currentNode.Key);
                currentNode = currentNode.Parent;
            }

            return new List<T>(resultPath);
        }

        public List<List<T>> PathsWithGivenSum(int sum)
        {
            var result = new List<List<T>>();
            int currentSum = Convert.ToInt32(this.Key);
            var currentPath = new List<T>();
            currentPath.Add(this.Key);

            this.DfsGetPath(this, result, currentPath, ref currentSum, sum);
            return result;
        }

        public List<Tree<T>> SubTreesWithGivenSum(int sum)
        {
            var subtreesWithGivenSum = new List<Tree<T>>();
            var allNodes = this.BfsTraversalNodes();

            foreach (var node in allNodes)
            {
                int subtreeSum = this.GetSubtreeSumDfs(node);

                if (subtreeSum == sum)
                {
                    subtreesWithGivenSum.Add(node);
                }
            }

            return subtreesWithGivenSum;
        }

        private void GetAsStringDfs(Tree<T> tree, StringBuilder sb, int depth)
        {
            sb.Append(new String(' ', depth)).Append(tree.Key).Append(Environment.NewLine);

            foreach (var child in tree.Children)
            {
                GetAsStringDfs(child, sb, depth + 2);
            }
        }

        private List<T> BfsTraversal(Func<Tree<T>, bool> predicate)
        {
            List<T> result = new List<T>();
            Queue<Tree<T>> nodes = new Queue<Tree<T>>();
            nodes.Enqueue(this);

            while (nodes.Count != 0)
            {
                Tree<T> currentNode = nodes.Dequeue();

                if (predicate.Invoke(currentNode))
                {
                    result.Add(currentNode.Key);
                }

                foreach (var child in currentNode.Children)
                {
                    nodes.Enqueue(child);
                }
            }

            return result;
        }

        private List<Tree<T>> BfsTraversalNodes()
        {
            List<Tree<T>> result = new List<Tree<T>>();
            Queue<Tree<T>> nodes = new Queue<Tree<T>>();
            nodes.Enqueue(this);

            while (nodes.Count != 0)
            {
                Tree<T> currentNode = nodes.Dequeue();

                result.Add(currentNode);

                foreach (var child in currentNode.Children)
                {
                    nodes.Enqueue(child);
                }
            }

            return result;
        }

        private bool IsLeaf(Tree<T> node) 
        {
            return node.Children.Count == 0;
        }

        private bool IsMiddle(Tree<T> node)
        {
            return node.Parent != null && node.Children.Count > 0;
        }

        private int GetDepthFromLeafToParent(Tree<T> node)
        {
            int depth = 0;
            Tree<T> current = node;

            while (current.Parent != null)
            {
                depth++;
                current = current.Parent;
            }

            return depth;
        }

        private void DfsGetPath(Tree<T> current, List<List<T>> wantedPaths, List<T> currentPath, ref int currentSum, int wantedSum)
        {
            foreach (var child in current.Children)
            {
                currentPath.Add(child.Key);
                currentSum += Convert.ToInt32(child.Key);

                this.DfsGetPath(child, wantedPaths, currentPath, ref currentSum, wantedSum);
            }

            if (currentSum == wantedSum)
            {
                wantedPaths.Add(new List<T>(currentPath));
            }

            currentSum -= Convert.ToInt32(current.Key);
            currentPath.RemoveAt(currentPath.Count - 1);
        }

        private int GetSubtreeSumDfs(Tree<T> currentNode)
        {
            int currentSum = Convert.ToInt32(currentNode.Key);
            int childSum = 0;

            foreach (var childNode in currentNode.Children)
            {
                childSum += this.GetSubtreeSumDfs(childNode);
            }

            return currentSum + childSum;
        }
    }
}
