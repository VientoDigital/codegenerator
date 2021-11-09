using System;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.DatabaseNavigator
{
    public class DatabaseEventArgs : EventArgs
    {
        public Database Database { get; }

        public DatabaseEventArgs(Database database)
        {
            Database = database;
        }
    }
}