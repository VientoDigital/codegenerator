using System;
using System.Text.RegularExpressions;
using Extenso;

namespace CodeGenerator.Generator
{
    public abstract class Expression
    {
        protected const string PLURALIZE = "PLURALIZE";
        protected const string SINGULARIZE = "SINGULARIZE";

        internal object Parameter { get; set; }

        public static string CaseConversion(string casing, string name) => casing switch
        {
            "CAMEL" => name.ToCamelCase(),
            "PASCAL" => name.ToPascalCase(),
            "LOWER" => name.ToLower(),
            "UPPER" => name.ToUpper(),
            "UNDERSCORE" => Separate(name, "_", false),
            "HUMAN" => name.SplitPascal().ToTitleCase(),
            "HYPHEN" => Separate(name, "-", false),
            "HYPHEN_LOWER" => (Separate(name, "-", false)).ToLower(),
            "HYPHEN_UPPER" => (Separate(name, "-", false)).ToUpper(),
            _ => name,
        };

        public abstract void Interpret(Context context);

        public virtual void AddExpression(Expression expression)
        {
            throw new NotImplementedException();
        }

        public virtual void RemoveExpression(Expression expression)
        {
            throw new NotImplementedException();
        }

        private static string Separate(string value, string separator, bool capitalizeFirstChar)
        {
            if (!capitalizeFirstChar && Regex.IsMatch(value[1..], separator))
            {
                return value;
            }

            string firstChar = value[..1];
            if (capitalizeFirstChar)
            {
                firstChar = firstChar.ToUpper();
            }

            value = firstChar + value[1..].Replace("_", string.Empty);
            var matches = Regex.Matches(value, "(?<min>[a-z])(?<may>[A-Z])");

            foreach (Match match in matches)
            {
                value = Regex.Replace(
                    value,
                    $"{match.Groups["min"].Value}{match.Groups["may"].Value}",
                    $"{match.Groups["min"].Value}{separator}{match.Groups["may"].Value}");
            }
            return value;
        }
    }
}