using ObsługaFaktur.Models;
using System;

namespace ObsługaFaktur.Exceptions
{
    internal class InvoiceConflictException : Exception
    {
        public InvoiceConflictException(Invoice existingInvoice, Invoice incomingInvoice)
        {
            ExistingInvoice = existingInvoice;
            IncomingInvoice = incomingInvoice;
        }

        public InvoiceConflictException(string? message, Invoice existingInvoice, Invoice incomingInvoice) : base(message)
        {
            ExistingInvoice = existingInvoice;
            IncomingInvoice = incomingInvoice;
        }

        public InvoiceConflictException(string? message, Exception? innerException, Invoice existingInvoice, Invoice incomingInvoice) : base(message, innerException)
        {
            ExistingInvoice = existingInvoice;
            IncomingInvoice = incomingInvoice;
        }

        public Invoice ExistingInvoice { get; }
        public Invoice IncomingInvoice { get; }
    }
}
