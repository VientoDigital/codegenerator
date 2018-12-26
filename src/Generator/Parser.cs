using System.Collections;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.Generator
{
    public class Parser : Expression
    {
        private ArrayList _expressions = new ArrayList();
        private Table _table;

        public override void AddExpression(Expression expression)
        {
            _expressions.Add(expression);
        }

        public override void RemoveExpression(Expression expression)
        {
            _expressions.Remove(expression);
        }

        public Parser(Table table)
        {
            _table = table;
        }

        public override void Interpret(Context context)
        {
            foreach (Expression expression in _expressions)
            {
                expression.Parameter = _table;
                expression.Interpret(context);
            }
        }
    }
}