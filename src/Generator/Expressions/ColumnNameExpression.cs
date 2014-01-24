using System;
using System.Text.RegularExpressions;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.Generator
{
	public class ColumnNameExpression : Expression
	{
		private const string COLUMN_NAME = "COLUMN.NAME";
		public override void Interpret(Context context)
		{			
			Column column = (Column) Parameter;
			Regex regex = new Regex(InputPattern, RegexOptions.Singleline);
			string inputString = context.Input;
			MatchCollection matches = regex.Matches(inputString);
			foreach(Match match in matches)
			{
				string matchString = match.Value;
				string naming = match.Groups["naming"].ToString();
				string replacement = column.Name;
				switch(naming)
				{
					case "CAMEL":
						replacement = CamelReplacement(column);
						break;
					case "PASCAL":
						replacement = PascalReplacement(column);
						break;
					case "LOWER":
						replacement = LowerReplacement(column);
						break;
					case "UPPER":
						replacement = UpperReplacement(column);
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
				inputString = Regex.Replace(inputString, matchString, replacement);
			}
			context.Output = inputString;
			context.Input = context.Output;
			
//			Column column = (Column)Parameter;
//			context.Output = Regex.Replace(context.Input,
//				InputPattern,
//			    column.Name);
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
			if(ignoreFirstChar && Regex.IsMatch(replacement.Substring(1),separatorString))
			{
				return replacement;
			}
			string firstChar = replacement.Substring(0, 1);
			if(!ignoreFirstChar)
				firstChar = firstChar.ToUpper();
			replacement = firstChar + replacement.Substring(1).Replace("_", String.Empty);
			MatchCollection minMay = Regex.Matches(replacement, "(?<min>[a-z])(?<may>[A-Z])");
			foreach(Match mm in minMay)
			{
				replacement =
					Regex.Replace(replacement, mm.Groups["min"].Value + mm.Groups["may"].Value, mm.Groups["min"].Value + separatorString + mm.Groups["may"].Value);
			}
			return replacement;
		}

		private static string UpperReplacement(Column column)
		{
			string replacement;
			replacement = column.Name.ToUpper();
			return replacement;
		}

		private static string LowerReplacement(Column column)
		{
			string replacement;
			replacement = column.Name.ToLower();
			return replacement;
		}

		private static string PascalReplacement(Column column)
		{
			string replacement;
			replacement = column.Name.Replace("_",String.Empty);
			replacement = replacement.Substring(0, 1).ToUpper() + replacement.Substring(1);
			return replacement;
		}

		private static string CamelReplacement(Column column)
		{
			string replacement;
			replacement = column.Name.Replace("_",String.Empty);
			replacement = replacement.Substring(0, 1).ToLower() + replacement.Substring(1);
			return replacement;
		}

		private static string InputPattern
		{
			get
			{
				return Context.StartDelimeter + 
			                COLUMN_NAME + 
							@"\s*" +
			                @"(?<naming>(CAMEL|PASCAL|HUMAN|UNDERSCORE|UPPER|LOWER))*"+ 
							Context.EndingDelimiter;
			}
		}
	}
}
