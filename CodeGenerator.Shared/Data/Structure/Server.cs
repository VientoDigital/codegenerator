using System.Collections.Generic;

namespace CodeGenerator.Data.Structure
{
    public class Server
    {
        private readonly DatabaseStrategy strategy;
        private ICollection<Database> databases;
        private bool reload;

        public Server()
        {
            switch (ProviderType)
            {
                case ProviderType.SqlServer: strategy = new SqlDatabaseStrategy(); break;
                case ProviderType.MySql: strategy = new MySqlDatabaseStrategy(); break;
                case ProviderType.PostgresSql: strategy = new PostgresDatabaseStrategy(); break;
                case ProviderType.Oracle: strategy = new OracleDatabaseStrategy(); break;
            }
        }

        public static string ConnectionString { get; set; }

        public static ProviderType ProviderType { get; set; }

        public ICollection<Database> Databases
        {
            get
            {
                if (reload || databases == null)
                {
                    databases = strategy.GetDatabases();
                }
                return databases;
            }
        }

        public Database SelectedDatabase => strategy.SelectedDatabase;

        public void Reload() => reload = true;
    }
}