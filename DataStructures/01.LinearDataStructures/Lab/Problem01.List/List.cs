namespace Problem01.List
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class List<T> : IAbstractList<T>
    {
        private const int DEFAULT_CAPACITY = 4;
        private T[] _items;

        public List()
            : this(DEFAULT_CAPACITY)
        {
        }

        public List(int capacity)
        {
            if (capacity <= 0)
            {
                throw new IndexOutOfRangeException("The size of the List cannot be a negative number.");
            }

            this._items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                this.ValidateIndex(index);
                return this._items[index];
            }
            set
            {
                this.ValidateIndex(index);
                this._items[index] = value;
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }

            this.EnsureArrayIsResized();
            this._items[Count++] = item;
        }

        public bool Contains(T item)
        {
            return this.IndexOf(item) > -1;
        }


        public int IndexOf(T item)
        {
            int index = -1;

            for (int i = 0; i < this.Count; i++)
            {
                if (this._items[i].Equals(item))
                {
                    index = i;
                }
            }

            return index;
        }

        public void Insert(int index, T item)
        {
            this.ValidateIndex(index);
            this.EnsureArrayIsResized();

            for (int i = this.Count; i > index; i--)
            {
                this._items[i] = this._items[i - 1];
            }

            this._items[index] = item;
            this.Count++;
        }

        public bool Remove(T item)
        {
            int indexOfItem = this.IndexOf(item);

            if (indexOfItem > -1)
            {
                this.RemoveAt(indexOfItem);

                return true;
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            this.ValidateIndex(index);

            for (int i = index; i < this.Count - 1; i++)
            {
                this._items[i] = this._items[i + 1];
            }

            this._items[Count - 1] = default;

            this.Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
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