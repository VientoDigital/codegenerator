namespace CodeGenerator.Generator;

public class ColumnDefaultExpression : Expression
{
    public override void Interpret(Context context)
    {
        var column = (Column)Parameter;

        if (column.Default != null)
        {
            context.Output = Regex.Replace(context.Input, "COLUMN.DEFAULT".DelimeterWrap(), column.Default, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            context.Input = context.Output;
        }
    }
}