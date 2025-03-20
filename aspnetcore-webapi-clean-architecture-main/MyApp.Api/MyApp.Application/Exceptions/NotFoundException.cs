using System;

namespace MyApp.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        // Constructor to pass a message
        public NotFoundException(string message) : base(message)
        {
        }

        // Optional: you can pass both message and inner exception if needed
        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}