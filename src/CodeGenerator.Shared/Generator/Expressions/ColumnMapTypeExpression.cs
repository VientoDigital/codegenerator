using System.Collections;
using System.Text.RegularExpressions;
using CodeGenerator.Data.Structure;
using CodeGenerator.Data.TypeConversion;
using CodeGenerator.Shared.Extensions;

namespace CodeGenerator.Generator
{
    public class ColumnMapTypeExpression : Expression
    {
        private static DataTypeManager manager = null;

        protected IDictionary Mappings => manager.SelectedLanguage.Mappings;

        public ColumnMapTypeExpression() : base()
        {
            if (manager == null)
            {
                manager = new DataTypeManager();
            }
        }

        public override void Interpret(Context context)
        {
            var column = (Column)Parameter;
            IDictionary mappings = Mappings;
            string strValue;

            if (mappings.Contains(column.Type.ToLower()))
            {
                strValue = mappings[column.Type.ToLower()].ToString();
            }
            else
            {
                strValue = "object";
            }

            context.Output = Regex.Replace(context.Input, "MAP COLUMN.TYPE".DelimeterWrap(), strValue);
            context.Input = context.Output;
        }
    }
}