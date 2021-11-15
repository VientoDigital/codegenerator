using System.Text.RegularExpressions;
using CodeGenerator.Data.Structure;

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

            if (column.Default != null)
            {
                context.Output = Regex.Replace(context.Input, "COLUMN.DEFAULT".DelimeterWrap(), column.Default);
                context.Input = context.Output;
            }
        }
    }
}