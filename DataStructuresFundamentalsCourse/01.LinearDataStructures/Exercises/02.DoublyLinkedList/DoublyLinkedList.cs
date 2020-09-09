namespace Problem02.DoublyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DoublyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> _head;
        private Node<T> _tail;

        public DoublyLinkedList()
        {
            this._head = this._head = null;
            this.Count = 0;
        }

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            Node<T> newItem = new Node<T> { Item = item };

            if (this.Count == 0)
            {
                this._head = this._tail = newItem;
            }
            else
            {
                this._head.Previous = newItem;
                newItem.Next = this._head;
                this._head = newItem;
            }

            this.Count++;
        }

        public void AddLast(T item)
        {
            Node<T> newItem = new Node<T> { Item = item };

            if (this.Count == 0)
            {
                this._head = this._tail = newItem;
            }
            else
            {
                this._tail.Next = newItem;
                this._tail.Previous = this._tail;
                this._tail = newItem;
            }

            this.Count++;
        }

        public T GetFirst()
        {
            this.EnsureNotEmpty();

            return this._head.Item;
        }

        public T GetLast()
        {
            this.EnsureNotEmpty();

            return this._tail.Item;
        }

        public T RemoveFirst()
        {
            this.EnsureNotEmpty();
            Node<T> firstItem = this._head;

            if (this.Count == 1)
            {
                this._head = this._tail = null;
            }
            else
            {
                Node<T> temp = this._head.Next;
                temp.Previous = null;
                this._head = temp;
            }

            this.Count--;
            return firstItem.Item;
        }

        public T RemoveLast()
        {
            this.EnsureNotEmpty();
            Node<T> lastItem = this._tail;

            if (this.Count == 1)
            {
                this._head = this._tail = null;
            }
            else
            {
                Node<T> temp = this._tail.Previous;
                temp.Next = null;
                this._tail = temp;
            }

            this.Count--;
            return lastItem.Item;
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