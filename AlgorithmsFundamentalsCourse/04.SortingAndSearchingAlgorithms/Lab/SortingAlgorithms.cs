namespace Lab
{
    using System.Linq;

    public class SortingAlgorithms
    {
        public void SelectionSort(int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                int minNumberIndex = i;

                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if (numbers[j] < numbers[minNumberIndex])
                    {
                        minNumberIndex = j;
                    }
                }

                this.Swap(numbers, minNumberIndex, i);
            }
        }

        public void BubbleSort(int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = 0; j < numbers.Length - 1; j++)
                {
                    if (numbers[j] > numbers[j + 1])
                    {
                        this.Swap(numbers, j, j + 1);
                    }
                }
            }
        }

        public void InsertionSort(int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                var j = i;

                while (j > 0 && numbers[j - 1] > numbers[j])
                {
                    this.Swap(numbers, j - 1, j);
                    j--;
                }
            }
        }

        public void QuickSort(int[] numbers)
        {
            this.QuickSort(numbers, 0, numbers.Length - 1);
        }

        private void QuickSort(int[] numbers, int start, int end)
        {
            if (start >= end)
            {
                return;
            }

            var pivot = start;
            var left = start + 1;
            var right = end;

            while (left <= right)
            {
                if (numbers[left] > numbers[pivot]
                    && numbers[right] < numbers[pivot])
                {
                    Swap(numbers, left, right);
                }

                if (numbers[left] <= numbers[pivot])
                {
                    left += 1;
                }

                if (numbers[right] >= numbers[pivot])
                {
                    right -= 1;
                }
            }

            Swap(numbers, pivot, right);

            var subLeftArrayIsSmaller = right - 1 - start < end - (right + 1);

            if (subLeftArrayIsSmaller)
            {
                QuickSort(numbers, start, right - 1);
                QuickSort(numbers, right + 1, end);
            }
            else
            {
                QuickSort(numbers, right + 1, end);
                QuickSort(numbers, start, right - 1);
            }
        }

        public void MergeSort(ref int[] numbers)
        {
            numbers = this.MergeSortSplit(numbers);
        }

        public int[] MergeSortSplit(int[] numbers)
        {
            if (numbers.Length <= 1)
            {
                return numbers;
            }

            var left = numbers.Take(numbers.Length / 2).ToArray();
            var right = numbers.Skip(numbers.Length / 2).ToArray();

            return Merge(MergeSortSplit(left), MergeSortSplit(right));
        }

        private int[] Merge(int[] left, int[] right)
        {
            var merged = new int[left.Length + right.Length];

            var mergedIndex = 0;
            var leftIndex = 0;
            var rightIndex = 0;

            while (leftIndex < left.Length && rightIndex < right.Length)
            {
                if (left[leftIndex] < right[rightIndex])
                {
                    merged[mergedIndex] = left[leftIndex];
                    leftIndex += 1;
                }
                else
                {
                    merged[mergedIndex] = right[rightIndex];
                    rightIndex += 1;
                }

                mergedIndex += 1;
            }

            while (leftIndex < left.Length)
            {
                merged[mergedIndex] = left[leftIndex];
                leftIndex += 1;
                mergedIndex += 1;
            }

            while (rightIndex < right.Length)
            {
                merged[mergedIndex] = right[rightIndex];
                rightIndex += 1;
                mergedIndex += 1;
            }

            return merged;
        }

        private void Swap(int[] arr, int firstIndex, int secondIndex)
        {
            int temp = arr[firstIndex];
            arr[firstIndex] = arr[secondIndex];
            arr[secondIndex] = temp;
        }
    }
}
