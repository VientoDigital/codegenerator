using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.Generator
{
	public abstract class Expression
	{
		public abstract void Interpret(Context context);
		public virtual void AddExpression(Expression expression)
		{
			throw new NotImplementedException();
		}
		public virtual void RemoveExpression(Expression expression)
		{
			throw new NotImplementedException();
		}
		private object _parameter;
		
		internal object Parameter
		{
			set { _parameter = value; }
			get { return _parameter; }
		}

        public static string CaseConvertion(string naming, string replacement, string name)
        {
            switch (naming)
            {
                case "CAMEL":
                    replacement = CamelReplacement(name);
                    break;
                case "PASCAL":
                    replacement = PascalReplacement(name);
                    break;
                case "LOWER":
                    replacement = LowerReplacement(name);
                    break;
                case "UPPER":
                    replacement = UpperReplacement(name);
                    break;
                case "UNDERSCORE":
                    replacement = UnderscoreReplacement(replacement);
                    break;
                case "HUMAN":
                    replacement = HumanReplacement(replacement);
                    break;
                default:
                    break;
            }
            return replacement;
        }

        private static string UnderscoreReplacement(string replacement)
        {
            return SeparatorReplacement(replacement, "_", true);
        }

        private static string HumanReplacement(string replacement)
        {
            return SeparatorReplacement(replacement, " ", false);
        }

        private static string SeparatorReplacement(string replacement, string separatorString, bool ignoreFirstChar)
        {
            if (ignoreFirstChar && Regex.IsMatch(replacement.Substring(1), separatorString))
            {
                return replacement;
            }
            string firstChar = replacement.Substring(0, 1);
            if (!ignoreFirstChar)
                firstChar = firstChar.ToUpper();
            replacement = firstChar + replacement.Substring(1).Replace("_", String.Empty);
            var minMay = Regex.Matches(replacement, "(?<min>[a-z])(?<may>[A-Z])");
            foreach (Match mm in minMay)
            {
                replacement =
                    Regex.Replace(replacement, mm.Groups["min"].Value + mm.Groups["may"].Value, mm.Groups["min"].Value + separatorString + mm.Groups["may"].Value);
            }
            return replacement;
        }

        private static string UpperReplacement(string name)
        {
            return name.ToUpper();
        }

	    private static string LowerReplacement(string name)
        {
	        return name.ToLower();
        }

	    private static string PascalReplacement(string name)
        {
            var replacement = name.Replace("_", String.Empty);
            replacement = replacement.Substring(0, 1).ToUpper() + replacement.Substring(1);
            return replacement;
        }

        private static string CamelReplacement(string name)
        {
            var replacement = name.Replace("_", String.Empty);
            replacement = replacement.Substring(0, 1).ToLower() + replacement.Substring(1);
            return replacement;
        }
	}
}
