using System.Collections.Generic;
using System.ComponentModel;

namespace CodeGenerator.Data.Structure
{
    public class Database
    {
        private readonly TableStrategy strategy;
        private bool reload;
        private ICollection<Table> tables;
        private ICollection<Table> views;

        public Database()
        {
            switch (Server.ProviderType)
            {
                case DataProviderType.SqlClient: strategy = new SqlTableStrategy(); break;
                case DataProviderType.MySql: strategy = new MySqlTableStrategy(); break;
                case DataProviderType.PostgresSql: strategy = new PostgresTableStrategy(); break;
                case DataProviderType.Oracle: strategy = new OracleTableStrategy(); break;
            }
        }

        [Category("Database"), ReadOnly(true)]
        public string Name { get; set; }

        [Browsable(false), DefaultValue(false)]
        public ICollection<Table> Tables
        {
            get
            {
                if (reload || tables == null)
                {
                    tables = strategy.GetTables(this);
                    reload = false;
                }
                return tables;
            }
        }

        [Browsable(false), DefaultValue(false)]
        public ICollection<Table> Views
        {
            get
            {
                if (reload || views == null)
                {
                    views = strategy.GetViews(this);
                    reload = false;
                }
                return views;
            }
        }

        public void Reload() => reload = true;
    }
}