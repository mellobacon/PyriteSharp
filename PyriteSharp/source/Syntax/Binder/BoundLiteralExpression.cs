namespace PyriteSharp.source.Syntax.Binder;

public class BoundLiteralExpression : BoundExpression
{
    public readonly object? Value;
    public BoundLiteralExpression(object? value)
    {
        Value = value;
    }

    public override Type ValueType => Value!.GetType();
    public override BoundType BoundType => BoundType.LITERAL;
}