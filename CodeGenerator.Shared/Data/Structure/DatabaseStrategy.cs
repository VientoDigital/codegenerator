using System.Collections.Generic;
using System.Data;
using CodeGenerator.Data;

namespace CodeGenerator.Data.Structure
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

        protected internal ICollection<Database> GetDatabases()
        {
            var databases = new List<Database>();
            var dataAccessProviderFactory = new DataAccessProviderFactory(Server.ProviderType);
            var connection = dataAccessProviderFactory.CreateConnection(Server.ConnectionString);

            var set = DatabaseSchema(dataAccessProviderFactory, connection);

            foreach (DataRow row in set.Tables[0].Rows)
            {
                databases.Add(CreateDatabase(row, databases));
            }

            return databases;
        }

        protected abstract Database CreateDatabase(DataRow row, ICollection<Database> databases);

        protected abstract DataSet DatabaseSchema(DataAccessProviderFactory dataAccessProviderFactory, IDbConnection connection);
    }
}