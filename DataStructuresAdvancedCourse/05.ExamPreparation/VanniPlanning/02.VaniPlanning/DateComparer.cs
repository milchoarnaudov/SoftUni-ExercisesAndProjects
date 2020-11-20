namespace _02.VaniPlanning
{
    using System.Collections.Generic;

    public class DateComparer : IComparer<Invoice>
    {
        public int Compare(Invoice x, Invoice y)
        {
            var result = x.IssueDate.CompareTo(y.IssueDate);

            if (result == 0)
            {
                result = x.DueDate.CompareTo(y.DueDate);
            }

            return result;
        }
    }
}
