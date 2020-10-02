namespace Problem03.Queue
{
    public class Node<T>
    {
        public Node(T value, Node<T> nextElement = null)
        {
            this.Value = value;
            this.Next = nextElement;
        }

        public T Value { get; set; }
        public Node<T> Next { get; set; }
    }
}