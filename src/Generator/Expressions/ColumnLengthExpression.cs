using System.Text.RegularExpressions;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.Generator
{
    public class ColumnLengthExpression : Expression
    {
        public ColumnLengthExpression()
        {
        }

        public override void Interpret(Context context)
        {
            Column column = (Column)Parameter;
            context.Output = Regex.Replace(context.Input, Context.StartDelimeter + "COLUMN.LENGTH" + Context.EndingDelimiter, column.Length.ToString());
            context.Input = context.Output;
        }
    }
}