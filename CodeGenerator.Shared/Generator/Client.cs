using System;
using System.Collections.Generic;
using CodeGenerator.Data.Structure;

namespace CodeGenerator.Generator
{
    public class Client
    {
        private readonly Context context;

        public Client()
        {
            context = new Context();
        }

        public event EventHandler OnComplete;

        public IDictionary<string, string> CustomValues { get; set; }

        public Table Table { get; set; }

        public string Parse()
        {
            string result = Intrepret();
            OnComplete?.Invoke(this, new EventArgs());
            return result;
        }

        public string Parse(Table table, string inputString)
        {
            Table = table;
            context.Input = inputString;
            string result = Intrepret();
            OnComplete?.Invoke(this, new EventArgs());
            return result;
        }

        private string Intrepret()
        {
            var parser = new Parser(Table);

            var columnsExpression = new ColumnsExpression();
            columnsExpression.AddExpression(new ColumnIfTypeExpression());
            columnsExpression.AddExpression(new ColumnNameExpression());
            columnsExpression.AddExpression(new ColumnTypeExpression());
            columnsExpression.AddExpression(new ColumnLengthExpression());
            columnsExpression.AddExpression(new ColumnDefaultExpression());
            columnsExpression.AddExpression(new ColumnMapTypeExpression());
            columnsExpression.AddExpression(new ColumnIfExpression());
            columnsExpression.AddExpression(new ColumnIfNullableExpression());
            columnsExpression.AddExpression(new ColumnNameMatchesExpression());
            parser.AddExpression(columnsExpression);

            var tablesExpression = new TablesExpression();
            tablesExpression.AddExpression(new TableNameExpression());
            tablesExpression.AddExpression(new TableSchemaExpression());
            tablesExpression.AddExpression(new DatabaseNameExpression());
            parser.AddExpression(tablesExpression);

            parser.AddExpression(new TableNameExpression());
            parser.AddExpression(new TableSchemaExpression());
            parser.AddExpression(new DatabaseNameExpression());

            if (CustomValues != null)
            {
                foreach (var entry in CustomValues)
                {
                    parser.AddExpression(new LiteralExpression(entry.Key, entry.Value));
                }
            }

            parser.Interpret(context);
            return context.Output;
        }
    }
}