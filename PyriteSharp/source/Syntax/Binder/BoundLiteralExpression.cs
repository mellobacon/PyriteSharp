namespace PyriteSharp.source.Syntax.Binder;

public class BoundLiteralExpression : BoundExpression
{
    public object? value;
    public BoundLiteralExpression(object? value)
    {
        this.value = value;
    }

    public override Type ValueType => value!.GetType();
    public override BoundType BoundType => BoundType.LITERAL;
}