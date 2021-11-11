using System.Text.RegularExpressions;
using CodeGenerator.Data.Structure;
using CodeGenerator.Shared.Extensions;

namespace CodeGenerator.Generator
{
    public class ColumnIfNullableExpression : Expression
    {
        public ColumnIfNullableExpression()
        {
        }

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
            string inputString = context.Input;
            var matches = regex.Matches(inputString);
            foreach (Match match in matches)
            {
                if (match.Length == 0)
                {
                    continue;
                }
                string matchString = match.Value;
                string contentString = match.Groups["content"].ToString();
                string endString = match.Groups["end"].ToString();
                string replacementString = contentString + endString;
                bool isIfNullable = !(match.Groups["not"].ToString().IndexOf("NOT") != -1);
                bool isIfNotNullable = !isIfNullable;
                if (isIfNotNullable && !column.Nullable)
                {
                    ReplaceContent(matchString, replacementString, ref inputString);
                }
                else if (isIfNullable && column.Nullable)
                {
                    ReplaceContent(matchString, replacementString, ref inputString);
                }
                else
                {
                    ReplaceContent(match.Value, string.Empty, ref inputString);
                }
                context.Output = inputString;
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