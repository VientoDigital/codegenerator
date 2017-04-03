using System.ComponentModel;
using iCodeGenerator.GenericDataAccess;

namespace iCodeGenerator.DatabaseStructure
{
    public class Table
    {
        private readonly ColumnStrategy _strategy;
        private bool _Reload;
        private ColumnCollection _Columns;
        private KeyCollection _Keys;

        public void Reload()
        {
            _Reload = true;
        }

        public Table()
        {
            switch (Server.ProviderType)
            {
                case DataProviderType.SqlClient:
                    _strategy = new ColumnStrategySQLServer();
                    break;

                case DataProviderType.MySql:
                    _strategy = new ColumnStrategyMySQL();
                    break;

                case DataProviderType.PostgresSql:
                    _strategy = new ColumnStrategyPostgres();
                    break;

                case DataProviderType.Oracle:
                    _strategy = new ColumnStrategyOracle();
                    break;
            }
        }

        [CategoryAttribute("Table"), ReadOnlyAttribute(true)]
        public string Schema { get; set; }

        [CategoryAttribute("Table"), ReadOnlyAttribute(true)]
        public string Name { get; set; }

        [BrowsableAttribute(false), DefaultValueAttribute(false)]
        public Database ParentDatabase { get; set; }

        [BrowsableAttribute(false),
        DefaultValueAttribute(false)]
        public ColumnCollection Columns
        {
            get
            {
                if (_Reload || _Columns == null)
                {
                    if (_Columns != null) _Columns.Clear();
                    _Columns = _strategy.GetColumns(this);
                }
                return _Columns;
            }
        }

        [BrowsableAttribute(false),
        DefaultValueAttribute(false)]
        public KeyCollection Keys
        {
            get
            {
                if (_Reload || _Keys == null)
                {
                    if (_Keys != null) _Keys.Clear();
                    _Keys = _strategy.GetKeys(this);
                }
                return _Keys;
            }
        }
    }
}