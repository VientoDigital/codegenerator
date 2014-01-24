using System;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.DatabaseNavigator
{
	public class DatabaseEventArgs : EventArgs
	{

		private Database _database;

		public Database Database
		{
			get { return _database; }
		}

		public DatabaseEventArgs(Database database)
		{
			_database = database;
		}
	}
}
