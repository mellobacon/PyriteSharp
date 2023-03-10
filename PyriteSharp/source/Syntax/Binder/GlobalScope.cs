namespace PyriteSharp.source.Syntax.Binder;

public struct GlobalScope
{
    public readonly BoundStatement Statement;
    public readonly Dictionary<string, object?> Variables;

    public GlobalScope(BoundStatement e, Scope scope)
    {
        Statement = e;
        Variables = scope.GetVariables();
    }
}