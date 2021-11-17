using System.Text.RegularExpressions;
using CodeGenerator.Data.Structure;

namespace CodeGenerator.Generator
{
    public class DatabaseNameExpression : Expression
    {
        private static string InputPattern => $@"DATABASE.NAME\s*(?<casing>(CAMEL|PASCAL|HUMAN|UNDERSCORE|UPPER|LOWER|HYPHEN|HYPHEN_LOWER|HYPHEN_UPPER))*".DelimeterWrap();

        public override void Interpret(Context context)
        {
            var database = ((Table)Parameter).ParentDatabase;
            var regex = new Regex(InputPattern, RegexOptions.Singleline);
            string result = context.Input;
            var matches = regex.Matches(result);

            foreach (Match match in matches)
            {
                string matchValue = match.Value;
                string casing = match.Groups["casing"].Value;
                string replacement = database.Name;
                replacement = CaseConversion(casing, replacement, database.Name);
                result = Regex.Replace(result, matchValue, replacement);
            }

            context.Output = result;
            context.Input = context.Output;
        }
    }
}