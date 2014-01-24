using System.Text.RegularExpressions;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.Generator
{
	public class ColumnDefaultExpression : Expression
	{
		public ColumnDefaultExpression()
		{
			
		}

		public override void Interpret(Context context)
		{
			Column column = (Column)Parameter;
			context.Output = Regex.Replace(context.Input,Context.StartDelimeter + "COLUMN.DEFAULT" + Context.EndingDelimiter,column.Default);
			context.Input = context.Output;
		}
	}
}
