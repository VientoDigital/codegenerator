using System.Text.RegularExpressions;
using CodeGenerator.Data.Structure;

namespace CodeGenerator.Generator
{
    public class ColumnNameExpression : Expression
    {
        private static string InputPattern => @"COLUMN.NAME\s*(?<casing>(CAMEL|PASCAL|HUMAN|UNDERSCORE|UPPER|LOWER|HYPHEN_LOWER|HYPHEN_UPPER|HYPHEN))*".DelimeterWrap();

        public override void Interpret(Context context)
        {
            var column = (Column)Parameter;
            var regex = new Regex(InputPattern, RegexOptions.Singleline);
            string result = context.Input;
            var matches = regex.Matches(result);

            foreach (Match match in matches)
            {
                string matchValue = match.Value;
                string columnName = column.Name;

                string casing = match.Groups["casing"].Value;
                if (!string.IsNullOrEmpty(casing))
                {
                    columnName = CaseConversion(casing, columnName);
                }

                result = Regex.Replace(result, Regex.Escape(matchValue), columnName);
            }

            context.Output = result;
            context.Input = context.Output;
        }
    }
}