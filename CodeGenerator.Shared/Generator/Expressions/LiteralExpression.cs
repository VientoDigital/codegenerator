using System.Text.RegularExpressions;

namespace CodeGenerator.Generator
{
    public class LiteralExpression : Expression
    {
        private readonly string key;
        private readonly string value;

        public LiteralExpression(string key, string value)
        {
            this.key = key;
            this.value = value;
        }

        public override void Interpret(Context context)
        {
            context.Output = Regex.Replace(context.Input, key.DelimeterWrap(), value);
            context.Input = context.Output;

            string inputPattern = $@"{key}\s*(?<casing>(CAMEL|PASCAL|HUMAN|UNDERSCORE|UPPER|LOWER|HYPHEN|HYPHEN_LOWER|HYPHEN_UPPER))*".DelimeterWrap();

            var regex = new Regex(inputPattern, RegexOptions.Singleline);
            string input = context.Input;
            var matches = regex.Matches(input);

            foreach (Match match in matches)
            {
                string matchValue = match.Value;
                string casing = match.Groups["casing"].ToString();
                string replacement = value;
                replacement = CaseConversion(casing, replacement, value);
                input = Regex.Replace(input, matchValue, replacement);
            }

            context.Output = input;
            context.Input = context.Output;
        }
    }
}