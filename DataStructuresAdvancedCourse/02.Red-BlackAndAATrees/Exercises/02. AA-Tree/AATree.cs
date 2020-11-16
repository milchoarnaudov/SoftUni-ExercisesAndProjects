namespace _02._AA_Tree
{
    using System;

    public class AATree<T> : IBinarySearchTree<T>
        where T : IComparable<T>
    {
        private Node<T> root;

        public int CountNodes()
        {
            return this.root != null ? this.root.Count : 0;
        }

        public bool IsEmpty()
        {
            return this.root == null;
        }

        public void Clear()
        {
            this.root = null;
        }

        public void Insert(T element)
        {
            this.root = Insert(this.root, element);
        }

        public bool Search(T element)
        {
            return this.Search(this.root, element) != null; 
        }

        public void InOrder(Action<T> action)
        {
            this.InOrderTraversal(this.root, action);
        }

        public void PreOrder(Action<T> action)
        {
            this.PreOrderTraversal(root, action);
        }


        public void PostOrder(Action<T> action)
        {
            this.PostOrderTraversal(root, action);
        }

        private void InOrderTraversal(Node<T> node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            this.InOrderTraversal(node.Left, action);
            action(node.Element);
            this.InOrderTraversal(node.Right, action);
        }

        private Node<T> Insert(Node<T> node, T elementToInsert)
        {
            if (node == null)
            {
                return new Node<T>(elementToInsert);
            }

            var comparator = elementToInsert.CompareTo(node.Element);

            if (comparator > 0)
            {
                node.Right = this.Insert(node.Right, elementToInsert);
            }
            else if (comparator < 0)
            {
                node.Left = this.Insert(node.Left, elementToInsert);
            }

            node = this.Skew(node);
            node = this.Split(node);

            node.Count = 1 + GetCount(node.Left) + GetCount(node.Right);
            return node;
        }


        private void PostOrderTraversal(Node<T> node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            this.PostOrderTraversal(node.Left, action);
            this.PostOrderTraversal(node.Right, action);
            action(node.Element);
        }

        private int GetCount(Node<T> node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.Count;
        }

        private void PreOrderTraversal(Node<T> node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            action(node.Element);
            this.PreOrderTraversal(node.Left, action);
            this.PreOrderTraversal(node.Right, action);
        }

        private Node<T> Split(Node<T> node)
        {
            if (node.Level == node.Right?.Right?.Level)
            {
                var temp = node.Right;
                node.Right = temp.Left;
                temp.Left = node;
                node.Count = 1 + GetCount(node.Left) + GetCount(node.Right);
                temp.Level = this.Level(temp.Right) + 1;

                return temp;
            }
            else
            {
                return node;
            }
        }

        private int Level(Node<T> node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.Level;
        }

        private Node<T> Search(Node<T> node, T element)
        {
            if (node == null)
            {
                return null;
            }

            Node<T> result = null;
            var comparator = element.CompareTo(node.Element);

            if (comparator > 0)
            {
                result = this.Search(node.Right, element);
            }
            else if (comparator < 0)
            {
                result = this.Search(node.Left, element);
            }
            else
            {
                result = node;
            }

            return result;
        }


        private Node<T> Skew(Node<T> node)
        {
            if (node.Level == node.Left?.Level)
            {
                var temp = node.Left;
                node.Left = temp.Right;
                temp.Right = node;
                node.Count = 1 + GetCount(node.Left) + GetCount(node.Right);

                return temp;
            }
            else
            {
                return node;
            }
        }
    }
}