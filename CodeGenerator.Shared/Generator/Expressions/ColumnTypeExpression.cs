namespace CodeGenerator.Generator;

public class ColumnTypeExpression : Expression
{
    public override void Interpret(Context context)
    {
        var column = (Column)Parameter;
        context.Output = Regex.Replace(context.Input, "COLUMN.TYPE".DelimeterWrap(), column.NativeType, RegexOptions.Singleline | RegexOptions.IgnoreCase);
        context.Input = context.Output;
    }
}