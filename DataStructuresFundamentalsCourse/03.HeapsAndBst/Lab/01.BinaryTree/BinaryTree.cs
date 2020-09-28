namespace _01.BinaryTree
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
    {
        public BinaryTree(T value
            , IAbstractBinaryTree<T> leftChild
            , IAbstractBinaryTree<T> rightChild)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
        }

        public T Value { get; private set; }

        public IAbstractBinaryTree<T> LeftChild { get; private set; }

        public IAbstractBinaryTree<T> RightChild { get; private set; }

        public string AsIndentedPreOrder(int indent)
        {
            StringBuilder stringBuilder = new StringBuilder();

            this.AsIndentedPreOrderDfsTraversal(this, indent, stringBuilder);

            return stringBuilder.ToString();
        }
        

        public List<IAbstractBinaryTree<T>> InOrder()
        {
            List<IAbstractBinaryTree<T>> result = new List<IAbstractBinaryTree<T>>();
            
            if (this.LeftChild != null)
            {
                result.AddRange(this.LeftChild.InOrder());
            }

            result.Add(this);

            if (this.RightChild != null)
            {
                result.AddRange(this.RightChild.InOrder());
            }

            return result;
        }

        public List<IAbstractBinaryTree<T>> PostOrder()
        {
            List<IAbstractBinaryTree<T>> result = new List<IAbstractBinaryTree<T>>();

            if (this.LeftChild != null)
            {
                result.AddRange(this.LeftChild.PostOrder());
            }

            if (this.RightChild != null)
            {
                result.AddRange(this.RightChild.PostOrder());
            }

            result.Add(this);

            return result;
        }

        public List<IAbstractBinaryTree<T>> PreOrder()
        {
            List<IAbstractBinaryTree<T>> result = new List<IAbstractBinaryTree<T>>();

            result.Add(this);

            if (this.LeftChild != null)
            {
                result.AddRange(this.LeftChild.PreOrder());
            }

            if (this.RightChild != null)
            {
                result.AddRange(this.RightChild.PreOrder());
            }

            return result;
        }

        public void ForEachInOrder(Action<T> action)
        {
            if (this.LeftChild != null)
            {
                this.LeftChild.ForEachInOrder(action);
            }

            action.Invoke(this.Value);

            if (this.RightChild != null)
            {
                this.RightChild.ForEachInOrder(action);
            }
        }

        private void AsIndentedPreOrderDfsTraversal(IAbstractBinaryTree<T> subtree,  int indent, StringBuilder result)
        {
            result.AppendLine($"{new String(' ', indent)}{subtree.Value}");

            if (subtree.LeftChild != null)
            {
                this.AsIndentedPreOrderDfsTraversal(subtree.LeftChild, indent + 2, result);
            }
            
            if (subtree.RightChild != null)
            {
                this.AsIndentedPreOrderDfsTraversal(subtree.RightChild, indent + 2, result);
            }
        }
    }
}
