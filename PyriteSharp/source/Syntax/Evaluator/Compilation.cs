using PyriteSharp.source.Syntax.Binder;
using PyriteSharp.source.Syntax.Parser.Expressions;

namespace PyriteSharp.source.Syntax.Evaluator;

public class Compilation
{
    private readonly Expression _root;
    public Compilation(Expression expression)
    {
        _root = expression;
    }

    public object? Evaluate()
    {
        Binder.Binder binder = new Binder.Binder();
        BoundExpression expression = binder.BindExpression(_root);
        Evaluator evaluator = new Evaluator(expression);
        return evaluator.Evaluate();
    }
}