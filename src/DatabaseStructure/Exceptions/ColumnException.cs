using System;

namespace iCodeGenerator.DatabaseStructure.Exceptions
{
    public class ColumnException : ApplicationException
    {
        public ColumnException(string message) : base(message)
        {
        }

        public ColumnException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ColumnException() : base()
        {
        }
    }
}