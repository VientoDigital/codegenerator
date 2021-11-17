using System.Text.RegularExpressions;
using CodeGenerator.Data.Structure;

namespace CodeGenerator.Generator
{
    public class ColumnTypeExpression : Expression
    {
        public ColumnTypeExpression()
        {
        }

        public override void Interpret(Context context)
        {
            var column = (Column)Parameter;
            context.Output = Regex.Replace(context.Input, "COLUMN.TYPE".DelimeterWrap(), column.NativeType);
            context.Input = context.Output;
        }
    }
}