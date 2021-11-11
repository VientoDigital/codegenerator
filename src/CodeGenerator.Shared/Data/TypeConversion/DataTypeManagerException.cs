using System;

namespace CodeGenerator.Data.TypeConversion
{
    public class DataTypeManagerException : ApplicationException
    {
        public DataTypeManagerException() : base()
        {
        }

        public DataTypeManagerException(string message) : base(message)
        {
        }

        public DataTypeManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}