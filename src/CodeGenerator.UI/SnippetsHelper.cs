using System.Collections;
using CodeGenerator.Generator;

namespace CodeGenerator.CodeGenerator.UI
{
    /// <summary>
    /// Summary description for SnippetsHelper.
    /// </summary>
    public class SnippetsHelper
    {
        private Hashtable snippets = new Hashtable();

        public SnippetsHelper()
        {
            LoadSnippets();
        }

        public Hashtable Snippets => snippets;

        private void AddColumnDefault()
        {
            snippets.Add("{COLUMN.DEFAULT...", Context.StartDelimeter + "COLUMN.DEFAULT" + Context.EndingDelimiter);
        }

        private void AddColumnLength()
        {
            snippets.Add("{COLUMN.LENGTH...", Context.StartDelimeter + "COLUMN.LENGTH" + Context.EndingDelimiter);
        }

        private void AddColumnName()
        {
            snippets.Add("{COLUMN.NAME...", Context.StartDelimeter + "COLUMN.NAME UPPER|LOWER|CAMEL|HUMAN" + Context.EndingDelimiter);
        }

        private void AddColumnType()
        {
            snippets.Add("{COLUMN.TYPE...", Context.StartDelimeter + "COLUMN.TYPE" + Context.EndingDelimiter);
        }

        private void AddDatabaseName()
        {
            snippets.Add("{DATABASE.NAME...", Context.StartDelimeter + "DATABASE.NAME" + Context.EndingDelimiter);
        }

        private void AddIfColumnName()
        {
            snippets.Add("{IF COLUMN.NAME...", Context.StartDelimeter + "IF COLUMN.NAME =~|!~ 'text'" + Context.EndingDelimiter + Context.StartDelimeter + "/IF" + Context.EndingDelimiter);
        }

        private void AddIfColumnNullable()
        {
            snippets.Add("{IF COLUMN.NULLABLE...", Context.StartDelimeter + "IF NOT COLUMN.NULLABLE" + Context.EndingDelimiter + Context.StartDelimeter + "/IF" + Context.EndingDelimiter);
        }

        private void AddIfColumnType()
        {
            snippets.Add("{IF COLUMN.TYPE...", Context.StartDelimeter + "IF COLUMN.TYPE EQ|NE ''" + Context.EndingDelimiter + Context.StartDelimeter + "/IF" + Context.EndingDelimiter);
        }

        private void AddIfLast()
        {
            snippets.Add("{IF LAST...", Context.StartDelimeter + "IF NOT LAST" + Context.EndingDelimiter + Context.StartDelimeter + "/IF" + Context.EndingDelimiter);
        }

        private void AddMapColumnType()
        {
            snippets.Add("{MAP COLUMN.TYPE...", Context.StartDelimeter + "MAP COLUMN.TYPE" + Context.EndingDelimiter);
        }

        private void AddTableColumns()
        {
            snippets.Add("{TABLE.COLUMNS...", Context.StartDelimeter + "TABLE.COLUMNS PRIMARY|NOPRIMARY" + Context.EndingDelimiter + Context.StartDelimeter + "/TABLE.COLUMNS" + Context.EndingDelimiter);
        }

        private void AddTableName()
        {
            snippets.Add("{TABLE.NAME...", Context.StartDelimeter + "TABLE.NAME" + Context.EndingDelimiter);
        }

        private void AddTableSchema()
        {
            snippets.Add("{TABLE.SCHEMA...", Context.StartDelimeter + "TABLE.SCHEMA" + Context.EndingDelimiter);
        }

        private void LoadSnippets()
        {
            AddDatabaseName();
            AddTableName();
            AddTableSchema();
            AddColumnType();
            AddIfColumnName();
            AddMapColumnType();
            AddColumnLength();
            AddIfColumnType();
            AddIfColumnNullable();
            AddIfLast();
            AddColumnDefault();
            AddColumnName();
            AddTableColumns();
        }
    }
}