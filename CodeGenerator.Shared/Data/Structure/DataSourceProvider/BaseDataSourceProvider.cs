using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Extenso;
using Oracle.ManagedDataAccess.Client;

namespace CodeGenerator.Data.Structure
{
    public abstract class BaseDataSourceProvider<TDbConnection> : IDataSourceProvider, IDisposable
        where TDbConnection : DbConnection
    {
        protected readonly TDbConnection connection;
        private readonly ProviderFactory providerFactory;
        private readonly ICollection<Column> columns = new List<Column>();
        private readonly ICollection<Key> keys = new List<Key>();
        private bool isDisposed;

        protected BaseDataSourceProvider()
        {
            providerFactory = new ProviderFactory(Server.ProviderType);
            connection = providerFactory.CreateConnection(Server.ConnectionString) as TDbConnection;
        }

        public Database SelectedDatabase => new()
        {
            Name = connection.Database
        };

        #region Databases

        public ICollection<Database> GetDatabases()
        {
            var databases = new List<Database>();
            var providerFactory = new ProviderFactory(Server.ProviderType);
            return GetDatabaseNames().Select(x => new Database { Name = x }).ToList();
        }

        protected abstract IEnumerable<string> GetDatabaseNames();

        #endregion Databases

        #region Tables / VIews

        public IEnumerable<Table> GetTables(Database database)
        {
            var providerFactory = new ProviderFactory(Server.ProviderType);

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            if (Server.ProviderType != DataSource.Oracle)
            {
                connection.ChangeDatabase(database.Name);
            }

            var set = GetTableSchema(database, providerFactory);
            connection.Close();
            return set;
        }

        public IEnumerable<Table> GetViews(Database database)
        {
            var providerFactory = new ProviderFactory(Server.ProviderType);

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            if (Server.ProviderType != DataSource.Oracle)
            {
                connection.ChangeDatabase(database.Name);
            }

            var set = GetViewSchema(database, providerFactory);
            connection.Close();
            return set;
        }

        protected abstract IEnumerable<Table> GetTableSchema(Database database, ProviderFactory providerFactory);

        protected abstract IEnumerable<Table> GetViewSchema(Database database, ProviderFactory providerFactory);

        #endregion Tables / VIews

        #region Columns / Keys

        protected ICollection<Column> Columns => columns;

        protected ICollection<Key> Keys => keys;

        public ICollection<Column> GetColumns(Table table)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            if (Server.ProviderType != DataSource.Oracle)
            {
                connection.ChangeDatabase(table.ParentDatabase.Name);
            }

            var columns = GetColumnSchema(table, providerFactory);
            foreach (var column in columns)
            {
                if (connection is OracleConnection)
                {
                    // Only needed for Oracle (for now), because the other providers take care of this..
                    column.IsPrimaryKey = table.Keys.OfType<Key>().FirstOrDefault(x => x.IsPrimary && x.ColumnName == column.Name) != null;
                }

                Columns.Add(column);
            }
            connection.Close();
            return Columns;
        }

        protected abstract IEnumerable<Column> GetColumnSchema(Table table, ProviderFactory providerFactory);

        public ICollection<Key> GetKeys(Table table)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            if (Server.ProviderType != DataSource.Oracle)
            {
                connection.ChangeDatabase(table.ParentDatabase.Name);
            }

            var keys = GetKeySchema(table, providerFactory);
            foreach (var key in keys)
            {
                Keys.Add(key);
            }
            connection.Close();
            return Keys;
        }

        protected abstract IEnumerable<Key> GetKeySchema(Table table, ProviderFactory providerFactory);

        #endregion Columns / Keys

        #region IDisposable Members

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    connection.DisposeIfNotNull();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                isDisposed = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ColumnStrategy()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}