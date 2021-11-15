using System;

namespace CodeGenerator.Data.Structure.Exceptions
{
    public class KeyException : ApplicationException
    {
        public KeyException(string message) : base(message)
        {
        }

        public KeyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public KeyException() : base()
        {
        }
    }
}