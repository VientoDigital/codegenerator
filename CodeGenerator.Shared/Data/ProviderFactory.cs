using System;
using System.Data;
using System.Data.Common;

namespace CodeGenerator.Data
{
    public class ProviderFactory
    {
        private readonly ProviderType providerType;

        public ProviderFactory(ProviderType providerType)
        {
            this.providerType = providerType;
        }

        public string DbProviderInvarianName => providerType switch
        {
            ProviderType.SqlServer => "System.Data.SqlClient",
            ProviderType.MySql => "MySql.Data.MySqlClient",
            ProviderType.PostgresSql => "Npgsql",
            ProviderType.Oracle => "Oracle.ManagedDataAccess.Client",
            _ => throw new ArgumentOutOfRangeException(nameof(providerType)),
        };

        public DbProviderFactory DbProviderFactory => DbProviderFactories.GetFactory(DbProviderInvarianName);

        public static IDbCommand CreateCommand(string cmdText, IDbConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = cmdText;
            return command;
        }

        public DbConnection CreateConnection(string connectionString)
        {
            var connection = DbProviderFactory.CreateConnection();
            connection.ConnectionString = connectionString;
            return connection;
        }

        public IDbDataAdapter CreateDataAdapter()
        {
            return DbProviderFactory.CreateDataAdapter();
        }

        public IDbDataParameter CreateParameter()
        {
            return DbProviderFactory.CreateParameter();
        }
    }
}