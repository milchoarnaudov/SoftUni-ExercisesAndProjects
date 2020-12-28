namespace Lab
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class NestedLoopsWithRecursion
    {
        public void Start()
        {
            var input = int.Parse(Console.ReadLine());

            this.SimulateNestedLoops(new int[input], 0);
        }

        public void SimulateNestedLoops(int[] arr, int index)
        {
            if (index >= arr.Length)
            {
                Console.WriteLine(string.Join(" ", arr));
                return;
            }

            for (int i = 0; i < arr.Length; i++)
            {
                arr[index] = i + 1;
                this.SimulateNestedLoops(arr, index + 1);
            }
        }
    }
}
