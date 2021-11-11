using System;
using CodeGenerator.Data.Structure;

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