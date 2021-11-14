using System.Text.RegularExpressions;
using CodeGenerator.Data.Structure;
using CodeGenerator.Shared.Extensions;

namespace CodeGenerator.Generator
{
    public class TableNameExpression : Expression
    {
        private const string TABLE_NAME = "TABLE.NAME";

        private static string InputPattern => $@"{TABLE_NAME}\s*(?<casing>(CAMEL|PASCAL|HUMAN|UNDERSCORE|UPPER|LOWER|HYPHEN|HYPHEN_LOWER|HYPHEN_UPPER))*".DelimeterWrap();

        public override void Interpret(Context context)
        {
            var table = (Table)Parameter;
            var regex = new Regex(InputPattern, RegexOptions.Singleline);
            string input = context.Input;
            var matches = regex.Matches(input);

            foreach (Match match in matches)
            {
                string matchValue = match.Value;
                string casing = match.Groups["casing"].ToString();
                string tableName = table.Name;
                tableName = CaseConversion(casing, tableName, table.Name);
                input = Regex.Replace(input, matchValue, tableName);
            }

            context.Output = input;
            context.Input = context.Output;
        }
    }
}