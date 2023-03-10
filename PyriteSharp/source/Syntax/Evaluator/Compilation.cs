using PyriteSharp.source.Syntax.Binder;
using PyriteSharp.source.Syntax.Parser.Expressions;

namespace PyriteSharp.source.Syntax.Evaluator;

public class Compilation
{
    private readonly Statement _root;
    public Compilation(Statement expression)
    {
        _root = expression;
    }

    public object? Evaluate()
    {
        GlobalScope scope = Binder.Binder.BindScope(_root);
        Evaluator evaluator = new Evaluator(scope.Statement, scope.Variables);
        return evaluator.Evaluate();
    }
}