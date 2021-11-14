using System.Text.RegularExpressions;
using CodeGenerator.Data.Structure;
using CodeGenerator.Shared.Extensions;

namespace CodeGenerator.Generator
{
    public class DatabaseNameExpression : Expression
    {
        private const string DATABASE_NAME = "DATABASE.NAME";

        private static string InputPattern => $@"{DATABASE_NAME}\s*(?<casing>(CAMEL|PASCAL|HUMAN|UNDERSCORE|UPPER|LOWER|HYPHEN|HYPHEN_LOWER|HYPHEN_UPPER))*".DelimeterWrap();

        public override void Interpret(Context context)
        {
            var database = ((Table)Parameter).ParentDatabase;
            var regex = new Regex(InputPattern, RegexOptions.Singleline);
            string input = context.Input;
            var matches = regex.Matches(input);

            foreach (Match match in matches)
            {
                string matchValue = match.Value;
                string casing = match.Groups["casing"].ToString();
                string replacement = database.Name;
                replacement = CaseConversion(casing, replacement, database.Name);
                input = Regex.Replace(input, matchValue, replacement);
            }

            context.Output = input;
            context.Input = context.Output;
        }
    }
}