using System;

namespace Example.Domain.Exceptions
{
    public class OutOfStockException : Exception
    {
        public OutOfStockException()
        {
        }

        public OutOfStockException(string productName) : base($"The product '{productName}' is out of stock.")
        {
        }

        public OutOfStockException(string productName, Exception innerException) : base($"The product '{productName}' is out of stock.", innerException)
        {
        }
    }
}