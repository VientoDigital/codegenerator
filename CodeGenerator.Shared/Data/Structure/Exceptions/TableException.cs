using System;

namespace CodeGenerator.Data.Structure.Exceptions
{
    public class TableException : ApplicationException
    {
        public TableException(string message) : base(message)
        {
        }

        public TableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public TableException() : base()
        {
        }
    }
}