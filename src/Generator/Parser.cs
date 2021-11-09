using System.Collections;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.Generator
{
    public class Parser : Expression
    {
        private ArrayList expressions = new ArrayList();
        private Table table;

        public Parser(Table table)
        {
            this.table = table;
        }

        public override void AddExpression(Expression expression)
        {
            expressions.Add(expression);
        }

        public override void Interpret(Context context)
        {
            foreach (Expression expression in expressions)
            {
                expression.Parameter = table;
                expression.Interpret(context);
            }
        }

        public override void RemoveExpression(Expression expression)
        {
            expressions.Remove(expression);
        }
    }
}