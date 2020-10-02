namespace Problem02.Stack
{
    using System;
    public class Node<T>
    {
        public Node(T value, Node<T> nextElement)
        {
            Value = value;
            Next = nextElement;
        }

        public T Value { get; set; }
        public Node<T> Next { get; set; }
    }
}
