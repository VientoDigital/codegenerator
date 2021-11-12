using System.Collections.Generic;
using System.Data;

namespace CodeGenerator.Data.Structure
{
    public abstract class ColumnStrategy
    {
        private readonly IDbConnection connection;
        private readonly DataAccessProviderFactory dataAccessProviderFactory;
        private readonly ICollection<Column> columns;
        private readonly ICollection<Key> keys;

        protected ColumnStrategy()
        {
            dataAccessProviderFactory = new DataAccessProviderFactory(Server.ProviderType);
            connection = dataAccessProviderFactory.CreateConnection(Server.ConnectionString);
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

            if (Server.ProviderType != DataProviderType.Oracle)
            {
                connection.ChangeDatabase(table.ParentDatabase.Name);
            }

            var set = ColumnSchema(table, dataAccessProviderFactory, connection);
            foreach (DataRow row in set.Tables[0].Rows)
            {
                var column = CreateColumn(row);
                column.SetParentTable(table);
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

        protected abstract DataSet ColumnSchema(Table table, DataAccessProviderFactory dataAccessProvider, IDbConnection connection);

        protected abstract Column CreateColumn(DataRow row);

        public ICollection<Key> GetKeys(Table table)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            if (Server.ProviderType != DataProviderType.Oracle)
            {
                connection.ChangeDatabase(table.ParentDatabase.Name);
            }
            DataSet set = KeySchema(table, dataAccessProviderFactory, connection);
            foreach (DataRow row in set.Tables[0].Rows)
            {
                var key = CreateKey(row);
                Keys.Add(key);
            }
            connection.Close();
            return Keys;
        }

        protected abstract DataSet KeySchema(Table table, DataAccessProviderFactory dataAccessProvider, IDbConnection connection);

        protected abstract Key CreateKey(DataRow row);
    }
}