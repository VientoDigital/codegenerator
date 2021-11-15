using System.Text.RegularExpressions;
using CodeGenerator.Data.Structure;

namespace CodeGenerator.Generator
{
    public class ColumnLengthExpression : Expression
    {
        public ColumnLengthExpression()
        {
        }

        public override void Interpret(Context context)
        {
            var column = (Column)Parameter;
            context.Output = Regex.Replace(context.Input, "COLUMN.LENGTH".DelimeterWrap(), column.Length.ToString());
            context.Input = context.Output;
        }
    }
}