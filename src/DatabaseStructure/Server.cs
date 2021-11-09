using iCodeGenerator.GenericDataAccess;

namespace iCodeGenerator.DatabaseStructure
{
    public class Server
    {
        private readonly DatabaseStrategy strategy;
        private DatabaseCollection databases;
        private bool reload;

        public Server()
        {
            switch (ProviderType)
            {
                case DataProviderType.SqlClient: strategy = new DatabaseStrategySQLServer(); break;
                case DataProviderType.MySql: strategy = new DatabaseStrategyMySQL(); break;
                case DataProviderType.PostgresSql: strategy = new DatabaseStrategyPostgres(); break;
                case DataProviderType.Oracle: strategy = new DatabaseStrategyOracle(); break;
            }
        }

        public static string ConnectionString { get; set; }

        public static DataProviderType ProviderType { get; set; }

        public DatabaseCollection Databases
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