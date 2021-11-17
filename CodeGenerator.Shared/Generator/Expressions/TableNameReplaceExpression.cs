using System.Text.RegularExpressions;
using CodeGenerator.Data.Structure;

namespace CodeGenerator.Generator
{
    public class TableNameReplaceExpression : Expression
    {
        private static string InputPattern => @"TABLE.NAME.REPLACE\((?<oldValue>(.*)),(?<newValue>(.*))\)".DelimeterWrap();

        public override void Interpret(Context context)
        {
            var table = (Table)Parameter;
            var regex = new Regex(InputPattern, RegexOptions.Multiline);
            string result = context.Input;
            var matches = regex.Matches(result);

            foreach (Match match in matches)
            {
                string matchValue = match.Value;
                string tableName = table.Name;

                string oldValue = match.Groups["oldValue"].Value;
                string newValue = match.Groups["newValue"].Value;

                tableName = tableName.Replace(oldValue, newValue);
                result = Regex.Replace(result, Regex.Escape(matchValue), tableName);
            }

            context.Output = result;
            context.Input = context.Output;
        }
    }
}