using ObsługaFaktur.Models;
using System;

namespace ObsługaFaktur.Exceptions
{
    public class ProductConflictException : Exception
    {
        public ProductConflictException(Product existingInvoice, Product incomingInvoice)
        {
            ExistingInvoice = existingInvoice;
            IncomingInvoice = incomingInvoice;
        }

        public ProductConflictException(string? message, Product existingInvoice, Product incomingInvoice) : base(message)
        {
            ExistingInvoice = existingInvoice;
            IncomingInvoice = incomingInvoice;
        }

        public ProductConflictException(string? message, Exception? innerException, Product existingInvoice, Product incomingInvoice) : base(message, innerException)
        {
            ExistingInvoice = existingInvoice;
            IncomingInvoice = incomingInvoice;
        }

        public Product ExistingInvoice { get; }
        public Product IncomingInvoice { get; }
    }
}
