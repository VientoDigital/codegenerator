using System.Collections.Generic;
using System.ComponentModel;

namespace CodeGenerator.Data.Structure
{
    public class Table
    {
        private ICollection<Column> columns;
        private ICollection<Key> keys;
        private bool reload;

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
                    keys = Server.DataSourceProvider.GetKeys(this);
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
                    columns = Server.DataSourceProvider.GetColumns(this);
                }
                return columns;
            }
        }

        public void Reload() => reload = true;

        public override string ToString() => $"{Schema}.{Name}";
    }
}