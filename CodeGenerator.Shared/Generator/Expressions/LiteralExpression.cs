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

            string inputPattern = $@"{key}\s*(?<casing>(CAMEL|PASCAL|HUMAN|UNDERSCORE|UPPER|LOWER|HYPHEN_LOWER|HYPHEN_UPPER|HYPHEN))*".DelimeterWrap();

            var regex = new Regex(inputPattern, RegexOptions.Singleline);
            string result = context.Input;
            var matches = regex.Matches(result);

            foreach (Match match in matches)
            {
                string matchValue = match.Value;
                string replacement = value;
                string casing = match.Groups["casing"].Value;

                replacement = CaseConversion(casing, replacement);
                result = Regex.Replace(result, matchValue, replacement);
            }

            context.Output = result;
            context.Input = context.Output;
        }
    }
}