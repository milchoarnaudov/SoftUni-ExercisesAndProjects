﻿namespace _02.Two_Three
{
    using System;

    public class TreeNode<T>
        where T : IComparable<T>
    {
        public TreeNode(T key)
        {
            this.LeftKey = key;
        }

        public T LeftKey { get; set; }
        public T RightKey { get; set; }

        public TreeNode<T> LeftChild { get; set; }
        public TreeNode<T> MiddleChild { get; set; }
        public TreeNode<T> RightChild { get; set; }

        public bool IsThreeNode()
        {
            return RightKey != null;
        }

        public bool IsTwoNode()
        {
            return RightKey == null;
        }

        public bool IsLeaf()
        {
            return this.LeftChild == null && this.MiddleChild == null && this.RightChild == null;
        }
    }
}
