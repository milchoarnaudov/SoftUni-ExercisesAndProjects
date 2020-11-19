using System.Collections.Generic;

namespace _01.Microsystem
{
    public class ComputerPriceDescComparer : IComparer<Computer>
    {
        public int Compare(Computer x, Computer y)
        {
            return y.Price.CompareTo(x.Price);
        }
    }
}
