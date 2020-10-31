namespace _01.Hierarchy
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;

    public class Hierarchy<T> : IHierarchy<T>
    {
        private Node<T> root;
        private Dictionary<T, Node<T>> elements;

        public Hierarchy(T root)
        {
            this.elements = new Dictionary<T, Node<T>>();
            this.root = this.CreateNode(root);
        }

        public int Count => this.elements.Count;

        public void Add(T element, T child)
        {
            this.ContainsItemOrThrowException(element);
            if (elements.ContainsKey(child))
            {
                throw new ArgumentException();
            }

            var node = this.CreateNode(child);
            node.Parent = elements[element];
            elements[element].Children.Add(node);

        }

        public void Remove(T element)
        {
            if (this.root.Value.Equals(element))
            {
                throw new InvalidOperationException();
            }

            this.ContainsItemOrThrowException(element);

            var node = this.elements[element];
            var nodeChildren = node.Children;
            var nodeParent = node.Parent;
            nodeParent?.Children.Remove(node);

            if (node.Parent != null && node.Children.Count > 0)
            {
                foreach (var child in nodeChildren)
                {
                    child.Parent = nodeParent;
                }

                nodeParent.Children.AddRange(nodeChildren);
            }
            this.elements.Remove(element);
        }


        public IEnumerable<T> GetChildren(T element)
        {
            this.ContainsItemOrThrowException(element);
            return elements[element].Children.Select(x => x.Value);
        }

        public T GetParent(T element)
        {
            this.ContainsItemOrThrowException(element);
            var node = elements[element];
            return node.Parent != null ? node.Parent.Value : default;
        }

        public bool Contains(T element)
        {
            return this.elements.ContainsKey(element);
        }

        public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        {
            foreach (var el in elements)
            {
                if (other.Contains(el.Value.Value))
                {
                    yield return el.Value.Value;
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            Queue<Node<T>> queue = new Queue<Node<T>>();
            queue.Enqueue(this.root);

            while(queue.Count > 0)
            {
                var node = queue.Dequeue();
                yield return node.Value;
                foreach (var child in node.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void ContainsItemOrThrowException(T element)
        {
            if (!this.Contains(element))
            {
                throw new ArgumentException();
            }
        }

        private Node<T> CreateNode(T element)
        {
            var node = new Node<T>(element);
            elements[element] = node;
            return node;
        }
    }
}