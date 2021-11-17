using System.Collections.Generic;

namespace CodeGenerator.Data.Structure
{
    public interface IDataSourceProvider
    {
        Database SelectedDatabase { get; }

        void Dispose();

        ICollection<Database> GetDatabases();

        IEnumerable<Table> GetTables(Database database);

        IEnumerable<Table> GetViews(Database database);

        ICollection<Column> GetColumns(Table table);

        ICollection<Key> GetKeys(Table table);
    }
}