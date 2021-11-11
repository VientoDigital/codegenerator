using System;
using CodeGenerator.Data.Structure;

namespace CodeGenerator.DatabaseNavigator
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