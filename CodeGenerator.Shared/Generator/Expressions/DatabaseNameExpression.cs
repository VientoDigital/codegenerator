using System.Text.RegularExpressions;
using CodeGenerator.Data.Structure;
using CodeGenerator.Shared.Extensions;

namespace CodeGenerator.Generator
{
    public class DatabaseNameExpression : Expression
    {
        private const string DATABASE_NAME = "DATABASE.NAME";

        private static string InputPattern => $@"{DATABASE_NAME}\s*(?<naming>(CAMEL|PASCAL|HUMAN|UNDERSCORE|UPPER|LOWER|HYPHEN|HYPHEN_LOWER|HYPHEN_UPPER))*".DelimeterWrap();

        public override void Interpret(Context context)
        {
            var database = ((Table)Parameter).ParentDatabase;
            var regex = new Regex(InputPattern, RegexOptions.Singleline);
            var inputString = context.Input;
            var matches = regex.Matches(inputString);

            foreach (Match match in matches)
            {
                var matchString = match.Value;
                var naming = match.Groups["naming"].ToString();
                var replacement = database.Name;
                replacement = CaseConversion(naming, replacement, database.Name);
                inputString = Regex.Replace(inputString, matchString, replacement);
            }

            context.Output = inputString;
            context.Input = context.Output;
        }
    }
}