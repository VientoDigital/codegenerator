using System.Text.RegularExpressions;

namespace iCodeGenerator.Generator
{
	public class LiteralExpression : Expression
	{
		private readonly string _strKey;
		private readonly string _strValue;
		public LiteralExpression(string strKey,string strValue)
		{
			_strKey = strKey;
			_strValue = strValue;
		}

		public override void Interpret(Context context)
		{
			context.Output = Regex.Replace(context.Input,Context.StartDelimeter + _strKey + Context.EndingDelimiter,_strValue);
			context.Input = context.Output;
            var inputPattern = Context.StartDelimeter + _strKey + @"\s*" +
			                @"(?<naming>(CAMEL|PASCAL|HUMAN|UNDERSCORE|UPPER|LOWER))*"+ 
							Context.EndingDelimiter;

			var regex = new Regex(inputPattern, RegexOptions.Singleline);
			var inputString = context.Input;
			var matches = regex.Matches(inputString);
			foreach(Match match in matches)
			{
				var matchString = match.Value;
				var naming = match.Groups["naming"].ToString();
				var replacement = _strValue;
				replacement = CaseConvertion(naming, replacement, _strValue);
				inputString = Regex.Replace(inputString, matchString, replacement);
			}
			context.Output = inputString;
			context.Input = context.Output;
		}

	}
}
