using System;

namespace Example.Domain.Exceptions
{
    public class PaymentProcessingException : Exception
    {
        public PaymentProcessingException()
        {
        }

        public PaymentProcessingException(string message) : base(message)
        {
        }

        public PaymentProcessingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}