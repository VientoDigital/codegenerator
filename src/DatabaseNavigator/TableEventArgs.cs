using System;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.DatabaseNavigator
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