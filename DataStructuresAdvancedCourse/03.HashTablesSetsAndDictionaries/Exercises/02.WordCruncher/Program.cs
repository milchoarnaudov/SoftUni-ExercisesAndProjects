namespace _02.WordCruncher
{
    using System;

    public class Program
    {
        public static void Main()
        {
            var input = Console.ReadLine().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var expected = Console.ReadLine();

            Solution solution = new Solution(input, expected);

            foreach (var path in solution.GetPaths())
            {
                Console.WriteLine(path);
            }
        }
    }
}
