namespace _02.LowestCommonAncestor
{
    using System;
    using System.Collections.Generic;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
        where T : IComparable<T>
    {
        public BinaryTree(
            T value,
            BinaryTree<T> leftChild,
            BinaryTree<T> rightChild)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;

            if (this.RightChild != null)
            {
                this.RightChild.Parent = this;
            }

            if (this.LeftChild != null)
            {
                this.LeftChild.Parent = this;
            }
        }

        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        public BinaryTree<T> Parent { get; set; }

        public T FindLowestCommonAncestor(T first, T second)
        {
            List<BinaryTree<T>> firstNodeList = new List<BinaryTree<T>>();
            List<BinaryTree<T>> secondNodeList = new List<BinaryTree<T>>();

            this.FindNodeDfs(this, first, firstNodeList);
            this.FindNodeDfs(this, second, secondNodeList);

            BinaryTree<T> firstNode = firstNodeList[0];
            BinaryTree<T> secondNode = secondNodeList[0];
            T result = firstNode.Parent.Value;

            while (!result.Equals(firstNode.Value) || !result.Equals(secondNode.Value))
            {
                if (!result.Equals(firstNode.Value))
                {
                    firstNode = firstNode.Parent;
                }

                if (!result.Equals(secondNode.Value))
                {
                    secondNode = secondNode.Parent;
                }
            }

            return firstNode.Value;
        }

        private void FindNodeDfs(BinaryTree<T> current, T value, List<BinaryTree<T>> list)
        {
            if (current == null)
            {
                return;
            }

            if (current.Value.Equals(value))
            {
                list.Add(current);
                return;
            }

            this.FindNodeDfs(current.LeftChild, value, list);
            this.FindNodeDfs(current.RightChild, value, list);
        }
    }
}
