using System.Text.RegularExpressions;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.Generator
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
