namespace Problem02.Stack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Stack<T> : IAbstractStack<T>
    {
        private Node<T> _top;
        public int Count { get; private set; }

        public bool Contains(T item)
        {
            Node<T> currElement = this._top;

            while (currElement != null)
            {
                if (currElement.Value.Equals(item))
                {
                    return true;
                }

                currElement = currElement.Next;
            }

            return false;
        }

        public T Peek()
        {
            this.EnsureNotEmpty();

            return this._top.Value;
        }

        public T Pop()
        {
            this.EnsureNotEmpty();

            Node<T> topElement = this._top;
            this._top = this._top.Next;
            this.Count--;

            return topElement.Value;
        }

        public void Push(T item)
        {
            Node<T> newElement = new Node<T>(item, this._top);
            this._top = newElement;
            this.Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> currentNode = this._top;

            while (currentNode != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void EnsureNotEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("The Stack is empty.");
            }
        }
    }
}