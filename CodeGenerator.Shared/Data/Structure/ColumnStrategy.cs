using System;
using System.Collections.Generic;
using System.Data;
using Extenso;

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
                column.ParentTable = table;
                foreach (Key key in table.Keys)
                {
                    if (key.IsPrimary)
                    {
                        if (key.ColumnName == column.Name)
                        {
                            column.IsPrimaryKey = true;
                            continue;
                        }
                    }
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
            DataSet set = KeySchema(table, providerFactory, connection);
            foreach (DataRow row in set.Tables[0].Rows)
            {
                var key = CreateKey(row);
                Keys.Add(key);
            }
            connection.Close();
            return Keys;
        }

        protected abstract DataSet KeySchema(Table table, ProviderFactory providerFactory, IDbConnection connection);

        protected abstract Key CreateKey(DataRow row);

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
    }
}