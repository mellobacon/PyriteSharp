namespace PyriteSharp.source.Syntax.Binder;

public struct GlobalScope
{
    public BoundStatement Expression;
    public readonly Dictionary<string, object?> Variables;

    public GlobalScope(BoundStatement e, Scope scope)
    {
        Expression = e;
        Variables = scope.GetVariables();
    }
}