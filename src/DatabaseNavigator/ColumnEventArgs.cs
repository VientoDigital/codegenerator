using System;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.DatabaseNavigator
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