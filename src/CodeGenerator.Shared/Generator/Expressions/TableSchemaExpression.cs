using System.Text.RegularExpressions;
using CodeGenerator.DatabaseStructure;

namespace CodeGenerator.Generator
{
    public class TableSchemaExpression : Expression
    {
        public override void Interpret(Context context)
        {
            var table = (Table)Parameter;
            context.Output = Regex.Replace(context.Input, Context.StartDelimeter + "TABLE.SCHEMA" + Context.EndingDelimiter, table.Schema);
            context.Input = context.Output;
        }
    }
}