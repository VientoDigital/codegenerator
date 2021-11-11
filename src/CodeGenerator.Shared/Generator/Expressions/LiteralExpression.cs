using System.Text.RegularExpressions;
using CodeGenerator.Shared.Extensions;

namespace CodeGenerator.Generator
{
    public class LiteralExpression : Expression
    {
        private readonly string key;
        private readonly string value;

        public LiteralExpression(string strKey, string strValue)
        {
            key = strKey;
            value = strValue;
        }

        public override void Interpret(Context context)
        {
            context.Output = Regex.Replace(context.Input, key.DelimeterWrap(), value);
            context.Input = context.Output;

            var inputPattern = $@"{key}\s*(?<naming>(CAMEL|PASCAL|HUMAN|UNDERSCORE|UPPER|LOWER|HYPHEN|HYPHEN_LOWER|HYPHEN_UPPER))*".DelimeterWrap();

            var regex = new Regex(inputPattern, RegexOptions.Singleline);
            var inputString = context.Input;
            var matches = regex.Matches(inputString);

            foreach (Match match in matches)
            {
                var matchString = match.Value;
                var naming = match.Groups["naming"].ToString();
                var replacement = value;
                replacement = CaseConversion(naming, replacement, value);
                inputString = Regex.Replace(inputString, matchString, replacement);
            }

            context.Output = inputString;
            context.Input = context.Output;
        }
    }
}