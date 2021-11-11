using System;
using CodeGenerator.DatabaseStructure;

namespace CodeGenerator.DatabaseNavigator
{
    public class TableEventArgs : EventArgs
    {
        public Table Table { get; }

        public TableEventArgs(Table table)
        {
            Table = table;
        }
    }
}