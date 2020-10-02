namespace Problem03.Queue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Queue<T> : IAbstractQueue<T>
    {
        private Node<T> _head;

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            Node<T> currElement = this._head;

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

        public T Dequeue()
        {
            EnsureNotEmpty();

            Node<T> currElement = this._head;

            this._head = this._head.Next;

            this.Count--;

            return currElement.Value;
        }



        public void Enqueue(T item)
        {
            if (this._head == null)
            {
                this._head = new Node<T>(item);
            }
            else
            {
                Node<T> currElement = this._head;

                while (currElement.Next != null)
                {
                    currElement = currElement.Next;
                }

                currElement.Next = new Node<T>(item);
            }

            this.Count++;
        }

        public T Peek()
        {
            this.EnsureNotEmpty();

            return this._head.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> currElement = this._head;

            while (currElement != null)
            {
                yield return currElement.Value;
                currElement = currElement.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void EnsureNotEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("The Queue is empty.");
            }
        }
    }
}