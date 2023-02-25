namespace PyriteSharp.source.Syntax.Binder;

public abstract class BoundExpression : BoundNode
{
    public abstract Type ValueType { get; }
}