using System.Collections;
using CodeGenerator.Data.Structure;

namespace CodeGenerator.Generator
{
    public class Parser : Expression
    {
        private readonly ArrayList expressions = new ArrayList();
        private readonly Table table;

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