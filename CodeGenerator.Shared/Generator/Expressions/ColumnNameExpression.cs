using System.Text.RegularExpressions;
using CodeGenerator.Data.Structure;

namespace CodeGenerator.Generator
{
    public class ColumnNameExpression : Expression
    {
        private static string InputPattern => @"COLUMN.NAME\s*(?<casing>(CAMEL|PASCAL|HUMAN|UNDERSCORE|UPPER|LOWER|HYPHEN|HYPHEN_LOWER|HYPHEN_UPPER))*".DelimeterWrap();

        public override void Interpret(Context context)
        {
            var column = (Column)Parameter;
            var regex = new Regex(InputPattern, RegexOptions.Singleline);
            string result = context.Input;
            var matches = regex.Matches(result);

            foreach (Match match in matches)
            {
                string matchValue = match.Value;
                string casing = match.Groups["casing"].Value;
                string replacement = column.Name;
                replacement = CaseConversion(casing, replacement, column.Name);
                result = Regex.Replace(result, matchValue, replacement);
            }

            context.Output = result;
            context.Input = context.Output;
        }
    }
}