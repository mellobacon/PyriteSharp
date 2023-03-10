namespace PyriteSharp.source.Syntax.Binder;

public class Scope
{
    private readonly Dictionary<Variable, object?> _variables = new();

    public bool AddVariable(Variable v)
    {
        return _variables.TryAdd(v, null);
    }

    public Dictionary<Variable, object?> GetVariables()
    {
        return _variables;
    }
}