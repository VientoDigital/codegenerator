using System.ComponentModel;
using CodeGenerator.Data;

namespace CodeGenerator.Data.Structure
{
    public class Table
    {
        private readonly ColumnStrategy strategy;
        private ColumnCollection columns;
        private KeyCollection keys;
        private bool reload;

        public Table()
        {
            switch (Server.ProviderType)
            {
                case DataProviderType.SqlClient: strategy = new SqlColumnStrategy(); break;
                case DataProviderType.MySql: strategy = new MySqlColumnStrategy(); break;
                case DataProviderType.PostgresSql: strategy = new PostgresColumnStrategy(); break;
                case DataProviderType.Oracle: strategy = new OracleColumnStrategy(); break;
            }
        }

        [Browsable(false), DefaultValue(false)]
        public ColumnCollection Columns
        {
            get
            {
                if (reload || columns == null)
                {
                    if (columns != null)
                    {
                        columns.Clear();
                    }
                    columns = strategy.GetColumns(this);
                }
                return columns;
            }
        }

        [Browsable(false), DefaultValue(false)]
        public KeyCollection Keys
        {
            get
            {
                if (reload || keys == null)
                {
                    if (keys != null)
                    {
                        keys.Clear();
                    }
                    keys = strategy.GetKeys(this);
                }
                return keys;
            }
        }

        [Category("Table"), ReadOnly(true)]
        public string Name { get; set; }

        [Browsable(false), DefaultValue(false)]
        public Database ParentDatabase { get; set; }

        [Category("Table"), ReadOnly(true)]
        public string Schema { get; set; }

        public void Reload() => reload = true;
    }
}