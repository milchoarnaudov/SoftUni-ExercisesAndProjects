namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> _children;

        public Tree(T value)
        {
            this.Value = value;
            this.Parent = null;
            this._children = new List<Tree<T>>();
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (var child in children)
            {
                child.Parent = this;
                this._children.Add(child);
            }
        }

        public T Value { get; private set; }
        public Tree<T> Parent { get; private set; }
        public IReadOnlyCollection<Tree<T>> Children => this._children.AsReadOnly();
        public bool IsRootDeleted { get; private set; }

        public ICollection<T> OrderBfs()
        {
            List<T> result = new List<T>();
            Queue<Tree<T>> queue = new Queue<Tree<T>>();

            if (this.IsRootDeleted)
            {
                return result;
            }

            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                var subTree = queue.Dequeue();
                result.Add(subTree.Value);

                foreach (var child in subTree.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }

        public ICollection<T> OrderDfs()
        {
            List<T> result = new List<T>();

            if (this.IsRootDeleted)
            {
                return result;
            }

            this.DfsRecursiveTraversal(this, result);
            return result;
        }

        public void AddChild(T parentKey, Tree<T> child)
        {
            Tree<T> parentSubtree = this.FindBfs(parentKey);

            if (parentSubtree == null)
            {
                throw new ArgumentNullException();
            }

            parentSubtree._children.Add(child);
        }


        public void RemoveNode(T nodeKey)
        {
            var currentNode = this.FindBfs(nodeKey);

            if (currentNode == null)
            {
                throw new ArgumentNullException();
            }

            foreach (var child in currentNode.Children)
            {
                child.Parent = null;
            }

            currentNode._children.Clear();

            var parentNode = currentNode.Parent;

            if (parentNode == null)
            {
                this.IsRootDeleted = true;
            }
            else
            {
                parentNode._children.Remove(currentNode);
            }

            currentNode.Value = default;
        }

        public void Swap(T firstKey, T secondKey)
        {
            var firstNode = this.FindBfs(firstKey);
            var secondNode = this.FindBfs(secondKey);

            if (firstNode == null || secondNode == null)
            {
                throw new ArgumentNullException();
            }

            var firstParent = firstNode.Parent;
            var secondParent = secondNode.Parent;


            if (firstParent == null)
            {
                SwapRoot(secondNode);
                return;
            }

            if (secondParent == null)
            {
                SwapRoot(firstNode);
                return;
            }

            firstNode.Parent = secondParent;
            secondNode.Parent = firstParent;

            int indexOfFirst = firstParent._children.IndexOf(firstNode);
            int indexOfSecond = secondParent._children.IndexOf(secondNode);

            firstParent._children[indexOfFirst] = secondNode;
            secondParent._children[indexOfSecond] = firstNode;
        }

        private void DfsRecursiveTraversal(Tree<T> subtree, List<T> result)
        {
            foreach (var child in subtree.Children)
            {
                this.DfsRecursiveTraversal(child, result);
            }

            result.Add(subtree.Value);
        }

        private Tree<T> FindBfs(T value)
        {
            Queue<Tree<T>> queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var subtree = queue.Dequeue();

                if (subtree.Value.Equals(value))
                {
                    return subtree;
                }

                foreach (var child in subtree.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }

        private Tree<T> FindDfs(T value, Tree<T> subtree)
        {
            foreach (var child in subtree.Children)
            {
                Tree<T> current = this.FindDfs(value, child);

                if (current.Value != null && current.Value.Equals(value))
                {
                    return current;
                }
            }

            if (subtree.Value.Equals(value))
            {
                return subtree;
            }

            return null;
        }

        private void SwapRoot(Tree<T> node)
        {
            this.Value = node.Value;
            this._children.Clear();

            foreach (var child in node.Children)
            {
                this._children.Add(child);
            }
        }
    }
}
