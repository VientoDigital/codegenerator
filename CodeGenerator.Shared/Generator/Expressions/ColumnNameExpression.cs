using System.Text.RegularExpressions;
using CodeGenerator.Data.Structure;

namespace CodeGenerator.Generator
{
    public class ColumnNameExpression : Expression
    {
        private const string COLUMN_NAME = "COLUMN.NAME";

        private static string InputPattern => $@"{COLUMN_NAME}\s*(?<casing>(CAMEL|PASCAL|HUMAN|UNDERSCORE|UPPER|LOWER|HYPHEN|HYPHEN_LOWER|HYPHEN_UPPER))*".DelimeterWrap();

        public override void Interpret(Context context)
        {
            var column = (Column)Parameter;
            var regex = new Regex(InputPattern, RegexOptions.Singleline);
            string input = context.Input;
            var matches = regex.Matches(input);

            foreach (Match match in matches)
            {
                string matchValue = match.Value;
                string casing = match.Groups["casing"].ToString();
                string replacement = column.Name;
                replacement = CaseConversion(casing, replacement, column.Name);
                input = Regex.Replace(input, matchValue, replacement);
            }

            context.Output = input;
            context.Input = context.Output;
        }
    }
}