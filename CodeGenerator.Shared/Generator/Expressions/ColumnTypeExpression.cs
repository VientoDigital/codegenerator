using System.Text.RegularExpressions;
using CodeGenerator.Data.Structure;
using CodeGenerator.Shared.Extensions;

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
            context.Output = Regex.Replace(context.Input, "COLUMN.TYPE".DelimeterWrap(), column.Type);
            context.Input = context.Output;
        }
    }
}