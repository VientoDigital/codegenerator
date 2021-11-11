using System.Text.RegularExpressions;
using CodeGenerator.Data.Structure;
using CodeGenerator.Shared.Extensions;

namespace CodeGenerator.Generator
{
    public class ColumnNameExpression : Expression
    {
        private const string COLUMN_NAME = "COLUMN.NAME";

        public override void Interpret(Context context)
        {
            var column = (Column)Parameter;
            var regex = new Regex(InputPattern, RegexOptions.Singleline);
            var inputString = context.Input;
            var matches = regex.Matches(inputString);

            foreach (Match match in matches)
            {
                var matchString = match.Value;
                var naming = match.Groups["naming"].ToString();
                var replacement = column.Name;
                replacement = CaseConversion(naming, replacement, column.Name);
                inputString = Regex.Replace(inputString, matchString, replacement);
            }

            context.Output = inputString;
            context.Input = context.Output;
        }

        private static string InputPattern => $@"{COLUMN_NAME}\s*(?<naming>(CAMEL|PASCAL|HUMAN|UNDERSCORE|UPPER|LOWER|HYPHEN|HYPHEN_LOWER|HYPHEN_UPPER))*".DelimeterWrap();
    }
}