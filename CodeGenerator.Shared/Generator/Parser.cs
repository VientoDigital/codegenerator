namespace CodeGenerator.Generator;

public class Parser : Expression
{
    private readonly ICollection<Expression> expressions = new List<Expression>();
    private readonly Table table;

    public Parser(Table table)
    {
        this.table = table;
    }

    public override void AddExpression(Expression expression)
    {
        expressions.Add(expression);
    }

    public override void Interpret(Context context)
    {
        foreach (var expression in expressions)
        {
            expression.Parameter = table;
            expression.Interpret(context);
        }
    }

    public override void RemoveExpression(Expression expression)
    {
        expressions.Remove(expression);
    }
}