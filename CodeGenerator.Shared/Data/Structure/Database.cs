using System.Collections.Generic;
using System.ComponentModel;

namespace CodeGenerator.Data.Structure
{
    // TODO: Add schemas collection in here..

    public class Database
    {
        private readonly TableStrategy strategy;
        private bool reload;
        private IEnumerable<Table> tables;
        private IEnumerable<Table> views;

        public Database()
        {
            switch (Server.ProviderType)
            {
                case ProviderType.SqlServer: strategy = new SqlTableStrategy(); break;
                case ProviderType.MySql: strategy = new MySqlTableStrategy(); break;
                case ProviderType.PostgresSql: strategy = new PostgresTableStrategy(); break;
                case ProviderType.Oracle: strategy = new OracleTableStrategy(); break;
            }
        }

        [Category("Database"), ReadOnly(true)]
        public string Name { get; set; }

        [Browsable(false), DefaultValue(false)]
        public IEnumerable<Table> Tables
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
        public IEnumerable<Table> Views
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

        public override string ToString() => $"{Name}";
    }
}