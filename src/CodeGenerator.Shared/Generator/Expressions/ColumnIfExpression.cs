using System.Text.RegularExpressions;
using CodeGenerator.DatabaseStructure;

namespace CodeGenerator.Generator
{
    public class ColumnIfExpression : Expression
    {
        private static string InputPattern
        {
            get
            {
                return @"\s*" +
                    Context.StartDelimeter +
                    @"IF (?<not>NOT )?LAST" +
                    Context.EndingDelimiter +
                    //Content between IF tags
                    "(?<content>.+?)" +
                    Context.StartDelimeter +
                    "/IF" +
                    Context.EndingDelimiter +
                    @"(?<end>\s*)";
            }
        }

        public override void Interpret(Context context)
        {
            var regex = new Regex(InputPattern, RegexOptions.Singleline);
            string inputString = context.Input;
            var matches = regex.Matches(inputString);
            var columns = (ColumnCollection)context.Extra;

            bool isLastColumn = (columns.Count == (columns.IndexOf((Column)Parameter) + 1));

            foreach (Match match in matches)
            {
                bool IsIfNotLast;
                string matchString = match.Value;
                if (match.Length != 0)
                {
                    string content = match.Groups["content"].ToString();
                    string endString = match.Groups["end"].ToString();
                    string replacementString = content + endString;
                    IsIfNotLast = (match.Groups["not"].ToString().Length != 0);

                    bool IsIfLast = !IsIfNotLast;
                    if (IsIfNotLast && isLastColumn)
                    {
                        ReplaceContent(matchString, string.Empty, ref inputString);
                    }
                    else if (IsIfLast && isLastColumn)
                    {
                        ReplaceContent(matchString, replacementString, ref inputString);
                    }
                    else if (IsIfNotLast && !isLastColumn)
                    {
                        ReplaceContent(matchString, replacementString, ref inputString);
                    }
                    else if (IsIfLast && !isLastColumn)
                    {
                        ReplaceContent(matchString, "", ref inputString);
                    }
                }
            }
            context.Output = inputString;
            context.Input = context.Output;
        }

        private static void ReplaceContent(string matchString, string replacementString, ref string inputString)
        {
            inputString = Regex.Replace(inputString, Regex.Escape(matchString), replacementString);
        }
    }
}