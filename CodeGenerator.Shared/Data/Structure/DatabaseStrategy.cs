using System.Collections.Generic;
using System.Data;

namespace CodeGenerator.Data.Structure
{
    public abstract class DatabaseStrategy
    {
        public Database SelectedDatabase
        {
            get
            {
                var providerFactory = new ProviderFactory(Server.ProviderType);
                var connection = providerFactory.CreateConnection(Server.ConnectionString);
                return new Database
                {
                    Name = connection.Database
                };
            }
        }

        protected internal ICollection<Database> GetDatabases()
        {
            var databases = new List<Database>();
            var providerFactory = new ProviderFactory(Server.ProviderType);
            var connection = providerFactory.CreateConnection(Server.ConnectionString);

            var set = DatabaseSchema(providerFactory, connection);

            foreach (DataRow row in set.Tables[0].Rows)
            {
                databases.Add(CreateDatabase(row, databases));
            }

            return databases;
        }

        protected abstract Database CreateDatabase(DataRow row, ICollection<Database> databases);

        protected abstract DataSet DatabaseSchema(ProviderFactory dataAccessProviderFactory, IDbConnection connection);
    }
}