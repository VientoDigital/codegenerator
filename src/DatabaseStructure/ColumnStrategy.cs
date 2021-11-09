using System.Data;
using iCodeGenerator.GenericDataAccess;

namespace iCodeGenerator.DatabaseStructure
{
    public abstract class ColumnStrategy
    {
        private IDbConnection connection;
        private DataAccessProviderFactory dataAccessProviderFactory;
        private ColumnCollection columns;
        private KeyCollection keys;

        protected ColumnStrategy()
        {
            dataAccessProviderFactory = new DataAccessProviderFactory(Server.ProviderType);
            connection = dataAccessProviderFactory.CreateConnection(Server.ConnectionString);
            columns = new ColumnCollection();
            keys = new KeyCollection();
        }

        protected ColumnCollection Columns => columns;

        protected KeyCollection Keys => keys;

        public ColumnCollection GetColumns(Table table)
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

        public KeyCollection GetKeys(Table table)
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