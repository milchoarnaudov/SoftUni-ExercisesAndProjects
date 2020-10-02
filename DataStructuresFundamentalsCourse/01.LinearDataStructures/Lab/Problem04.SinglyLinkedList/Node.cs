namespace Problem04.SinglyLinkedList
{
    public class Node<T>
    {
        public Node(T value, Node<T> nextElement = null)
        {
            Value = value;
            Next = nextElement;
        }

        public T Value { get; set; }
        public Node<T> Next { get; set; }
    }
}