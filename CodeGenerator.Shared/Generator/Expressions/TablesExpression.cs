using System.Collections.Generic;
using System.Text.RegularExpressions;
using CodeGenerator.Data.Structure;

namespace CodeGenerator.Generator
{
    public class TablesExpression : Expression
    {
        private readonly ICollection<Expression> expressions;

        public TablesExpression()
        {
            expressions = new List<Expression>();
        }

        private static string InputPattern
        {
            get
            {
                return
                    @"DATABASE.TABLES".DelimeterWrap() +
                    "(?<table>.+?)" +
                    "/DATABASE.TABLES".DelimeterWrap();
            }
        }

        public override void AddExpression(Expression expression)
        {
            expressions.Add(expression);
        }

        public override void Interpret(Context context)
        {
            var regex = new Regex(InputPattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            var matches = regex.Matches(context.Input);

            foreach (Match match in matches)
            {
                string tableOutput = string.Empty;
                string tableInput = match.Groups["table"].Value;
                var database = ((Table)Parameter).ParentDatabase;
                var tables = database.Tables;

                foreach (var table in tables)
                {
                    string tableTemporaryText = tableInput;
                    RunExpressionsReplace(table, tables, ref tableTemporaryText);
                    tableOutput += tableTemporaryText;
                }

                string escapedString = Regex.Escape(match.Value);
                context.Output = Regex.Replace(context.Input, escapedString, tableOutput);
                context.Input = context.Output;
            }
        }

        public override void RemoveExpression(Expression expression)
        {
            expressions.Remove(expression);
        }

        private void RunExpressionsReplace(Table table, IEnumerable<Table> tables, ref string tableInputText)
        {
            var tableContext = new Context
            {
                Extra = tables
            };

            foreach (var expression in expressions)
            {
                tableContext.Input = tableInputText;
                expression.Parameter = table;
                expression.Interpret(tableContext);
                tableInputText = tableContext.Output;
            }
        }
    }
}