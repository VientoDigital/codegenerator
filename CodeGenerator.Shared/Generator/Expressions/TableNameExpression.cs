using System.Text.RegularExpressions;
using CodeGenerator.Data.Structure;
using Pluralize.NET;

namespace CodeGenerator.Generator
{
    public class TableNameExpression : Expression
    {
        private static string InputPattern => $@"TABLE.NAME\s*(?<case>CASE=(?<casing>(CAMEL|PASCAL|HUMAN|UNDERSCORE|UPPER|LOWER|HYPHEN|HYPHEN_LOWER|HYPHEN_UPPER)))?\s*(?<replace>REPLACE\((?<oldValue>(.*)),(?<newValue>(.*))\))?\s*(?<pluralization>({PLURALIZE}|{SINGULARIZE}))?".DelimeterWrap();

        public override void Interpret(Context context)
        {
            var table = (Table)Parameter;
            var regex = new Regex(InputPattern, RegexOptions.Singleline);
            string result = context.Input;
            var matches = regex.Matches(result);

            foreach (Match match in matches)
            {
                string matchValue = match.Value;
                string tableName = table.Name;

                string oldValue = match.Groups["oldValue"].Value;
                string newValue = match.Groups["newValue"].Value;
                if (!string.IsNullOrEmpty(oldValue))
                {
                    tableName = tableName.Replace(oldValue, newValue);
                }

                string casing = match.Groups["casing"].Value;
                if (!string.IsNullOrEmpty(casing))
                {
                    tableName = CaseConversion(casing, tableName);
                }

                string pluralization = match.Groups["pluralization"].Value;
                if (!string.IsNullOrEmpty(pluralization))
                {
                    var pluralizer = new Pluralizer();
                    switch (pluralization)
                    {
                        case PLURALIZE: tableName = pluralizer.Pluralize(tableName); break;
                        case SINGULARIZE: tableName = pluralizer.Singularize(tableName); break;
                        default: break;
                    }
                }

                result = Regex.Replace(result, Regex.Escape(matchValue), tableName);
            }

            context.Output = result;
            context.Input = context.Output;
        }
    }
}