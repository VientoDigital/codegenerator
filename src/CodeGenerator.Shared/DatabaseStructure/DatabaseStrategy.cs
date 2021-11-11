using System.Data;
using CodeGenerator.GenericDataAccess;

namespace CodeGenerator.DatabaseStructure
{
    public abstract class DatabaseStrategy
    {
        public Database SelectedDatabase
        {
            get
            {
                var dataAccessProviderFactory = new DataAccessProviderFactory(Server.ProviderType);
                var connection = dataAccessProviderFactory.CreateConnection(Server.ConnectionString);
                return new Database
                {
                    Name = connection.Database
                };
            }
        }

        protected internal DatabaseCollection GetDatabases()
        {
            var databases = new DatabaseCollection();
            var dataAccessProviderFactory = new DataAccessProviderFactory(Server.ProviderType);
            var connection = dataAccessProviderFactory.CreateConnection(Server.ConnectionString);

            var set = DatabaseSchema(dataAccessProviderFactory, connection);

            foreach (DataRow row in set.Tables[0].Rows)
            {
                databases.Add(CreateDatabase(row, databases));
            }

            return databases;
        }

        protected abstract Database CreateDatabase(DataRow row, DatabaseCollection databases);

        protected abstract DataSet DatabaseSchema(DataAccessProviderFactory dataAccessProviderFactory, IDbConnection connection);
    }
}