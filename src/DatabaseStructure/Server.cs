using iCodeGenerator.GenericDataAccess;

namespace iCodeGenerator.DatabaseStructure
{
    public class Server
    {
        #region Attributes

        private DatabaseStrategy _strategy;
        private bool _reload;
        private DatabaseCollection _databases;
        private static DataProviderType _providerType;
        private static string _connectionString;

        #endregion Attributes

        #region Properties

        public static DataProviderType ProviderType
        {
            get { return _providerType; }
            set { _providerType = value; }
        }

        public DatabaseCollection Databases
        {
            get
            {
                if (_reload || _databases == null)
                {
                    _databases = _strategy.GetDatabases();
                }
                return _databases;
            }
        }

        public Database SelectedDatabase
        {
            get
            {
                return _strategy.SelectedDatabase;
            }
        }

        public static string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        #endregion Properties

        #region Methods

        public void Reload()
        {
            _reload = true;
        }

        #endregion Methods

        #region Constructor

        public Server()
        {
            if (_providerType == DataProviderType.SqlClient)
            {
                _strategy = new DatabaseStrategySQLServer();
            }
            else if (_providerType == DataProviderType.MySql)
            {
                _strategy = new DatabaseStrategyMySQL();
            }
            else if (_providerType == DataProviderType.PostgresSql)
            {
                _strategy = new DatabaseStrategyPostgres();
            }
            else if (_providerType == DataProviderType.Oracle)
            {
                _strategy = new DatabaseStrategyOracle();
            }
        }

        #endregion Constructor
    }
}