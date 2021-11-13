using System.Collections.Generic;
using System.ComponentModel;

namespace CodeGenerator.Data.Structure
{
    public class Table
    {
        private readonly ColumnStrategy strategy;
        private ICollection<Column> columns;
        private ICollection<Key> keys;
        private bool reload;

        public Table()
        {
            switch (Server.ProviderType)
            {
                case ProviderType.SqlServer: strategy = new SqlColumnStrategy(); break;
                case ProviderType.MySql: strategy = new MySqlColumnStrategy(); break;
                case ProviderType.PostgresSql: strategy = new PostgresColumnStrategy(); break;
                case ProviderType.Oracle: strategy = new OracleColumnStrategy(); break;
            }
        }

        [Browsable(false), DefaultValue(false)]
        public ICollection<Column> Columns
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
        public ICollection<Key> Keys
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