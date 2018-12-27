using System;

namespace Paginator.Core.Exceptions
{
    public class InvalidCursorException : Exception
    {
        public InvalidCursorException() : base()
        {
        }

        public InvalidCursorException(string message) : base(message)
        {
        }

        public InvalidCursorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
