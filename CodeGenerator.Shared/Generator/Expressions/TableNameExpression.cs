using System.Text.RegularExpressions;
using CodeGenerator.Data.Structure;

namespace CodeGenerator.Generator
{
    public class TableNameExpression : Expression
    {
        private static string InputPattern => @"TABLE.NAME\s*(?<casing>(CAMEL|PASCAL|HUMAN|UNDERSCORE|UPPER|LOWER|HYPHEN|HYPHEN_LOWER|HYPHEN_UPPER))*".DelimeterWrap();

        public override void Interpret(Context context)
        {
            var table = (Table)Parameter;
            var regex = new Regex(InputPattern, RegexOptions.Singleline);
            string result = context.Input;
            var matches = regex.Matches(result);

            foreach (Match match in matches)
            {
                string matchValue = match.Value;
                string casing = match.Groups["casing"].Value;
                string tableName = table.Name;
                tableName = CaseConversion(casing, tableName, table.Name);
                result = Regex.Replace(result, matchValue, tableName);
            }

            context.Output = result;
            context.Input = context.Output;
        }
    }
}