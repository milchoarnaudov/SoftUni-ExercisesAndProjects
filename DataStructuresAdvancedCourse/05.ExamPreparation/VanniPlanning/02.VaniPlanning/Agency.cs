namespace _02.VaniPlanning
{
    using System;
    using System.Collections.Generic;

    public class Agency : IAgency
    {
        public void Create(Invoice invoice)
        {
            throw new NotImplementedException();
        }

        public void ThrowInvoice(string number)
        {
            throw new NotImplementedException();
        }

        public void ThrowPayed()
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public bool Contains(string number)
        {
            throw new NotImplementedException();
        }

        public void PayInvoice(DateTime due)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Invoice> GetAllInvoiceInPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Invoice> SearchBySerialNumber(string serialNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Invoice> ThrowInvoiceInPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Invoice> GetAllFromDepartment(Department department)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Invoice> GetAllByCompany(string company)
        {
            throw new NotImplementedException();
        }

        public void ExtendDeadline(DateTime dueDate, int days)
        {
            throw new NotImplementedException();
        }
    }
}
