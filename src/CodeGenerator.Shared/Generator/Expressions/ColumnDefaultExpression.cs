using System.Text.RegularExpressions;
using CodeGenerator.DatabaseStructure;

namespace CodeGenerator.Generator
{
    public class ColumnDefaultExpression : Expression
    {
        public ColumnDefaultExpression()
        {
        }

        public override void Interpret(Context context)
        {
            var column = (Column)Parameter;
            context.Output = Regex.Replace(context.Input, Context.StartDelimeter + "COLUMN.DEFAULT" + Context.EndingDelimiter, column.Default);
            context.Input = context.Output;
        }
    }
}