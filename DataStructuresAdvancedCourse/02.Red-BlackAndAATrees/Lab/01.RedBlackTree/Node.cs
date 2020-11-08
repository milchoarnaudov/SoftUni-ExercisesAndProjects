namespace _01.RedBlackTree
{
    using System;

    public partial class RedBlackTree<T> where T : IComparable
    {
        private class Node
        {
            const bool Red = true;
            const bool Black = false;

            public Node(T value)
            {
                this.Value = value;
                this.Color = Red;
            }

            public T Value { get; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public bool Color { get; set; }
            public int Count { get; set; }
        }

    }
}