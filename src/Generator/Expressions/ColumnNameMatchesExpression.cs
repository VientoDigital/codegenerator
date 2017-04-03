using System.Text.RegularExpressions;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.Generator
{
    /// <summary>
    /// Summary description for ColumnNameMatchesExpression.
    /// </summary>
    public class ColumnNameMatchesExpression : Expression
    {
        public override void Interpret(Context context)
        {
            Column column = (Column)Parameter;
            Regex regex = new Regex(InputPattern, RegexOptions.Singleline);
            string inputString = context.Input;
            MatchCollection matches = regex.Matches(inputString);
            foreach (Match match in matches)
            {
                if (match.Length == 0)
                    continue;
                bool isEqual = (match.Groups["equality"].ToString().IndexOf("=~") != -1);
                bool isNotEqual = (match.Groups["equality"].ToString().IndexOf("!~") != -1);
                string contentString = match.Groups["content"].ToString();
                string regularExp = match.Groups["regularExp"].ToString();
                string endString = match.Groups["end"].ToString();
                string replacementString = contentString + endString;
                bool isAMatch = false;

                if (isEqual && Regex.IsMatch(column.Name, regularExp))
                {
                    isAMatch = true;
                    ReplaceContent(match.Value, replacementString, ref inputString);
                }
                else if (isNotEqual && !Regex.IsMatch(column.Name, regularExp))
                {
                    isAMatch = true;
                    ReplaceContent(match.Value, replacementString, ref inputString);
                }

                if (!isAMatch)
                {
                    ReplaceContent(match.Value, "", ref inputString);
                }
            }

            context.Output = inputString;
            context.Input = context.Output;
        }

        private static void ReplaceContent(string matchString, string replacementString, ref string inputString)
        {
            inputString = Regex.Replace(inputString, Regex.Escape(matchString), replacementString);
        }

        private static string InputPattern
        {
            get
            {
                return @"\s*" +
                    Context.StartDelimeter +
                    @"IF COLUMN.NAME\s+(?<equality>(=~|!~))\s+'(?<regularExp>[a-zA-Z0-9_\*\+\.\^\$)(|]+)'" +
                       Context.EndingDelimiter +
                    //Content between IF tags
                    "(?<content>.+?)" +
                    Context.StartDelimeter +
                    "/IF" +
                    Context.EndingDelimiter +
                    @"(?<end>\s*)";
            }
        }
    }
}