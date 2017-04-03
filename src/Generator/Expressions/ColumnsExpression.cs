using System.Collections;
using System.Text.RegularExpressions;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.Generator
{
    public class ColumnsExpression : Expression
    {
        private const string PRIMARY = "PRIMARY";
        private const string NOPRIMARY = "NOPRIMARY";
        private ArrayList _expressions;

        public ColumnsExpression()
        {
            _expressions = new ArrayList();
        }

        public override void Interpret(Context context)
        {
            Regex regex = new Regex(InputPattern, RegexOptions.Singleline);
            MatchCollection matches = regex.Matches(context.Input);
            foreach (Match match in matches)
            {
                string columnOutput = "";
                string columnInput = match.Groups["column"].Value;
                ColumnCollection columns = ((Table)Parameter).Columns;
                ColumnCollection filteredColumns = new ColumnCollection();
                foreach (Column column in columns)
                {
                    if (IsValidColumn(column, match.Groups["selection"].Value.Trim()))
                    {
                        filteredColumns.Add(column);
                    }
                }
                foreach (Column column in filteredColumns)
                {
                    string columnTemporaryText = columnInput;
                    RunExpressionsReplace(column, filteredColumns, ref columnTemporaryText);
                    columnOutput += columnTemporaryText;
                }
                string escapedString = Regex.Escape(match.Value);
                context.Output = Regex.Replace(context.Input, escapedString, columnOutput);
                context.Input = context.Output;
            }
        }

        private bool IsValidColumn(Column column, string selectionString)
        {
            if (selectionString == PRIMARY && !column.IsPrimaryKey)
            {
                return false;
            }
            else
            {
                if (selectionString == NOPRIMARY && column.IsPrimaryKey)
                {
                    return false;
                }
            }
            return true;
        }

        private static string InputPattern
        {
            get
            {
                return Context.StartDelimeter +
                    @"TABLE.COLUMNS(?<selection> (ALL|PRIMARY|NOPRIMARY))?" +
                    Context.EndingDelimiter +
                    "(?<column>.+?)" +
                    Context.StartDelimeter +
                    "/TABLE.COLUMNS" +
                    Context.EndingDelimiter;
            }
        }

        private void RunExpressionsReplace(Column column, object columns, ref string columnInputText)
        {
            Context columnContext = new Context();
            columnContext.Extra = columns;
            foreach (Expression expression in _expressions)
            {
                columnContext.Input = columnInputText;
                expression.Parameter = column;
                expression.Interpret(columnContext);
                columnInputText = columnContext.Output;
            }
        }

        public override void AddExpression(Expression expression)
        {
            _expressions.Add(expression);
        }

        public override void RemoveExpression(Expression expression)
        {
            _expressions.Remove(expression);
        }
    }
}