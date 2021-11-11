using System.Text.RegularExpressions;
using CodeGenerator.Data.Structure;
using CodeGenerator.Shared.Extensions;

namespace CodeGenerator.Generator
{
    public class TableSchemaExpression : Expression
    {
        public override void Interpret(Context context)
        {
            var table = (Table)Parameter;
            context.Output = Regex.Replace(context.Input, "TABLE.SCHEMA".DelimeterWrap(), table.Schema);
            context.Input = context.Output;
        }
    }
}