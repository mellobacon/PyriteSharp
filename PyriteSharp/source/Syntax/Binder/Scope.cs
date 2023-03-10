namespace PyriteSharp.source.Syntax.Binder;

public class Scope
{
    private readonly Dictionary<string, object?> _variables = new();

    public bool AddVariable(Variable v)
    {
        return _variables.TryAdd(v.Name, null);
    }

    public object? GetVariable(string name)
    {
        if (_variables.TryGetValue(name, out object? value))
        {
            return value;
        }

        return null;
    }

    public Dictionary<string, object?> GetVariables()
    {
        return _variables;
    }
}