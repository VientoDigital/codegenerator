using System.Collections;
using iCodeGenerator.Generator;

namespace iCodeGenerator.iCodeGeneratorGui
{
    /// <summary>
    /// Summary description for SnippetsHelper.
    /// </summary>
    public class SnippetsHelper
    {
        private Hashtable _htSnippets = new Hashtable();

        public SnippetsHelper()
        {
            LoadSnippets();
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

        private void AddTableSchema()
        {
            _htSnippets.Add("{TABLE.SCHEMA...",
                            Context.StartDelimeter
                            + "TABLE.SCHEMA"
                            + Context.EndingDelimiter
                );
        }

        private void AddTableName()
        {
            _htSnippets.Add("{TABLE.NAME...",
                        Context.StartDelimeter
                        + "TABLE.NAME"
                        + Context.EndingDelimiter
            );
        }

        private void AddDatabaseName()
        {
            _htSnippets.Add("{DATABASE.NAME...",
                            Context.StartDelimeter
                            + "DATABASE.NAME"
                            + Context.EndingDelimiter
                );
        }

        private void AddColumnType()
        {
            _htSnippets.Add("{COLUMN.TYPE...",
                            Context.StartDelimeter
                            + "COLUMN.TYPE"
                            + Context.EndingDelimiter
                );
        }

        private void AddIfColumnName()
        {
            _htSnippets.Add("{IF COLUMN.NAME...",
                            Context.StartDelimeter
                            + "IF COLUMN.NAME =~|!~ 'text'"
                            + Context.EndingDelimiter
                            + Context.StartDelimeter
                            + "/IF"
                            + Context.EndingDelimiter
                );
        }

        private void AddMapColumnType()
        {
            _htSnippets.Add("{MAP COLUMN.TYPE...",
                            Context.StartDelimeter
                            + "MAP COLUMN.TYPE"
                            + Context.EndingDelimiter
                );
        }

        private void AddColumnLength()
        {
            _htSnippets.Add("{COLUMN.LENGTH...",
                            Context.StartDelimeter
                            + "COLUMN.LENGTH"
                            + Context.EndingDelimiter
                );
        }

        private void AddIfColumnType()
        {
            _htSnippets.Add("{IF COLUMN.TYPE...",
                            Context.StartDelimeter
                            + "IF COLUMN.TYPE EQ|NE ''"
                            + Context.EndingDelimiter
                            + Context.StartDelimeter
                            + "/IF"
                            + Context.EndingDelimiter
                );
        }

        private void AddIfColumnNullable()
        {
            _htSnippets.Add("{IF COLUMN.NULLABLE...",
                            Context.StartDelimeter
                            + "IF NOT COLUMN.NULLABLE"
                            + Context.EndingDelimiter
                            + Context.StartDelimeter
                            + "/IF"
                            + Context.EndingDelimiter
                );
        }

        private void AddIfLast()
        {
            _htSnippets.Add("{IF LAST...",
                            Context.StartDelimeter
                            + "IF NOT LAST"
                            + Context.EndingDelimiter
                            + Context.StartDelimeter
                            + "/IF"
                            + Context.EndingDelimiter
                );
        }

        private void AddColumnDefault()
        {
            _htSnippets.Add("{COLUMN.DEFAULT...",
                            Context.StartDelimeter
                            + "COLUMN.DEFAULT"
                            + Context.EndingDelimiter
                );
        }

        private void AddColumnName()
        {
            _htSnippets.Add("{COLUMN.NAME...",
                            Context.StartDelimeter
                            + "COLUMN.NAME UPPER|LOWER|CAMEL|HUMAN"
                            + Context.EndingDelimiter
                );
        }

        private void AddTableColumns()
        {
            _htSnippets.Add("{TABLE.COLUMNS...",
                            Context.StartDelimeter
                            + "TABLE.COLUMNS PRIMARY|NOPRIMARY"
                            + Context.EndingDelimiter
                            + Context.StartDelimeter
                            + "/TABLE.COLUMNS"
                            + Context.EndingDelimiter
                );
        }

        public Hashtable Snippets
        {
            get { return _htSnippets; }
        }
    }
}