using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ObsługaFaktur.Models;

namespace ObsługaFaktur.Exceptions
{
    public class CustomerConflictException : Exception
    {
        public Customer ExistingCustomer { get; }
        public Customer IncomingCustomer { get; }


        public CustomerConflictException(Customer existingCustomer, Customer incomingCustomer)
        {
            ExistingCustomer = existingCustomer;
            IncomingCustomer = incomingCustomer;
        }

        public CustomerConflictException(string? message, Customer existingCustomer, Customer incomingCustomer) : base(message)
        {
            ExistingCustomer = existingCustomer;
            IncomingCustomer = incomingCustomer;
        }

        public CustomerConflictException(string? message, Exception? innerException, Customer existingCustomer, Customer incomingCustomer) : base(message, innerException)
        {
            ExistingCustomer = existingCustomer;
            IncomingCustomer = incomingCustomer;
        }
    }
}
