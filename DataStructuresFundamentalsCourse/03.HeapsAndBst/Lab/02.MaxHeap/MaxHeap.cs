namespace _02.MaxHeap
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using System.Runtime.InteropServices;

    public class MaxHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> _elements;

        public MaxHeap()
        {
            this._elements = new List<T>();
        }

        public int Size => this._elements.Count;

        public void Add(T element)
        {
            this._elements.Add(element);

            this.HeapifyUp(this._elements.Count - 1);
        }

        public T Peek()
        {
            if (this._elements.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return this._elements[0];
        }

        private void HeapifyUp(int currentIndex)
        {
            if (currentIndex < 1)
            {
                return;
            }

            int parrentIndex = this.FindParentIndex(currentIndex);
            T currentElement = this._elements[currentIndex];
            T parentElement = this._elements[parrentIndex];

            if (currentElement.CompareTo(parentElement) > 0)
            {
                this._elements[parrentIndex] = currentElement;
                this._elements[currentIndex] = parentElement;
                this.HeapifyUp(parrentIndex);
            }
        }

        private int FindParentIndex(int index)
        {
            if (index < 1 && index >= this.Size)
            {
                throw new ArgumentOutOfRangeException();
            }

            return (index - 1) / 2;
        }
    }
}
