using System;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.DatabaseNavigator
{
    public class TableEventArgs : EventArgs
    {
        private Table _table;

        public Table Table
        {
            get { return _table; }
        }

        public TableEventArgs(Table table)
        {
            _table = table;
        }
    }
}