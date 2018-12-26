using System.Collections;
using System.Text.RegularExpressions;
using iCodeGenerator.DatabaseStructure;
using iCodeGenerator.DataTypeConverter;

namespace iCodeGenerator.Generator
{
    public class ColumnMapTypeExpression : Expression
    {
        private static DataTypeManager manager = null;

        protected IDictionary Mappings
        {
            get { return manager.SelectedLanguage.Mappings; }
        }

        public ColumnMapTypeExpression() : base()
        {
            if (manager == null)
            {
                manager = new DataTypeManager();
            }
        }

        public override void Interpret(Context context)
        {
            Column column = (Column)Parameter;
            string strValue = "";
            IDictionary mappings = Mappings;
            if (mappings.Contains(column.Type.ToLower()))
            {
                strValue = mappings[column.Type.ToLower()].ToString();
            }
            else
            {
                strValue = "object";
            }

            context.Output = Regex.Replace(context.Input, Context.StartDelimeter + "MAP COLUMN.TYPE" + Context.EndingDelimiter, strValue);
            context.Input = context.Output;
        }
    }
}