using System.Collections.Generic;

namespace CodeGenerator.UI
{
    /// <summary>
    /// Summary description for SnippetsHelper.
    /// </summary>
    public static class SnippetsHelper
    {
        public static IDictionary<string, string> Snippets { get; } = new Dictionary<string, string>();

        static SnippetsHelper()
        {
            Snippets.Add("{DATABASE.NAME…", "DATABASE.NAME CAMEL|PASCAL|HUMAN|UNDERSCORE|UPPER|LOWER|HYPHEN|HYPHEN_LOWER|HYPHEN_UPPER".DelimeterWrap());
            Snippets.Add("{DATABASE.TABLES…", "DATABASE.TABLES".DelimeterWrap() + "/DATABASE.TABLES".DelimeterWrap());

            Snippets.Add("{TABLE.SCHEMA…", "TABLE.SCHEMA".DelimeterWrap());
            Snippets.Add("{TABLE.NAME…", "TABLE.NAME CAMEL|PASCAL|HUMAN|UNDERSCORE|UPPER|LOWER|HYPHEN|HYPHEN_LOWER|HYPHEN_UPPER".DelimeterWrap());
            Snippets.Add("{TABLE.NAME.REPLACE…", "TABLE.NAME.REPLACE(OldValue,NewValue)".DelimeterWrap());
            Snippets.Add("{TABLE.COLUMNS…", "TABLE.COLUMNS PRIMARY|NOPRIMARY".DelimeterWrap() + "/TABLE.COLUMNS".DelimeterWrap());

            Snippets.Add("{COLUMN.NAME…", "COLUMN.NAME CAMEL|PASCAL|HUMAN|UNDERSCORE|UPPER|LOWER|HYPHEN|HYPHEN_LOWER|HYPHEN_UPPER".DelimeterWrap());
            Snippets.Add("{COLUMN.TYPE…", "COLUMN.TYPE".DelimeterWrap());
            Snippets.Add("{MAP COLUMN.TYPE…", "MAP COLUMN.TYPE".DelimeterWrap());
            Snippets.Add("{COLUMN.LENGTH…", "COLUMN.LENGTH".DelimeterWrap());
            Snippets.Add("{COLUMN.DEFAULT…", "COLUMN.DEFAULT".DelimeterWrap());

            Snippets.Add("{IF COLUMN.NAME…", "IF COLUMN.NAME =~|!~ 'text'".DelimeterWrap() + "/IF".DelimeterWrap());
            Snippets.Add("{IF COLUMN.TYPE…", "IF COLUMN.TYPE EQ|NE ''".DelimeterWrap() + "/IF".DelimeterWrap());
            Snippets.Add("{IF COLUMN.NULLABLE…", "IF NOT COLUMN.NULLABLE".DelimeterWrap() + "/IF".DelimeterWrap());
            Snippets.Add("{IF LAST…", "IF NOT LAST".DelimeterWrap() + "/IF".DelimeterWrap());
        }
    }
}