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

        public string DbProviderInvarianName
        {
            get
            {
                switch (providerType)
                {
                    case ProviderType.SqlServer: return "Microsoft.Data.SqlClient";
                    case ProviderType.MySql: return "MySql.Data.MySqlClient";
                    case ProviderType.PostgresSql: return "Npgsql";
                    case ProviderType.Oracle: return "Oracle.DataAccess.Client";
                    default: throw new ArgumentOutOfRangeException(nameof(providerType));
                }
            }
        }

        public DbProviderFactory DbProviderFactory => DbProviderFactories.GetFactory(DbProviderInvarianName);

        public IDbCommand CreateCommand(string cmdText, IDbConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = cmdText;
            return command;
        }

        public IDbConnection CreateConnection(string connectionString)
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