using System.Text.RegularExpressions;
using CodeGenerator.Data.Structure;
using CodeGenerator.Shared.Extensions;

namespace CodeGenerator.Generator
{
    public class ColumnMapTypeExpression : Expression
    {
        public override void Interpret(Context context)
        {
            var column = (Column)Parameter;

            string value;
            if (ConfigFile.Instance.SelectedLanguage.Mappings.ContainsKey(column.Type.ToLower()))
            {
                value = ConfigFile.Instance.SelectedLanguage.Mappings[column.Type.ToLower()].ToString();
            }
            else
            {
                value = "object";
            }

            if (column.Nullable && value != "string")
            {
                value += "?";
            }

            context.Output = Regex.Replace(context.Input, "MAP COLUMN.TYPE".DelimeterWrap(), value);
            context.Input = context.Output;
        }
    }
}