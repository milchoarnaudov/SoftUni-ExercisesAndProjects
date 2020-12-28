namespace Lab
{
    using System;
    using System.Linq;

    public class ReverseArray
    {
        public void Start()
        {
            var input = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

            this.ReverseArr(input, 0);

            Console.WriteLine(String.Join(" ", input));
        }

        private void ReverseArr(int[] arr, int left)
        {
            if (left >= arr.Length / 2)
            {
                return;
            }

            var right = arr.Length - 1 - left;

            this.Swap(arr, left, right);
            this.ReverseArr(arr, left + 1);
        }

        private void Swap(int[] arr, int left, int right)
        {
            int temp = arr[left];
            arr[left] = arr[right];
            arr[right] = temp;
        }
    }
}
