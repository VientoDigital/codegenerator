namespace CodeGenerator.Generator;

/// <summary>
/// Summary description for ColumnNameMatchesExpression.
/// </summary>
public class ColumnNameMatchesExpression : Expression
{
    private static string InputPattern
    {
        get
        {
            return @"\s*" +
                @"IF COLUMN.NAME\s+(?<equality>(=~|!~))\s+'(?<regularExp>[a-zA-Z0-9_\*\+\.\^\$)(|]+)'".DelimeterWrap() +
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

            bool isEqual = (match.Groups["equality"].Value.IndexOf("=~") != -1);
            bool isNotEqual = (match.Groups["equality"].Value.IndexOf("!~") != -1);
            string contentString = match.Groups["content"].Value;
            string regularExp = match.Groups["regularExp"].Value;
            string endString = match.Groups["end"].Value;
            string replacementString = contentString + endString;
            bool isAMatch = false;

            if (isEqual && Regex.IsMatch(column.Name, regularExp))
            {
                isAMatch = true;
                ReplaceContent(match.Value, replacementString, ref result);
            }
            else if (isNotEqual && !Regex.IsMatch(column.Name, regularExp))
            {
                isAMatch = true;
                ReplaceContent(match.Value, replacementString, ref result);
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