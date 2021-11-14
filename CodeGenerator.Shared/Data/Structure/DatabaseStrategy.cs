using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace CodeGenerator.Data.Structure
{
    public abstract class DatabaseStrategy
    {
        public static Database SelectedDatabase
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

            return GetDatabaseNames(connection).Select(x => new Database { Name = x }).ToList();
        }

        protected abstract IEnumerable<string> GetDatabaseNames(DbConnection connection);
    }
}