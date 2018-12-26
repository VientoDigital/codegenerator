using System.Text.RegularExpressions;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.Generator
{
    public class TableNameExpression : Expression
    {
        private const string TABLE_NAME = "TABLE.NAME";

        public override void Interpret(Context context)
        {
            var table = (Table)Parameter;
            var regex = new Regex(InputPattern, RegexOptions.Singleline);
            var inputString = context.Input;
            var matches = regex.Matches(inputString);
            foreach (Match match in matches)
            {
                var matchString = match.Value;
                var naming = match.Groups["naming"].ToString();
                var replacement = table.Name;
                replacement = CaseConversion(naming, replacement, table.Name);
                inputString = Regex.Replace(inputString, matchString, replacement);
            }
            context.Output = inputString;
            context.Input = context.Output;
        }

        private static string InputPattern
        {
            get
            {
                return Context.StartDelimeter +
                    TABLE_NAME +
                    @"\s*" +
                    @"(?<naming>(CAMEL|PASCAL|HUMAN|UNDERSCORE|UPPER|LOWER|HYPHEN|HYPHEN_LOWER|HYPHEN_UPPER))*" +
                    Context.EndingDelimiter;
            }
        }
    }
}