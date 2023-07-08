namespace CodeGenerator.Generator;

public class ColumnIfNullableExpression : Expression
{
    private static string InputPattern
    {
        get
        {
            return @"\s*" +
                @"IF COLUMN.NULLCHECK (?<not>NOT )?NULLABLE".DelimeterWrap() +
                //Content between IF tags
                "(?<content>.+?)" +
                "/IF COLUMN.NULLCHECK".DelimeterWrap() +
                @"(?<end>\s*)";
        }
    }

    public override void Interpret(Context context)
    {
        var column = (Column)Parameter;
        string result = context.Input;
        var regex = new Regex(InputPattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
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