using System.Collections;
using CodeGenerator.Shared.Extensions;

namespace CodeGenerator.UI
{
    /// <summary>
    /// Summary description for SnippetsHelper.
    /// </summary>
    public class SnippetsHelper
    {
        private readonly Hashtable snippets = new Hashtable();

        public SnippetsHelper()
        {
            LoadSnippets();
        }

        public Hashtable Snippets => snippets;

        private void AddColumnDefault()
        {
            snippets.Add("{COLUMN.DEFAULT...", "COLUMN.DEFAULT".DelimeterWrap());
        }

        private void AddColumnLength()
        {
            snippets.Add("{COLUMN.LENGTH...", "COLUMN.LENGTH".DelimeterWrap());
        }

        private void AddColumnName()
        {
            snippets.Add("{COLUMN.NAME...", "COLUMN.NAME UPPER|LOWER|CAMEL|HUMAN".DelimeterWrap());
        }

        private void AddColumnType()
        {
            snippets.Add("{COLUMN.TYPE...", "COLUMN.TYPE".DelimeterWrap());
        }

        private void AddDatabaseName()
        {
            snippets.Add("{DATABASE.NAME...", "DATABASE.NAME".DelimeterWrap());
        }

        private void AddIfColumnName()
        {
            snippets.Add("{IF COLUMN.NAME...", "IF COLUMN.NAME =~|!~ 'text'".DelimeterWrap() + "/IF".DelimeterWrap());
        }

        private void AddIfColumnNullable()
        {
            snippets.Add("{IF COLUMN.NULLABLE...", "IF NOT COLUMN.NULLABLE".DelimeterWrap() + "/IF".DelimeterWrap());
        }

        private void AddIfColumnType()
        {
            snippets.Add("{IF COLUMN.TYPE...", "IF COLUMN.TYPE EQ|NE ''".DelimeterWrap() + "/IF".DelimeterWrap());
        }

        private void AddIfLast()
        {
            snippets.Add("{IF LAST...", "IF NOT LAST".DelimeterWrap() + "/IF".DelimeterWrap());
        }

        private void AddMapColumnType()
        {
            snippets.Add("{MAP COLUMN.TYPE...", "MAP COLUMN.TYPE".DelimeterWrap());
        }

        private void AddTableColumns()
        {
            snippets.Add("{TABLE.COLUMNS...", "TABLE.COLUMNS PRIMARY|NOPRIMARY".DelimeterWrap() + "/TABLE.COLUMNS".DelimeterWrap());
        }

        private void AddTableName()
        {
            snippets.Add("{TABLE.NAME...", "TABLE.NAME".DelimeterWrap());
        }

        private void AddTableSchema()
        {
            snippets.Add("{TABLE.SCHEMA...", "TABLE.SCHEMA".DelimeterWrap());
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