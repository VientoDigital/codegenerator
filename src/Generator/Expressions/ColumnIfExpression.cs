using System.Text.RegularExpressions;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.Generator
{
	public class ColumnIfExpression : Expression
	{
		public override void Interpret(Context context)
		{	
			Regex regex = new Regex(InputPattern,RegexOptions.Singleline);
			string inputString = context.Input;
			MatchCollection matches = regex.Matches(inputString);
			ColumnCollection columns = (ColumnCollection) context.Extra;
			bool IsLastColumn = (columns.Count == (columns.IndexOf((Column)Parameter) + 1));

			foreach(Match match in matches)
			{
				bool IsIfNotLast;
				string matchString = match.Value;
				if(match.Length != 0)
				{
					string content = "";
					string endString = "";
					content = match.Groups["content"].ToString();
					endString = match.Groups["end"].ToString();
					string replacementString = content + endString;
					IsIfNotLast = (match.Groups["not"].ToString().Length != 0);
					bool IsIfLast = !IsIfNotLast;
					if( IsIfNotLast && IsLastColumn )
					{
						ReplaceContent(matchString, "", ref inputString);
					}
					else if( IsIfLast && IsLastColumn )
					{
						ReplaceContent(matchString, replacementString, ref inputString);
					}
					else if( IsIfNotLast && !IsLastColumn )
					{
						ReplaceContent(matchString, replacementString, ref inputString);
					}
					else if( IsIfLast && !IsLastColumn )
					{
						ReplaceContent(matchString, "", ref inputString);
					}
				}	
			}			
			context.Output = inputString;
			context.Input = context.Output;
		}

		private static void ReplaceContent(string matchString, string replacementString, ref string inputString)
		{
			inputString = Regex.Replace(inputString,Regex.Escape( matchString ),replacementString);
		}

		private static string InputPattern
		{
			get
			{
				return @"\s*" + 
					Context.StartDelimeter +
					@"IF (?<not>NOT )?LAST" + 
					Context.EndingDelimiter +
					//Content between IF tags
					"(?<content>.+?)" + 
					Context.StartDelimeter +
					"/IF" + 
					Context.EndingDelimiter +
					@"(?<end>\s*)";
			}
		}

	}
}
