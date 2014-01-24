using System.Text.RegularExpressions;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.Generator
{
	public class ColumnIfNullableExpression : Expression
	{
		public ColumnIfNullableExpression()
		{
		}

		private static string InputPattern
		{
			get
			{
				return @"\s*" + 
					Context.StartDelimeter +
					@"IF (?<not>NOT )?COLUMN.NULLABLE" + 
					Context.EndingDelimiter +
					//Content between IF tags
					"(?<content>.+?)" + 
					Context.StartDelimeter +
					"/IF" + 
					Context.EndingDelimiter +
					@"(?<end>\s*)";
			}
		}

		public override void Interpret(Context context)
		{			
			Column column = (Column)Parameter;

			Regex regex = new Regex(InputPattern,RegexOptions.Singleline);
			string inputString = context.Input;
			MatchCollection matches = regex.Matches(inputString);
			foreach(Match match in matches)
			{
				if(match.Length == 0)
				{
					continue;
				}
				string matchString = match.Value;
				string contentString = match.Groups["content"].ToString();
				string endString = match.Groups["end"].ToString();
				string replacementString = contentString + endString;
				bool isIfNullable = !( match.Groups["not"].ToString().IndexOf("NOT") != -1);
				bool isIfNotNullable = !isIfNullable;
				if(isIfNotNullable && !column.Nullable)
				{
					ReplaceContent(matchString,replacementString,ref inputString);
				}
				else if(isIfNullable && column.Nullable)
				{
					ReplaceContent(matchString,replacementString,ref inputString);
				}
				else
				{
					ReplaceContent(match.Value, "", ref inputString);
				}
				context.Output = inputString;
				context.Input = context.Output;
			}
		}

		private static void ReplaceContent(string matchString, string replacementString, ref string inputString)
		{
//			inputString = Regex.Replace(inputString,matchString,replacementString);
			inputString = Regex.Replace(inputString,Regex.Escape( matchString ),replacementString);
		}
	}
}
