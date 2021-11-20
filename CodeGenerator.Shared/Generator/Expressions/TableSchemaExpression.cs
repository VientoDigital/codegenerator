using System.Text.RegularExpressions;
using CodeGenerator.Data.Structure;

namespace CodeGenerator.Generator
{
    public class TableSchemaExpression : Expression
    {
        public override void Interpret(Context context)
        {
            var table = (Table)Parameter;
            context.Output = Regex.Replace(context.Input, "TABLE.SCHEMA".DelimeterWrap(), table.Schema, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            context.Input = context.Output;
        }
    }
}