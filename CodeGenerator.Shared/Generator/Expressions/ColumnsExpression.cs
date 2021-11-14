using System.Collections.Generic;
using System.Text.RegularExpressions;
using CodeGenerator.Data.Structure;
using CodeGenerator.Shared.Extensions;

namespace CodeGenerator.Generator
{
    public class ColumnsExpression : Expression
    {
        private const string NOPRIMARY = "NOPRIMARY";
        private const string PRIMARY = "PRIMARY";
        private readonly ICollection<Expression> expressions;

        public ColumnsExpression()
        {
            expressions = new List<Expression>();
        }

        private static string InputPattern
        {
            get
            {
                return
                    @"TABLE.COLUMNS(?<selection> (ALL|PRIMARY|NOPRIMARY))?".DelimeterWrap() +
                    "(?<column>.+?)" +
                    "/TABLE.COLUMNS".DelimeterWrap();
            }
        }

        public override void AddExpression(Expression expression)
        {
            expressions.Add(expression);
        }

        public override void Interpret(Context context)
        {
            var regex = new Regex(InputPattern, RegexOptions.Singleline);
            var matches = regex.Matches(context.Input);

            foreach (Match match in matches)
            {
                string columnOutput = string.Empty;
                string columnInput = match.Groups["column"].Value;
                var columns = ((Table)Parameter).Columns;
                var filteredColumns = new List<Column>();

                foreach (var column in columns)
                {
                    if (IsValidColumn(column, match.Groups["selection"].Value.Trim()))
                    {
                        filteredColumns.Add(column);
                    }
                }

                foreach (var column in filteredColumns)
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

        public override void RemoveExpression(Expression expression)
        {
            expressions.Remove(expression);
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

        private void RunExpressionsReplace(Column column, object columns, ref string columnInputText)
        {
            var columnContext = new Context
            {
                Extra = columns
            };

            foreach (Expression expression in expressions)
            {
                columnContext.Input = columnInputText;
                expression.Parameter = column;
                expression.Interpret(columnContext);
                columnInputText = columnContext.Output;
            }
        }
    }
}