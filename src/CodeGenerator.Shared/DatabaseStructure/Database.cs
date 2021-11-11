using System.ComponentModel;
using CodeGenerator.GenericDataAccess;

namespace CodeGenerator.DatabaseStructure
{
    public class Database
    {
        private readonly TableStrategy strategy;
        private bool reload;
        private TableCollection tables;
        private TableCollection views;

        public Database()
        {
            switch (Server.ProviderType)
            {
                case DataProviderType.SqlClient: strategy = new TableStrategySQLServer(); break;
                case DataProviderType.MySql: strategy = new TableStrategyMySQL(); break;
                case DataProviderType.PostgresSql: strategy = new TableStrategyPostgres(); break;
                case DataProviderType.Oracle: strategy = new TableStrategyOracle(); break;
            }
        }

        [Category("Database"), ReadOnly(true)]
        public string Name { get; set; }

        [Browsable(false), DefaultValue(false)]
        public TableCollection Tables
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
        public TableCollection Views
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