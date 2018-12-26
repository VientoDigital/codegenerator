using System;
using System.Data;

namespace iCodeGenerator.GenericDataAccess
{
    public class DataAccessProviderFactory
    {
        #region Attributtes

        private DataAccessProviderTypeFactory _typeAccessProvider;

        #endregion Attributtes

        #region Constructors

        private DataAccessProviderFactory()
        {
        }

        public DataAccessProviderFactory(DataProviderType providerType)
        {
            _typeAccessProvider = new DataAccessProviderTypeFactory(providerType);
        }

        public DataAccessProviderFactory(DataAccessProviderTypeFactory typeProvider)
        {
            _typeAccessProvider = typeProvider;
        }

        public DataAccessProviderFactory(string providerTypeName)
        {
            _typeAccessProvider = new DataAccessProviderTypeFactory(providerTypeName);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Creates a Connection
        /// SqlClient: "SERVER=(local);DATABASE=;UID=sa;PWD=;
        /// MySql: "Data Source=test;Password=;User ID=root;Location=localhost;"
        /// <returns></returns>
        public IDbConnection CreateConnection()
        {
            return (IDbConnection)Activator.CreateInstance(_typeAccessProvider.ConnectionType);
        }

        public IDbConnection CreateConnection(string connectionString)
        {
            IDbConnection conn = null;
            Object[] args = { connectionString };
            conn = (IDbConnection)Activator.CreateInstance(_typeAccessProvider.ConnectionType, args);
            return conn;
        }

        public IDbCommand CreateCommand()
        {
            IDbCommand command = null;
            command = (IDbCommand)Activator.CreateInstance(_typeAccessProvider.CommandType);
            return command;
        }

        public IDbCommand CreateCommand(string cmdText)
        {
            IDbCommand command = null;
            Object[] args = { cmdText };
            command = (IDbCommand)Activator.CreateInstance(_typeAccessProvider.CommandType, args);
            return command;
        }

        public IDbCommand CreateCommand(string cmdText, IDbConnection connection)
        {
            IDbCommand command = null;
            Object[] args = { cmdText, connection };
            command = (IDbCommand)Activator.CreateInstance(_typeAccessProvider.CommandType, args);
            return command;
        }

        public IDbCommand CreateCommand(string cmdText, IDbConnection connection, IDbTransaction transaction)
        {
            IDbCommand command = null;
            Object[] args = { cmdText, connection, transaction };
            command = (IDbCommand)Activator.CreateInstance(_typeAccessProvider.CommandType, args);
            return command;
        }

        public IDbDataAdapter CreateDataAdapter()
        {
            IDbDataAdapter dataAdapter = null;
            dataAdapter = (IDbDataAdapter)Activator.CreateInstance(_typeAccessProvider.DataAdapterType);
            return dataAdapter;
        }

        public IDbDataAdapter CreateDataAdapter(IDbCommand selectCommand)
        {
            IDbDataAdapter dataAdapter = null;
            Object[] args = { selectCommand };
            dataAdapter = (IDbDataAdapter)Activator.CreateInstance(_typeAccessProvider.DataAdapterType, args);
            return dataAdapter;
        }

        public IDbDataAdapter CreateDataAdapter(string selectCommandText, string selectConnectionString)
        {
            IDbDataAdapter dataAdapter = null;
            Object[] args = { selectCommandText, selectConnectionString };
            dataAdapter = (IDbDataAdapter)Activator.CreateInstance(_typeAccessProvider.DataAdapterType, args);
            return dataAdapter;
        }

        public IDbDataAdapter CreateDataAdapter(string selectCommandText, IDbConnection connection)
        {
            IDbDataAdapter dataAdapter = null;
            Object[] args = { selectCommandText, connection };
            dataAdapter = (IDbDataAdapter)Activator.CreateInstance(_typeAccessProvider.DataAdapterType, args);
            return dataAdapter;
        }

        public IDbDataParameter CreateParameter()
        {
            IDbDataParameter parameter = null;
            parameter = (IDbDataParameter)Activator.CreateInstance(_typeAccessProvider.ParameterType);
            return parameter;
        }

        public IDbDataParameter CreateParameter(string parameterName, object value)
        {
            IDbDataParameter param = null;
            object[] args = { parameterName, value };
            param = (IDbDataParameter)Activator.CreateInstance(_typeAccessProvider.ParameterType, args);
            return param;
        }

        public IDbDataParameter CreateParameter(string parameterName, DbType dataType)
        {
            IDbDataParameter param = CreateParameter();

            if (param != null)
            {
                param.ParameterName = parameterName;
                param.DbType = dataType;
            }

            return param;
        }

        public IDbDataParameter CreateParameter(string parameterName, DbType dataType, int size)
        {
            IDbDataParameter param = CreateParameter();

            if (param != null)
            {
                param.ParameterName = parameterName;
                param.DbType = dataType;
                param.Size = size;
            }

            return param;
        }

        public IDbDataParameter CreateParameter(string parameterName, DbType dataType, int size, string sourceColumn)
        {
            IDbDataParameter param = CreateParameter();

            if (param != null)
            {
                param.ParameterName = parameterName;
                param.DbType = dataType;
                param.Size = size;
                param.SourceColumn = sourceColumn;
            }

            return param;
        }

        #endregion Methods
    }
}