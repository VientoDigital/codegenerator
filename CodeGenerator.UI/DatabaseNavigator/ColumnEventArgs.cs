using System;
using CodeGenerator.Data.Structure;

namespace CodeGenerator.DatabaseNavigator
{
    public class ColumnEventArgs : EventArgs
    {
        public Column Column { get; }

        public ColumnEventArgs(Column column)
        {
            Column = column;
        }
    }
}