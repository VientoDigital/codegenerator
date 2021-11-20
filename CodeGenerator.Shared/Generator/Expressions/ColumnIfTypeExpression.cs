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
                    @"IF COLUMN.TYPE\s+(?<equality>(NE|EQ))\s+('|‘)(?<types>[ a-zA-Z0-9_)(|]+)('|’)".DelimeterWrap() +
                    //Content between IF tags
                    "(?<content>.+?)" +
                    "/IF".DelimeterWrap() +
                    @"(?<end>\s*)";
            }
        }

        public override void Interpret(Context context)
        {
            var column = (Column)Parameter;
            var regex = new Regex(InputPattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            string result = context.Input;
            var matches = regex.Matches(result);

            foreach (Match match in matches)
            {
                if (match.Length == 0)
                {
                    continue;
                }

                bool hasEQ = (match.Groups["equality"].Value.IndexOf("EQ") != -1);
                bool hasNE = (match.Groups["equality"].Value.IndexOf("NE") != -1);
                string content = match.Groups["content"].Value;
                string[] specifiedTypes = match.Groups["types"].Value.Split('|');
                string end = match.Groups["end"].Value;
                string replacement = content + end;

                bool isMatch = false;
                for (int i = 0; i < specifiedTypes.Length; i++)
                {
                    string type = specifiedTypes[i].ToLowerInvariant().Trim();
                    if (hasEQ && (column.NativeType.ToLower() == type)) // if EQ && the column's type == that specified type
                    {
                        ReplaceContent(match.Value, replacement, ref result);
                        isMatch = true;
                        break;
                    }
                    else if (hasNE && (column.NativeType.ToLower() != type)) // if NE && the column's type != that specified type
                    {
                        ReplaceContent(match.Value, replacement, ref result);
                        isMatch = true;
                        break;
                    }
                }
                if (!isMatch)
                {
                    ReplaceContent(match.Value, string.Empty, ref result);
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