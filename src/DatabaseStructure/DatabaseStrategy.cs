using System.Data;
using iCodeGenerator.GenericDataAccess;

namespace iCodeGenerator.DatabaseStructure
{
    public abstract class DatabaseStrategy
    {
        protected internal DatabaseCollection GetDatabases()
        {
            DatabaseCollection databases = new DatabaseCollection();
            DataAccessProviderFactory dataAccessProviderFactory = new DataAccessProviderFactory(Server.ProviderType);
            IDbConnection connection = dataAccessProviderFactory.CreateConnection(Server.ConnectionString);

            DataSet ds = DatabaseSchema(dataAccessProviderFactory, connection);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                databases.Add(CreateDatabase(row, databases));
            }
            return databases;
        }

        protected abstract Database CreateDatabase(DataRow row, DatabaseCollection databases);

        protected abstract DataSet DatabaseSchema(DataAccessProviderFactory dataAccessProviderFactory, IDbConnection connection);

        public Database SelectedDatabase
        {
            get
            {
                DataAccessProviderFactory dataAccessProviderFactory = new DataAccessProviderFactory(Server.ProviderType);
                IDbConnection connection = dataAccessProviderFactory.CreateConnection(Server.ConnectionString);
                Database database = new Database();
                database.Name = connection.Database;
                return database;
            }
        }
    }
}