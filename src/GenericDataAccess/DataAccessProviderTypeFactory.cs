using System;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Npgsql;

namespace iCodeGenerator.GenericDataAccess
{
    public enum DataProviderType
    {
        SqlClient,
        MySql,
        Access,
        PostgresSql,
        Oracle,
        ODBC
    }

    public class DataAccessProviderTypeFactory
    {
        private Type[] commandTypes = new Type[]
        {
            typeof(SqlCommand),
            typeof(MySqlCommand),
            typeof(OleDbCommand),
            typeof(NpgsqlCommand),
            typeof(OracleCommand)
        };

        private Type[] connectionTypes = new Type[]
        {
            typeof(SqlConnection),
            typeof(MySqlConnection),
            typeof(OleDbConnection),
            typeof(NpgsqlConnection),
            typeof(OracleConnection)
        };

        private Type[] dataAdapterTypes = new Type[]
        {
            typeof(SqlDataAdapter),
            typeof(MySqlDataAdapter),
            typeof(OleDbDataAdapter),
            typeof(NpgsqlDataAdapter),
            typeof(OracleDataAdapter)
        };

        private Type[] paramenterTypes = new Type[]
        {
            typeof(SqlParameter),
            typeof(MySqlParameter),
            typeof(OleDbParameter),
            typeof(NpgsqlParameter),
            typeof(OracleParameter)
        };

        private DataProviderType providerType;

        public DataAccessProviderTypeFactory(DataProviderType providerType)
        {
            this.providerType = providerType;
        }

        public DataAccessProviderTypeFactory(string dataTypeName)
        {
            switch (dataTypeName.ToLower().Trim())
            {
                case "sqlclient": providerType = DataProviderType.SqlClient; break;
                case "mysql": providerType = DataProviderType.MySql; break;
                case "access": providerType = DataProviderType.Access; break;
                case "postgressql": providerType = DataProviderType.PostgresSql; break;
                case "oracle": providerType = DataProviderType.Oracle; break;
            }
        }

        private DataAccessProviderTypeFactory()
        {
        }

        public Type CommandType => commandTypes[(int)providerType];

        public Type ConnectionType => connectionTypes[(int)providerType];

        public Type DataAdapterType => dataAdapterTypes[(int)providerType];

        public Type ParameterType => paramenterTypes[(int)providerType];
    }
}