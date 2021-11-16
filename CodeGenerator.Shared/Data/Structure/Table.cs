using System.Collections.Generic;
using System.ComponentModel;

namespace CodeGenerator.Data.Structure
{
    public class Table
    {
        private readonly ColumnStrategy columnStrategy;
        private ICollection<Column> columns;
        private ICollection<Key> keys;
        private bool reload;

        public Table()
        {
            switch (Server.ProviderType)
            {
                case ProviderType.SqlServer: columnStrategy = new SqlColumnStrategy(); break;
                case ProviderType.MySql: columnStrategy = new MySqlColumnStrategy(); break;
                case ProviderType.PostgresSql: columnStrategy = new PostgresColumnStrategy(); break;
                case ProviderType.Oracle: columnStrategy = new OracleColumnStrategy(); break;
            }
        }

        [Browsable(false), DefaultValue(false)]
        public Database ParentDatabase { get; set; }

        [Category("Table"), ReadOnly(true)]
        public string Schema { get; set; }

        [Category("Table"), ReadOnly(true)]
        public string Name { get; set; }

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
                    keys = columnStrategy.GetKeys(this);
                }
                return keys;
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
                    columns = columnStrategy.GetColumns(this);
                }
                return columns;
            }
        }

        public void Reload() => reload = true;

        public override string ToString() => $"{Schema}.{Name}";
    }
}