using System;
using System.Data;

namespace CodeGenerator.Data
{
    public class DataAccessProviderFactory
    {
        private readonly DataAccessProviderTypeFactory dataAccessProviderTypeFactory;

        public DataAccessProviderFactory(DataProviderType providerType)
        {
            dataAccessProviderTypeFactory = new DataAccessProviderTypeFactory(providerType);
        }

        public DataAccessProviderFactory(DataAccessProviderTypeFactory dataAccessProviderTypeFactory)
        {
            this.dataAccessProviderTypeFactory = dataAccessProviderTypeFactory;
        }

        public DataAccessProviderFactory(string providerTypeName)
        {
            dataAccessProviderTypeFactory = new DataAccessProviderTypeFactory(providerTypeName);
        }

        private DataAccessProviderFactory()
        {
        }

        public IDbCommand CreateCommand()
        {
            return (IDbCommand)Activator.CreateInstance(dataAccessProviderTypeFactory.CommandType);
        }

        public IDbCommand CreateCommand(string cmdText)
        {
            object[] args = { cmdText };
            return (IDbCommand)Activator.CreateInstance(dataAccessProviderTypeFactory.CommandType, args);
        }

        public IDbCommand CreateCommand(string cmdText, IDbConnection connection)
        {
            object[] args = { cmdText, connection };
            return (IDbCommand)Activator.CreateInstance(dataAccessProviderTypeFactory.CommandType, args);
        }

        public IDbCommand CreateCommand(string cmdText, IDbConnection connection, IDbTransaction transaction)
        {
            object[] args = { cmdText, connection, transaction };
            return (IDbCommand)Activator.CreateInstance(dataAccessProviderTypeFactory.CommandType, args);
        }

        /// <summary>
        /// Creates a Connection
        /// SqlClient: "SERVER=(local);DATABASE=;UID=sa;PWD=;
        /// MySql: "Data Source=test;Password=;User ID=root;Location=localhost;"
        /// <returns></returns>
        public IDbConnection CreateConnection()
        {
            return (IDbConnection)Activator.CreateInstance(dataAccessProviderTypeFactory.ConnectionType);
        }

        public IDbConnection CreateConnection(string connectionString)
        {
            object[] args = { connectionString };
            IDbConnection conn = (IDbConnection)Activator.CreateInstance(dataAccessProviderTypeFactory.ConnectionType, args);
            return conn;
        }

        public IDbDataAdapter CreateDataAdapter()
        {
            return (IDbDataAdapter)Activator.CreateInstance(dataAccessProviderTypeFactory.DataAdapterType);
        }

        public IDbDataAdapter CreateDataAdapter(IDbCommand selectCommand)
        {
            object[] args = { selectCommand };
            return (IDbDataAdapter)Activator.CreateInstance(dataAccessProviderTypeFactory.DataAdapterType, args);
        }

        public IDbDataAdapter CreateDataAdapter(string selectCommandText, string selectConnectionString)
        {
            object[] args = { selectCommandText, selectConnectionString };
            return (IDbDataAdapter)Activator.CreateInstance(dataAccessProviderTypeFactory.DataAdapterType, args);
        }

        public IDbDataAdapter CreateDataAdapter(string selectCommandText, IDbConnection connection)
        {
            object[] args = { selectCommandText, connection };
            return (IDbDataAdapter)Activator.CreateInstance(dataAccessProviderTypeFactory.DataAdapterType, args);
        }

        public IDbDataParameter CreateParameter()
        {
            return (IDbDataParameter)Activator.CreateInstance(dataAccessProviderTypeFactory.ParameterType);
        }

        public IDbDataParameter CreateParameter(string parameterName, object value)
        {
            object[] args = { parameterName, value };
            return (IDbDataParameter)Activator.CreateInstance(dataAccessProviderTypeFactory.ParameterType, args);
        }

        public IDbDataParameter CreateParameter(string parameterName, DbType dataType)
        {
            var parameter = CreateParameter();

            if (parameter != null)
            {
                parameter.ParameterName = parameterName;
                parameter.DbType = dataType;
            }

            return parameter;
        }

        public IDbDataParameter CreateParameter(string parameterName, DbType dataType, int size)
        {
            var parameter = CreateParameter();

            if (parameter != null)
            {
                parameter.ParameterName = parameterName;
                parameter.DbType = dataType;
                parameter.Size = size;
            }

            return parameter;
        }

        public IDbDataParameter CreateParameter(string parameterName, DbType dataType, int size, string sourceColumn)
        {
            var parameter = CreateParameter();

            if (parameter != null)
            {
                parameter.ParameterName = parameterName;
                parameter.DbType = dataType;
                parameter.Size = size;
                parameter.SourceColumn = sourceColumn;
            }

            return parameter;
        }
    }
}