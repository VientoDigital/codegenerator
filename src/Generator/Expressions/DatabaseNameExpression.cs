using System;
using System.Text.RegularExpressions;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.Generator
{
	public class DatabaseNameExpression : Expression
	{
        private const string DATABASE_NAME = "DATABASE.NAME";
        public override void Interpret(Context context)
        {

            Database database = ((Table)Parameter).ParentDatabase;
            var regex = new Regex(InputPattern, RegexOptions.Singleline);
            var inputString = context.Input;
            var matches = regex.Matches(inputString);
            foreach (Match match in matches)
            {
                var matchString = match.Value;
                var naming = match.Groups["naming"].ToString();
                var replacement = database.Name;
                replacement = Expression.CaseConvertion(naming, replacement, database.Name);
                inputString = Regex.Replace(inputString, matchString, replacement);
            }
            context.Output = inputString;
            context.Input = context.Output;
        }

        private static string InputPattern
        {
            get
            {
                return Context.StartDelimeter +
                            DATABASE_NAME +
                            @"\s*" +
                            @"(?<naming>(CAMEL|PASCAL|HUMAN|UNDERSCORE|UPPER|LOWER))*" +
                            Context.EndingDelimiter;
            }
        }
	}
}
