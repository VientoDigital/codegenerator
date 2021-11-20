using System.Linq;
using System.Text.RegularExpressions;
using CodeGenerator.Data.Structure;
using Extenso.Collections;
using Pluralize.NET;

namespace CodeGenerator.Generator
{
    public class TableNameExpression : Expression
    {
        private static readonly Regex tableNameRegex = new(
            @"TABLE.NAME(?<options>(?:(?!{).)*)".DelimeterWrap(),
            RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static readonly Regex optionsRegex = new(
            @"OPTIONS\s*(CASE=(?<casing>(CAMEL|PASCAL|HUMAN|UNDERSCORE|UPPER|LOWER|HYPHEN_LOWER|HYPHEN_UPPER|HYPHEN)))?\s*(?<replace>REPLACE\((?<oldValue>(.*)),(?<newValue>(.*))\))?\s*(?<pluralization>(PLURALIZE|SINGULARIZE))?",
            RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public override void Interpret(Context context)
        {
            var table = (Table)Parameter;
            string result = context.Input;
            var matches = tableNameRegex.Matches(result).DistinctBy(x => x.Value); // No need to run Regex.Replace() on same instance more than once..

            foreach (Match match in matches)
            {
                string matchValue = match.Value;
                string tableName = table.Name;

                var optionMatches = optionsRegex.Matches(matchValue);
                if (optionMatches.IsNullOrEmpty())
                {
                    // No options.. so just run table name replace only..
                    result = Regex.Replace(result, Regex.Escape(matchValue), tableName);
                    continue;
                }

                var optionsMatch = optionMatches.OfType<Match>().FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.Value));
                if (optionsMatch != null && optionsMatch.Success)
                {
                    string oldValue = optionsMatch.Groups["oldValue"].Value;
                    string newValue = optionsMatch.Groups["newValue"].Value;
                    if (!string.IsNullOrEmpty(oldValue))
                    {
                        tableName = tableName.Replace(oldValue, newValue);
                    }

                    string casing = optionsMatch.Groups["casing"].Value;
                    if (!string.IsNullOrEmpty(casing))
                    {
                        tableName = CaseConversion(casing, tableName);
                    }

                    string pluralization = optionsMatch.Groups["pluralization"].Value;
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
            }

            context.Output = result;
            context.Input = context.Output;
        }
    }
}