namespace _02.VaniPlanning
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class Agency : IAgency
    {
        private readonly Dictionary<string, Invoice> invoices;
        private readonly OrderedDictionary<DateTime, OrderedBag<Invoice>> invoicesByCreateDate;
        private readonly OrderedDictionary<DateTime, OrderedBag<Invoice>> invoicesByDueDate;
        private readonly Dictionary<string, OrderedBag<Invoice>> invoicesByCompany;
        private readonly Dictionary<Department, OrderedBag<Invoice>> invoicesByDepartment;

        public Agency()
        {
            this.invoices = new Dictionary<string, Invoice>();
            this.invoicesByCreateDate = new OrderedDictionary<DateTime, OrderedBag<Invoice>>();
            this.invoicesByDueDate = new OrderedDictionary<DateTime, OrderedBag<Invoice>>();
            this.invoicesByCompany = new Dictionary<string, OrderedBag<Invoice>>();
            this.invoicesByDepartment = new Dictionary<Department, OrderedBag<Invoice>>();
        }

        public void Create(Invoice invoice)
        {
            if (this.invoices.ContainsKey(invoice.SerialNumber))
            {
                throw new ArgumentException();
            }

            this.invoices.Add(invoice.SerialNumber, invoice);
            this.AddToByDate(invoice);
            this.AddToByCompany(invoice);
            this.AddToByDepartment(invoice);
        }

        
        public void ThrowInvoice(string number)
        {
            if (!this.invoices.ContainsKey(number))
            {
                throw new ArgumentException();
            }

            var invoiceToRemove = this.invoices[number];

            this.invoices.Remove(number);
            this.invoicesByCreateDate[invoiceToRemove.IssueDate].Remove(invoiceToRemove);
            this.invoicesByDueDate[invoiceToRemove.DueDate].Remove(invoiceToRemove);
            this.invoicesByCompany[invoiceToRemove.CompanyName].Remove(invoiceToRemove);
            this.invoicesByDepartment[invoiceToRemove.Department].Remove(invoiceToRemove);
        }

        public void ThrowPayed()
        {
            var toRemove = this.invoices.Values.Where(x => x.Subtotal == 0).ToList();

            foreach (var item in toRemove)
            {
                this.ThrowInvoice(item.SerialNumber);
            }
        }

        public int Count()
        {
            return this.invoices.Count;
        }

        public bool Contains(string number)
        {
            return this.invoices.ContainsKey(number);
        }

        public void PayInvoice(DateTime due)
        {
            if (!this.invoicesByDueDate.ContainsKey(due))
            {
                throw new ArgumentException();
            }

            foreach (var invoice in this.invoicesByDueDate[due])
            {
                invoice.Subtotal = 0;
            }
        }

        public IEnumerable<Invoice> GetAllInvoiceInPeriod(DateTime start, DateTime end)
        {
            var invoicesInRange = this.invoicesByCreateDate.Range(start, true, end, true);
            var result = new OrderedBag<Invoice>(new DateComparer());

            foreach (var invoices in invoicesInRange)
            {
                foreach (var invoice in invoices.Value)
                {
                    result.Add(invoice);
                }
            }

            return result;
        }

        public IEnumerable<Invoice> SearchBySerialNumber(string serialNumber)
        {
            var result = this.invoices.Where(x => x.Key.Contains(serialNumber)).Select(x => x.Value);

            if (result.Count() == 0)
            {
                throw new ArgumentException();
            }

            return new OrderedBag<Invoice>(result, (x, y) => y.SerialNumber.CompareTo(x.SerialNumber));
        }

        public IEnumerable<Invoice> ThrowInvoiceInPeriod(DateTime start, DateTime end)
        {
            var invoicesInRange = this.invoicesByDueDate.Range(start, false, end, false);
            var result = new OrderedBag<Invoice>(new DateComparer());

            foreach (var invoices in invoicesInRange)
            {
                foreach (var invoice in invoices.Value)
                {
                    result.Add(invoice);
                }
            }

            if (result.Count == 0)
            {
                throw new ArgumentException();
            }

            foreach (var invoice in result)
            {
                this.ThrowInvoice(invoice.SerialNumber);
            }

            return result;
        }

        public IEnumerable<Invoice> GetAllFromDepartment(Department department)
        {
            if (!this.invoicesByDepartment.ContainsKey(department))
            {
                return Enumerable.Empty<Invoice>();
            }

            return this.invoicesByDepartment[department];
        }

        public IEnumerable<Invoice> GetAllByCompany(string company)
        {
            if (!this.invoicesByCompany.ContainsKey(company))
            {
                return Enumerable.Empty<Invoice>();
            }

            return this.invoicesByCompany[company];
        }

        public void ExtendDeadline(DateTime dueDate, int days)
        {
            if (!this.invoicesByDueDate.ContainsKey(dueDate))
            {
                throw new ArgumentException();
            }

            foreach (var invoice in this.invoicesByDueDate[dueDate])
            {
                invoice.DueDate = invoice.DueDate.AddDays(days);
            }
        }
        private void AddToByDate(Invoice invoice)
        {
            if (!this.invoicesByDueDate.ContainsKey(invoice.DueDate))
            {
                this.invoicesByDueDate[invoice.DueDate] = new OrderedBag<Invoice>(new DateComparer());
            }

            this.invoicesByDueDate[invoice.DueDate].Add(invoice);

            if (!this.invoicesByCreateDate.ContainsKey(invoice.IssueDate))
            {
                this.invoicesByCreateDate[invoice.IssueDate] = new OrderedBag<Invoice>(new DateComparer());
            }

            this.invoicesByCreateDate[invoice.IssueDate].Add(invoice);
        }

        private void AddToByDepartment(Invoice invoice)
        {
            if (!this.invoicesByDepartment.ContainsKey(invoice.Department))
            {
                this.invoicesByDepartment[invoice.Department] = new OrderedBag<Invoice>(new DepartmentComparer());
            }

            this.invoicesByDepartment[invoice.Department].Add(invoice);
        }

        private void AddToByCompany(Invoice invoice)
        {
            if (!this.invoicesByCompany.ContainsKey(invoice.CompanyName))
            {
                this.invoicesByCompany[invoice.CompanyName] = new OrderedBag<Invoice>((x, y) => y.SerialNumber.CompareTo(x.SerialNumber));
            }

            this.invoicesByCompany[invoice.CompanyName].Add(invoice);
        }
    }
}
