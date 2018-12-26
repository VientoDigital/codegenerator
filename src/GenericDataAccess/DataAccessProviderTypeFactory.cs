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
        #region Attributes

        private DataProviderType _providerType;

        private Type[] _connectionType = new Type[] {
                                typeof(SqlConnection),
                                typeof(MySqlConnection),
                                typeof(OleDbConnection),
                                typeof(NpgsqlConnection),
                                typeof(OracleConnection)
                                };

        private Type[] _commandType = new Type[] {
                                typeof(SqlCommand),
                                typeof(MySqlCommand),
                                typeof(OleDbCommand),
                                typeof(NpgsqlCommand),
                                typeof(OracleCommand),
                                 };

        private Type[] _dataAdapterType = new Type[] {
                                                         typeof(SqlDataAdapter),
                                                         typeof(MySqlDataAdapter),
                                                         typeof(OleDbDataAdapter),
                                                         typeof(NpgsqlDataAdapter),
                                                         typeof(OracleDataAdapter)
                                                     };

        private Type[] _paramenterType = new Type[] {
                                                        typeof(SqlParameter),
                                                        typeof(MySqlParameter),
                                                        typeof(OleDbParameter),
                                                        typeof(NpgsqlParameter),
                                                        typeof(OracleParameter)
                                                    };

        #endregion Attributes

        #region Constructor

        private DataAccessProviderTypeFactory()
        {
        }

        public DataAccessProviderTypeFactory(DataProviderType dataType)
        {
            _providerType = dataType;
        }

        public DataAccessProviderTypeFactory(string dataTypeName)
        {
            switch (dataTypeName.ToLower().Trim())
            {
                case "sqlclient":
                    _providerType = DataProviderType.SqlClient;
                    break;

                case "mysql":
                    _providerType = DataProviderType.MySql;
                    break;

                case "access":
                    _providerType = DataProviderType.Access;
                    break;

                case "postgressql":
                    _providerType = DataProviderType.PostgresSql;
                    break;

                case "oracle":
                    _providerType = DataProviderType.Oracle;
                    break;
            }
        }

        #endregion Constructor

        #region Properties

        public Type ConnectionType
        {
            get { return _connectionType[(int)_providerType]; }
        }

        public Type CommandType
        {
            get { return _commandType[(int)_providerType]; }
        }

        public Type DataAdapterType
        {
            get { return _dataAdapterType[(int)_providerType]; }
        }

        public Type ParameterType
        {
            get { return _paramenterType[(int)_providerType]; }
        }

        #endregion Properties
    }
}