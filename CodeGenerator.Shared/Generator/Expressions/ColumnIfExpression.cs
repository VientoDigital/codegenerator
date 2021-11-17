using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CodeGenerator.Data.Structure;

namespace CodeGenerator.Generator
{
    public class ColumnIfExpression : Expression
    {
        private static string InputPattern
        {
            get
            {
                return @"\s*" +
                    @"IF (?<not>NOT )?LAST".DelimeterWrap() +
                    //Content between IF tags
                    "(?<content>.+?)" +
                    "/IF".DelimeterWrap() +
                    @"(?<end>\s*)";
            }
        }

        public override void Interpret(Context context)
        {
            var columns = ((IEnumerable<Column>)context.Extra).ToList();
            bool isLastColumn = (columns.Count == (columns.IndexOf((Column)Parameter) + 1));

            string result = context.Input;
            var regex = new Regex(InputPattern, RegexOptions.Singleline);
            var matches = regex.Matches(result);

            foreach (Match match in matches)
            {
                bool matchNotLast;
                string matchValue = match.Value;
                if (match.Length != 0)
                {
                    string content = match.Groups["content"].Value;
                    string endString = match.Groups["end"].Value;
                    string replacementString = content + endString;
                    matchNotLast = (match.Groups["not"].Value.Length != 0);

                    if (matchNotLast && isLastColumn)
                    {
                        ReplaceContent(matchValue, string.Empty, ref result);
                    }
                    else if (!matchNotLast && isLastColumn)
                    {
                        ReplaceContent(matchValue, replacementString, ref result);
                    }
                    else if (matchNotLast && !isLastColumn)
                    {
                        ReplaceContent(matchValue, replacementString, ref result);
                    }
                    else if (!matchNotLast && !isLastColumn)
                    {
                        ReplaceContent(matchValue, string.Empty, ref result);
                    }
                }
            }
            context.Output = result;
            context.Input = context.Output;
        }

        private static void ReplaceContent(string matchString, string replacementString, ref string inputString)
        {
            inputString = Regex.Replace(inputString, Regex.Escape(matchString), replacementString);
        }
    }
}