using PyriteSharp.source.Syntax.Binder;

namespace PyriteSharp.source.Syntax.Evaluator;

public class Evaluator
{
    public object? value;
    private readonly BoundExpression _root;

    public Evaluator(BoundExpression expression)
    {
        _root = expression;
    }

    public object? Evaluate()
    {
        value = EvaluateExpression(_root);
        return value;
    }

    private object? EvaluateExpression(BoundExpression expression)
    {
        switch (expression.BoundType)
        {
            case BoundType.BINARY:
                return EvaluateBinaryExpression((BoundBinaryExpression)expression);
            case BoundType.LITERAL:
                return EvaluateLiteral((BoundLiteralExpression)expression);
        }

        return null;
    }

    private object? EvaluateBinaryExpression(BoundBinaryExpression expression)
    {
        object? left = EvaluateExpression(expression.left);
        object? right = EvaluateExpression(expression.right);
        if (left is null || right is null)
        {
            return null;
        }

        return expression.op?.BinaryType switch
        {
            BoundBinaryType.ADDITION => (int)left + (int)right,
            BoundBinaryType.SUBTRACTION => (int)left - (int)right,
            BoundBinaryType.DIVISION => (int)left / (int)right,
            BoundBinaryType.MULTIPLICATION => (int)left * (int)right,
            BoundBinaryType.MOD => (int)left % (int)right,
            _ => null
        };
    }
    private object? EvaluateLiteral(BoundLiteralExpression expression)
    {
        if (expression.value is null) return null;
        if (int.TryParse((string)expression.value, out int v))
        {
            return v;
        }

        return 0;
    }
}