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
                    @"IF COLUMN.TYPE\s+(?<equality>(NE|EQ))\s+('|‘)(?<typeValue>[a-zA-Z0-9_)(|]+)('|’)".DelimeterWrap() +
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

                bool isEqual = (match.Groups["equality"].Value.IndexOf("EQ") != -1);
                bool isNotEqual = (match.Groups["equality"].Value.IndexOf("NE") != -1);
                string contentString = match.Groups["content"].Value;
                string typeValueString = match.Groups["typeValue"].Value;
                string endString = match.Groups["end"].Value;
                string replacementString = contentString + endString;

                bool isAMatch = false;
                //				Console.WriteLine(typeValueString);
                string[] valueStrings = typeValueString.Split('|');
                for (int i = 0; i < valueStrings.Length; i++)
                {
                    valueStrings[i] = valueStrings[i].Trim();
                    if (isEqual && (column.NativeType.ToLower() == valueStrings[i].ToLower()))
                    {
                        ReplaceContent(match.Value, replacementString, ref result);
                        isAMatch = true;
                        break;
                    }
                    // TODO: Possible bug to fix (https://github.com/VientoDigital/codegenerator/issues/2)
                    else if (isNotEqual && (column.NativeType.ToLower() != valueStrings[i].ToLower()))
                    {
                        ReplaceContent(match.Value, replacementString, ref result);
                        isAMatch = true;
                        break;
                    }
                }
                if (!isAMatch)
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