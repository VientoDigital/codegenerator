using System;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeGenerator.Generator
{
    public abstract class Expression
    {
        internal object Parameter { get; set; }

        public static string CaseConversion(string casing, string replacement, string name)
        {
            switch (casing)
            {
                case "CAMEL": replacement = CamelReplacement(name); break;
                case "PASCAL": replacement = PascalReplacement(name); break;
                case "LOWER": replacement = LowerReplacement(name); break;
                case "UPPER": replacement = UpperReplacement(name); break;
                case "UNDERSCORE": replacement = UnderscoreReplacement(replacement); break;
                case "HUMAN": replacement = HumanReplacement(replacement); break;
                case "HYPHEN": replacement = HyphenReplacement(replacement); break;
                case "HYPHEN_LOWER": replacement = LowerReplacement(HyphenReplacement(replacement)); break;
                case "HYPHEN_UPPER": replacement = UpperReplacement(HyphenReplacement(replacement)); break;
                default: break;
            }
            return replacement;
        }

        public virtual void AddExpression(Expression expression)
        {
            throw new NotImplementedException();
        }

        public abstract void Interpret(Context context);

        public virtual void RemoveExpression(Expression expression)
        {
            throw new NotImplementedException();
        }

        private static string CamelReplacement(string name)
        {
            var replacement = name.Replace("_", string.Empty);
            replacement = replacement.Substring(0, 1).ToLower() + replacement.Substring(1);
            return replacement;
        }

        private static string HumanReplacement(string replacement) => SeparatorReplacement(replacement, " ", false);

        private static string HyphenReplacement(string replacement) => SeparatorReplacement(replacement, "-", true);

        private static string LowerReplacement(string name)
        {
            return name.ToLower();
        }

        private static string PascalReplacement(string name)
        {
            var replacement = name.Replace("_", string.Empty);
            replacement = replacement.Substring(0, 1).ToUpper() + replacement.Substring(1);
            return replacement;
        }

        private static string SeparatorReplacement(string replacement, string separator, bool ignoreFirstChar)
        {
            if (ignoreFirstChar && Regex.IsMatch(replacement.Substring(1), separator))
            {
                return replacement;
            }

            string firstChar = replacement.Substring(0, 1);
            if (!ignoreFirstChar)
            {
                firstChar = firstChar.ToUpper();
            }

            replacement = firstChar + replacement.Substring(1).Replace("_", string.Empty);
            var matches = Regex.Matches(replacement, "(?<min>[a-z])(?<may>[A-Z])");
            foreach (Match match in matches)
            {
                replacement = Regex.Replace(
                    replacement,
                    $"{match.Groups["min"].Value}{match.Groups["may"].Value}",
                    $"{match.Groups["min"].Value}{separator}{match.Groups["may"].Value}");
            }
            return replacement;
        }

        private static string UnderscoreReplacement(string replacement) => SeparatorReplacement(replacement, "_", true);

        private static string UpperReplacement(string name)
        {
            return name.ToUpper();
        }
    }
}