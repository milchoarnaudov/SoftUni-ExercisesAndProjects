namespace Problem01.FasterQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class FastQueue<T> : IAbstractQueue<T>
    {
        private Node<T> _head;
        private Node<T> _tail;

        public FastQueue()
        {
            this._head = this._tail = null;
            this.Count = 0;
        }

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            var current = this._head;

            while (current != null)
            {
                if (current.Item.Equals(item))
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        public T Dequeue()
        {
            EnsureNotEmpty();

            Node<T> lastItem = this._head;

            if (this.Count == 1)
            {
                this._head = this._tail = null;
            }
            else
            {
                this._head = this._head.Next;
            }

            this.Count--;
            return lastItem.Item;
        }

        public void Enqueue(T item)
        {
            Node<T> newItem = new Node<T>() { Item = item };

            if (this._head == null)
            {
                this._head = this._tail = newItem;
            }
            else
            {
                this._tail.Next = newItem;
                this._tail = newItem;
            }

            this.Count++;
        }

        public T Peek()
        {
            EnsureNotEmpty();

            return this._head.Item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this._head;
            while (current != null)
            {
                yield return current.Item;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void EnsureNotEmpty()
        {
            if (this.Count == 0)
                throw new InvalidOperationException();
        }
    }
}
