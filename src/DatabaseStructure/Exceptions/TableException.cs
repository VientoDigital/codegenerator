using System;

namespace iCodeGenerator.DatabaseStructure.Exceptions
{
	public class TableException : ApplicationException
	{		
		public TableException(string message) : base(message) {}
		public TableException(string message, Exception innerException) : base(message,innerException){}
		public TableException() : base() {}
	}
}
