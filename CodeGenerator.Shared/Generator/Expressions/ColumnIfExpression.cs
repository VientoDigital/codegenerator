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
                // TODO: Consider supporting a "FIRST" check as well.. can check first or last
                return @"\s*" +
                    @"IF COLUMN.LASTCHECK (?<not>NOT )?LAST".DelimeterWrap() +
                    //Content between IF tags
                    "(?<content>.+?)" +
                    "/IF COLUMN.LASTCHECK".DelimeterWrap() +
                    @"(?<end>\s*)";
            }
        }

        public override void Interpret(Context context)
        {
            var columns = ((IEnumerable<Column>)context.Extra).ToList();
            bool isLastColumn = (columns.Count == (columns.IndexOf((Column)Parameter) + 1));

            string result = context.Input;
            var regex = new Regex(InputPattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            var matches = regex.Matches(result);

            foreach (Match match in matches)
            {
                bool matchNotLast;
                string matchValue = match.Value;
                if (match.Length != 0)
                {
                    string content = match.Groups["content"].Value;
                    string endString = match.Groups["end"].Value;
                    string replacement = content + endString;

                    matchNotLast = (match.Groups["not"].Value.Length != 0);

                    if (matchNotLast && isLastColumn)
                    {
                        ReplaceContent(matchValue, string.Empty, ref result);
                    }
                    else if (!matchNotLast && isLastColumn)
                    {
                        ReplaceContent(matchValue, replacement, ref result);
                    }
                    else if (matchNotLast && !isLastColumn)
                    {
                        ReplaceContent(matchValue, replacement, ref result);
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

        private static void ReplaceContent(string matchValue, string replacement, ref string input)
        {
            input = Regex.Replace(input, Regex.Escape(matchValue), replacement);
        }
    }
}