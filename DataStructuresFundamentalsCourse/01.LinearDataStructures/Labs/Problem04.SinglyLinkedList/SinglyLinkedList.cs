namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> _head;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            Node<T> newElement = new Node<T>(item, this._head);
            this._head = newElement;
            this.Count++;
        }

        public void AddLast(T item)
        {
            Node<T> newElement = new Node<T>(item);
            Node<T> currElement = this._head;

            if (currElement == null)
            {
                this._head = newElement;
            }
            else
            {
                while (currElement.Next != null)
                {
                    currElement = currElement.Next;
                }

                currElement.Next = newElement;
            }

            this.Count++;
        }

        public T GetFirst()
        {
            this.EnsureNotEmpty();

            return this._head.Value;
        }

        public T GetLast()
        {
            this.EnsureNotEmpty();

            Node<T> currElement = this._head;

            while (currElement.Next != null)
            {
                currElement = currElement.Next;
            }

            return currElement.Value;
        }

        public T RemoveFirst()
        {
            this.EnsureNotEmpty();
            Node<T> firstElement = this._head;
            this._head = this._head.Next;
            this.Count--;

            return firstElement.Value;
        }

        public T RemoveLast()
        {
            EnsureNotEmpty();
            Node<T> removedElement;

            if (this.Count == 1)
            {
                removedElement = this._head;
                this._head = null;
            }
            else
            {
                Node<T> currElement = this._head;

                while (currElement.Next != null)
                {
                    if (currElement.Next.Next == null)
                    {
                        break;
                    }

                    currElement = currElement.Next;
                }

                removedElement = currElement.Next;
                currElement.Next = null;
            }

            this.Count--;
            return removedElement.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> currentElement = this._head;

            while (currentElement != null)
            {
                yield return currentElement.Value;
                currentElement = currentElement.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void EnsureNotEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("The Linked List is emtpy.");
            }
        }
    }
}