using System;
using System.Data;
using CodeGenerator.GenericDataAccess;

namespace CodeGenerator.DatabaseStructure
{
    public abstract class TableStrategy
    {
        protected internal TableCollection GetTables(Database database)
        {
            var tables = new TableCollection();
            var dataAccessProviderFactory = new DataAccessProviderFactory(Server.ProviderType);
            var connection = dataAccessProviderFactory.CreateConnection(Server.ConnectionString);

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            connection.ChangeDatabase(database.Name);
            DataSet set;
            if (Server.ProviderType != DataProviderType.Oracle)
            {
                connection.ChangeDatabase(database.Name);
                set = TableSchema(dataAccessProviderFactory, connection);
            }
            else
            {
                set = TableSchema(dataAccessProviderFactory, connection, database);
            }

            connection.Close();

            /* Changed by Ferhat */
            if (set.Tables.Count > 0)
            {
                foreach (DataRow row in set.Tables[0].Rows)
                {
                    tables.Add(CreateTable(database, row));
                }
            }
            return tables;
        }

        /* Add by Ferhat */

        protected internal TableCollection GetViews(Database database)
        {
            var tables = new TableCollection();
            var dataAccessProviderFactory = new DataAccessProviderFactory(Server.ProviderType);
            var connection = dataAccessProviderFactory.CreateConnection(Server.ConnectionString);

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            connection.ChangeDatabase(database.Name);
            var ds = ViewSchema(dataAccessProviderFactory, connection);
            connection.Close();

            if (ds.Tables.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    tables.Add(CreateTable(database, row));
                }
            }
            return tables;
        }

        protected abstract Table CreateTable(Database database, DataRow row);

        protected abstract DataSet TableSchema(DataAccessProviderFactory dataAccessProvider, IDbConnection connection);

        protected virtual DataSet TableSchema(DataAccessProviderFactory dataAccessProvider, IDbConnection connection, Database database)
        {
            throw new NotImplementedException();
        }

        protected abstract DataSet ViewSchema(DataAccessProviderFactory dataAccessProvider, IDbConnection connection);
    }
}