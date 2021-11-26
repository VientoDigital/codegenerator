using System;
using CodeGenerator.Extensions;
using Humanizer;

namespace CodeGenerator.Generator
{
    public abstract class Expression
    {
        internal object Parameter { get; set; }

        public static string CaseConversion(string casing, string name) => casing switch
        {
            "CAMEL" => name.Camelize(),
            "PASCAL" => name.Pascalize(),
            "LOWER" => name.ToLower(),
            "UPPER" => name.ToUpper(),
            "UNDERSCORE" => name.UnderscoreNoCaseChange(),
            "HUMAN" => name.Humanize().Titleize(),
            "HYPHEN" => name.KebaberizeNoCaseChange(),
            "HYPHEN_LOWER" => name.Kebaberize(),
            "HYPHEN_UPPER" => name.Kebaberize().ToUpper(),
            _ => name,
        };

        public abstract void Interpret(Context context);

        public virtual void AddExpression(Expression expression)
        {
            throw new NotImplementedException();
        }

        public virtual void RemoveExpression(Expression expression)
        {
            throw new NotImplementedException();
        }
    }
}