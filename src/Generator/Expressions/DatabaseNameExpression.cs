using System;
using System.Text.RegularExpressions;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.Generator
{
	public class DatabaseNameExpression : Expression
	{
		public DatabaseNameExpression()
		{
		}

		public override void Interpret(Context context)
		{
			Database database = ((Table)Parameter).ParentDatabase;
			context.Output = Regex.Replace(context.Input,Context.StartDelimeter + "DATABASE.NAME" + Context.EndingDelimiter,database.Name);
			context.Input = context.Output;
		}
	}
}
