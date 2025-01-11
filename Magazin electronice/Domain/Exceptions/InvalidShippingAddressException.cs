using System;

namespace Example.Domain.Exceptions
{
    public class InvalidShippingAddressException : Exception
    {
        public InvalidShippingAddressException()
        {
        }

        public InvalidShippingAddressException(string address) : base($"The shipping address '{address}' is invalid.")
        {
        }

        public InvalidShippingAddressException(string address, Exception innerException) : base($"The shipping address '{address}' is invalid.", innerException)
        {
        }
    }
}