using System;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.DatabaseNavigator
{
    public class ColumnEventArgs : EventArgs
    {
        private Column _column;

        public Column Column
        {
            get { return _column; }
        }

        public ColumnEventArgs(Column column)
        {
            _column = column;
        }
    }
}