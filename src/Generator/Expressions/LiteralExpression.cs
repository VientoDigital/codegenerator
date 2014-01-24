using System.Text.RegularExpressions;

namespace iCodeGenerator.Generator
{
	public class LiteralExpression : Expression
	{
		private string _strKey;
		private string _strValue;
		public LiteralExpression(string strKey,string strValue)
		{
			_strKey = strKey;
			_strValue = strValue;
		}

		public override void Interpret(Context context)
		{
			context.Output = Regex.Replace(context.Input,Context.StartDelimeter + _strKey + Context.EndingDelimiter,_strValue);
			context.Input = context.Output;
		}
	}
}
