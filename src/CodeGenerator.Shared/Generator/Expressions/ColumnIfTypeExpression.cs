using System.Text.RegularExpressions;
using CodeGenerator.Data.Structure;

namespace CodeGenerator.Generator
{
    public class ColumnIfTypeExpression : Expression
    {
        private static string InputPattern
        {
            get
            {
                return @"\s*" +
                    Context.StartDelimeter +
                    @"IF COLUMN.TYPE\s+(?<equality>(NE|EQ))\s+'(?<typeValue>[a-zA-Z0-9_)(|]+)'" +
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

                bool isEqual = (match.Groups["equality"].ToString().IndexOf("EQ") != -1);
                bool isNotEqual = (match.Groups["equality"].ToString().IndexOf("NE") != -1);
                string contentString = match.Groups["content"].ToString();
                string typeValueString = match.Groups["typeValue"].ToString();
                string endString = match.Groups["end"].ToString();
                string replacementString = contentString + endString;

                bool isAMatch = false;
                //				Console.WriteLine(typeValueString);
                string[] valueStrings = typeValueString.Split('|');
                for (int i = 0; i < valueStrings.Length; i++)
                {
                    valueStrings[i] = valueStrings[i].Trim();
                    if (isEqual && (column.Type.ToLower() == valueStrings[i].ToLower()))
                    {
                        ReplaceContent(match.Value, replacementString, ref inputString);
                        isAMatch = true;
                        break;
                    }
                    else if (isNotEqual && (column.Type.ToLower() != valueStrings[i].ToLower()))
                    {
                        ReplaceContent(match.Value, replacementString, ref inputString);
                        isAMatch = true;
                        break;
                    }
                }
                if (!isAMatch)
                {
                    ReplaceContent(match.Value, string.Empty, ref inputString);
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