using System.Text.RegularExpressions;
using CodeGenerator.Data.Structure;

namespace CodeGenerator.Generator
{
    public class ColumnIfNullableExpression : Expression
    {
        private static string InputPattern
        {
            get
            {
                return @"\s*" +
                    @"IF (?<not>NOT )?COLUMN.NULLABLE".DelimeterWrap() +
                    //Content between IF tags
                    "(?<content>.+?)" +
                    "/IF".DelimeterWrap() +
                    @"(?<end>\s*)";
            }
        }

        public override void Interpret(Context context)
        {
            var column = (Column)Parameter;
            var regex = new Regex(InputPattern, RegexOptions.Singleline);
            string result = context.Input;
            var matches = regex.Matches(result);

            foreach (Match match in matches)
            {
                if (match.Length == 0)
                {
                    continue;
                }
                string matchString = match.Value;
                string contentString = match.Groups["content"].Value;
                string endString = match.Groups["end"].Value;
                string replacementString = contentString + endString;
                bool isIfNullable = !(match.Groups["not"].Value.IndexOf("NOT") != -1);
                bool isIfNotNullable = !isIfNullable;
                if (isIfNotNullable && !column.Nullable)
                {
                    ReplaceContent(matchString, replacementString, ref result);
                }
                else if (isIfNullable && column.Nullable)
                {
                    ReplaceContent(matchString, replacementString, ref result);
                }
                else
                {
                    ReplaceContent(match.Value, string.Empty, ref result);
                }
                context.Output = result;
                context.Input = context.Output;
            }
        }

        private static void ReplaceContent(string matchString, string replacementString, ref string inputString)
        {
            //			inputString = Regex.Replace(inputString,matchString,replacementString);
            inputString = Regex.Replace(inputString, Regex.Escape(matchString), replacementString);
        }
    }
}