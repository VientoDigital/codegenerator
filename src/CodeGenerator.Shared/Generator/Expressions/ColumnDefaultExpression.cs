using System.Text.RegularExpressions;
using CodeGenerator.Data.Structure;
using CodeGenerator.Shared.Extensions;

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
            context.Output = Regex.Replace(context.Input, "COLUMN.DEFAULT".DelimeterWrap(), column.Default);
            context.Input = context.Output;
        }
    }
}