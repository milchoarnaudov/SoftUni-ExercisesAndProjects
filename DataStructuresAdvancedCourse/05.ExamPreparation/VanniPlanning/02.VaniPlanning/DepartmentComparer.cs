namespace _02.VaniPlanning
{
    using System.Collections.Generic;

    public class DepartmentComparer : IComparer<Invoice>
    {
        public int Compare(Invoice x, Invoice y)
        {
            var result = y.Subtotal.CompareTo(x.Subtotal);

            if (result == 0)
            {
                result = x.IssueDate.CompareTo(y.IssueDate);
            }

            return result;
        }
    }
}
