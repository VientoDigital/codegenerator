using System.Collections.Generic;
using System.Data;

namespace CodeGenerator.Data.Structure
{
    public abstract class TableStrategy
    {
        protected internal IEnumerable<Table> GetTables(Database database)
        {
            var providerFactory = new ProviderFactory(Server.ProviderType);
            using var connection = providerFactory.CreateConnection(Server.ConnectionString);

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            if (Server.ProviderType != ProviderType.Oracle)
            {
                connection.ChangeDatabase(database.Name);
            }

            var set = TableSchema(database, providerFactory, connection);
            connection.Close();
            return set;
        }

        protected internal IEnumerable<Table> GetViews(Database database)
        {
            var providerFactory = new ProviderFactory(Server.ProviderType);
            using var connection = providerFactory.CreateConnection(Server.ConnectionString);

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            if (Server.ProviderType != ProviderType.Oracle)
            {
                connection.ChangeDatabase(database.Name);
            }

            var set = ViewSchema(database, providerFactory, connection);
            connection.Close();
            return set;
        }

        protected abstract IEnumerable<Table> TableSchema(Database database, ProviderFactory providerFactory, IDbConnection connection);

        protected abstract IEnumerable<Table> ViewSchema(Database database, ProviderFactory providerFactory, IDbConnection connection);
    }
}