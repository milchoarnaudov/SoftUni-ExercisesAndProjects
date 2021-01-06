namespace Exercises
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Salaries
    {
        private List<int>[] graph;

        public void FindSum()
        {
            int numberOfEmployees = int.Parse(Console.ReadLine());
            this.graph = new List<int>[numberOfEmployees];

            ReadGraph(numberOfEmployees);

            var totalSalary = 0;
            for (int i = 0; i < graph.Length; i++)
            {
                var salary = this.GetSalary(i);
                totalSalary += salary;
            }

            Console.WriteLine(totalSalary);
        }

        private int GetSalary(int node)
        {
            var children = this.graph[node];

            if (children.Count == 0)
            {
                return 1;
            }

            var salary = 0;
            foreach (var child in children)
            {
                salary += this.GetSalary(child);
            }

            return salary;
        }

        private void ReadGraph(int numberOfEmployees)
        {
            for (int i = 0; i < numberOfEmployees; i++)
            {
                var input = Console.ReadLine();
                var edges = new List<int>();

                for (int j = 0; j < input.Length; j++)
                {
                    if (input[j] == 'Y')
                    {
                        edges.Add(j);
                    }
                }

                this.graph[i] = edges;
            }
        }
    }
}
