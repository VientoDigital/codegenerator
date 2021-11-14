using System;
using System.Collections.Generic;
using System.Data;

namespace CodeGenerator.Data.Structure
{
    public abstract class TableStrategy
    {
        protected internal ICollection<Table> GetTables(Database database)
        {
            var tables = new List<Table>();
            var providerFactory = new ProviderFactory(Server.ProviderType);
            var connection = providerFactory.CreateConnection(Server.ConnectionString);

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            //connection.ChangeDatabase(database.Name);
            DataSet set;
            if (Server.ProviderType != ProviderType.Oracle)
            {
                connection.ChangeDatabase(database.Name);
                set = TableSchema(providerFactory, connection);
            }
            else
            {
                set = TableSchema(providerFactory, connection, database);
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

        protected internal ICollection<Table> GetViews(Database database)
        {
            var tables = new List<Table>();
            var providerFactory = new ProviderFactory(Server.ProviderType);
            var connection = providerFactory.CreateConnection(Server.ConnectionString);

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            if (Server.ProviderType != ProviderType.Oracle)
            {
                connection.ChangeDatabase(database.Name);
            }

            var set = ViewSchema(providerFactory, connection);
            connection.Close();

            if (set.Tables.Count > 0)
            {
                foreach (DataRow row in set.Tables[0].Rows)
                {
                    tables.Add(CreateTable(database, row));
                }
            }
            return tables;
        }

        protected abstract Table CreateTable(Database database, DataRow row);

        protected abstract DataSet TableSchema(ProviderFactory providerFactory, IDbConnection connection);

        protected virtual DataSet TableSchema(ProviderFactory providerFactory, IDbConnection connection, Database database)
        {
            throw new NotImplementedException();
        }

        protected abstract DataSet ViewSchema(ProviderFactory providerFactory, IDbConnection connection);
    }
}