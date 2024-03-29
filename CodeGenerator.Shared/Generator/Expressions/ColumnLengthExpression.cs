namespace CodeGenerator.Generator;

public class ColumnLengthExpression : Expression
{
    public override void Interpret(Context context)
    {
        var column = (Column)Parameter;
        context.Output = Regex.Replace(context.Input, "COLUMN.LENGTH".DelimeterWrap(), column.Length.ToString(), RegexOptions.Singleline | RegexOptions.IgnoreCase);
        context.Input = context.Output;
    }
}