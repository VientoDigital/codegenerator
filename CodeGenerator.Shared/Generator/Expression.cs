using System;
using System.Text.RegularExpressions;
using Extenso;

namespace CodeGenerator.Generator
{
    public abstract class Expression
    {
        internal object Parameter { get; set; }

        public static string CaseConversion(string casing, string value, string name)
        {
            switch (casing)
            {
                case "CAMEL": value = name.ToCamelCase(); break;
                case "PASCAL": value = name.ToPascalCase(); break;
                case "LOWER": value = name.ToLower(); break;
                case "UPPER": value = name.ToUpper(); break;
                case "UNDERSCORE": value = Separate(value, "_", false); break;
                case "HUMAN": value = value.ToTitleCase(); break;
                case "HYPHEN": value = Separate(value, "-", false); break;
                case "HYPHEN_LOWER": value = (Separate(value, "-", false)).ToLower(); break;
                case "HYPHEN_UPPER": value = (Separate(value, "-", false)).ToUpper(); break;
                default: break;
            }
            return value;
        }

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