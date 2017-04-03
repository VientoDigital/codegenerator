using System.Text.RegularExpressions;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.Generator
{
    public class ColumnTypeExpression : Expression
    {
        public ColumnTypeExpression()
        {
        }

        public override void Interpret(Context context)
        {
            Column column = (Column)Parameter;
            context.Output = Regex.Replace(context.Input, Context.StartDelimeter + "COLUMN.TYPE" + Context.EndingDelimiter, column.Type);
            context.Input = context.Output;
        }
    }
}