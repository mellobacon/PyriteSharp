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
        object? left = EvaluateExpression(expression.Left);
        object? right = EvaluateExpression(expression.Right);
        if (left is null || right is null)
        {
            return null;
        }

        dynamic? leftvalue = left switch
        {
            double d => d,
            float f => f,
            int i => i,
            _ => null
        };

        dynamic? rightvalue = right switch
        {
            double d => d,
            float f => f,
            int i => i,
            _ => null
        };
        return expression.Op?.BinaryType switch
        {
            BoundBinaryType.ADDITION => leftvalue + rightvalue,
            BoundBinaryType.SUBTRACTION => leftvalue - rightvalue,
            BoundBinaryType.DIVISION => leftvalue / rightvalue,
            BoundBinaryType.MULTIPLICATION => leftvalue * rightvalue,
            BoundBinaryType.MOD => leftvalue % rightvalue,
            BoundBinaryType.BITSHIFT_LEFT => leftvalue << rightvalue,
            BoundBinaryType.BITSHIFT_RIGHT => leftvalue >> rightvalue,
            _ => null
        };
    }
    private object? EvaluateLiteral(BoundLiteralExpression expression)
    {
        return expression.Value;
    }
}