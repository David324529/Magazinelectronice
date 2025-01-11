using System;

namespace Example.Domain.Exceptions
{
    public class InvalidOrderStateException : Exception
    {
        public InvalidOrderStateException()
        {
        }

        public InvalidOrderStateException(string state) : base($"Order state '{state}' is not valid for this operation.")
        {
        }

        public InvalidOrderStateException(string state, Exception innerException) : base($"Order state '{state}' is not valid for this operation.", innerException)
        {
        }
    }
}