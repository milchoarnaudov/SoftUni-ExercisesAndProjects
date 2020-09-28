namespace _03.PriorityQueue
{
    using System;
    using System.Collections.Generic;

    public class PriorityQueue<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> _elements;

        public PriorityQueue()
        {
            this._elements = new List<T>();
        }
        public int Size => this._elements.Count;

        public T Dequeue()
        {
            T firstElement = this.Peek();
            T lastElement = this._elements[this.Size - 1];

            this._elements[this.Size - 1] = firstElement;
            this._elements[0] = lastElement;

            this._elements.RemoveAt(this.Size - 1);
            this.HeapifyDown();

            return firstElement;
        }

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

        private void HeapifyDown()
        {
            int currentIndex = 0;
            int leftChildIndex = this.GetLeftChildIndex(currentIndex);

            while (leftChildIndex < this.Size && this.IsLess(currentIndex, leftChildIndex))
            {
                int toSwapWith = leftChildIndex;
                int rightChildIndex = this.GetRightChildIndex(currentIndex);

                if (rightChildIndex < this.Size && this.IsLess(toSwapWith, rightChildIndex))
                {
                    toSwapWith = rightChildIndex;
                }

                T temp = this._elements[toSwapWith];
                this._elements[toSwapWith] = this._elements[currentIndex];
                this._elements[currentIndex] = temp;

                currentIndex = toSwapWith;
                leftChildIndex = this.GetLeftChildIndex(currentIndex);
            }
        }

        private int FindParentIndex(int index)
        {
            this.ValidateIndex(index);

            return (index - 1) / 2;
        }

        private int GetLeftChildIndex(int index)
        {
            this.ValidateIndex(index);

            return (2 * index) + 1;
        }

        private int GetRightChildIndex(int index)
        {
            this.ValidateIndex(index);

            return (2 * index) + 2;
        }

        private void ValidateIndex(int index)
        {
            if (index < 1 && index > this.Size)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        private bool IsLess(int firstNode, int secondNode)
        {
            return this._elements[firstNode]
                .CompareTo(this._elements[secondNode]) < 0;
        }
    }
}
