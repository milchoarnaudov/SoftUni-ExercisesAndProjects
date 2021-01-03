namespace Lab
{
    // The algorithms return the index of the found element
    public class SearchingAlgorithms
    {
        public int LinearSearching(int[] numbers, int number)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == number)
                {
                    return i;
                }
            }

            return -1;
        }

        public int BinarySearch(int[] sortedNumbers, int number)
        {
            var left = 0;
            var right = sortedNumbers.Length - 1;

            while (left <= right)
            {
                var mid = (left + right) / 2;

                if (sortedNumbers[mid] == number)
                {
                    return mid;
                }

                if (sortedNumbers[mid] > number)
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }

            return -1;
        }
    }
}
