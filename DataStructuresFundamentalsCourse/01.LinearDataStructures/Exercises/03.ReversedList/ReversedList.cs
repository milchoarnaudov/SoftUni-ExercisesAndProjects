namespace Problem03.ReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IAbstractList<T>
    {
        private const int DefaultCapacity = 4;

        private T[] _items;

        public ReversedList()
            : this(DefaultCapacity) { }

        public ReversedList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            this._items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                ValidateIndex(index);
                return this._items[Count - 1 - index];
            }
            set
            {
                ValidateIndex(index);
                this._items[index] = value;
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            this.EnsureArrayIsResized();
            this._items[Count++] = item;
        }

        public bool Contains(T item)
        {
            return this.IndexOf(item) != -1;
        }

        public int IndexOf(T item)
        {
            for (int i = 1; i <= this.Count; i++)
            {
                if (this._items[this.Count - i].Equals(item))
                {
                    return i - 1;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            this.EnsureArrayIsResized();
            this.ValidateIndex(index);
            int indexOfItem = this.Count - index;

            for (int i = this.Count; i > indexOfItem; i--)
            {
                this._items[i] = this._items[i - 1];
            }

            this._items[indexOfItem] = item;
            this.Count++;
        }

        public bool Remove(T item)
        {
            int indexOfItem = this.IndexOf(item);

            if (indexOfItem == -1)
            {
                return false;
            }

            this.RemoveAt(indexOfItem);
            return true;
        }

        public void RemoveAt(int index)
        {
            this.ValidateIndex(index);
            int indexOfItem = this.Count - 1 - index;

            for (int i = indexOfItem; i < this.Count - 1; i++)
            {
                this._items[i] = this._items[i + 1];
            }

            this._items[this.Count - 1] = default;
            this.Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                yield return this._items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void EnsureArrayIsResized()
        {
            if (this.Count == this._items.Length)
            {
                this.ResizeArray();
            }
        }

        private void ResizeArray()
        {
            var temp = new T[this._items.Length * 2];

            for (int i = 0; i < this._items.Length; i++)
            {
                temp[i] = this._items[i];
            }

            this._items = temp;
        }

        private void ValidateIndex(int index)
        {
            if (index >= this.Count || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}