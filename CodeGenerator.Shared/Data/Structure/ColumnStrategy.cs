using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Extenso;
using Oracle.ManagedDataAccess.Client;

namespace CodeGenerator.Data.Structure
{
    public abstract class ColumnStrategy : IDisposable
    {
        private readonly IDbConnection connection;
        private readonly ProviderFactory providerFactory;
        private readonly ICollection<Column> columns;
        private readonly ICollection<Key> keys;
        private bool isDisposed;

        protected ColumnStrategy()
        {
            providerFactory = new ProviderFactory(Server.ProviderType);
            connection = providerFactory.CreateConnection(Server.ConnectionString);
            columns = new List<Column>();
            keys = new List<Key>();
        }

        protected ICollection<Column> Columns => columns;

        protected ICollection<Key> Keys => keys;

        public ICollection<Column> GetColumns(Table table)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            if (Server.ProviderType != ProviderType.Oracle)
            {
                connection.ChangeDatabase(table.ParentDatabase.Name);
            }

            var columns = ColumnSchema(table, providerFactory, connection);
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

        protected abstract IEnumerable<Column> ColumnSchema(Table table, ProviderFactory providerFactory, IDbConnection connection);

        public ICollection<Key> GetKeys(Table table)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            if (Server.ProviderType != ProviderType.Oracle)
            {
                connection.ChangeDatabase(table.ParentDatabase.Name);
            }

            var keys = KeySchema(table, providerFactory, connection);
            foreach (var key in keys)
            {
                Keys.Add(key);
            }
            connection.Close();
            return Keys;
        }

        protected abstract IEnumerable<Key> KeySchema(Table table, ProviderFactory providerFactory, IDbConnection connection);

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