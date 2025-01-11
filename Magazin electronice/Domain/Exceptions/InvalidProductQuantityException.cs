using System;

namespace Example.Domain.Exceptions
{
    public class InvalidProductQuantityException : Exception
    {
        public InvalidProductQuantityException()
        {
        }

        public InvalidProductQuantityException(int requestedQuantity) : base($"Requested quantity {requestedQuantity} is not valid.")
        {
        }

        public InvalidProductQuantityException(int requestedQuantity, Exception innerException) : base($"Requested quantity {requestedQuantity} is not valid.", innerException)
        {
        }
    }
}