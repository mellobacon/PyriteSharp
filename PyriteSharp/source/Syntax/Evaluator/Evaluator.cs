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
        return expression.op?.BinaryType switch
        {
            BoundBinaryType.ADDITION => leftvalue + rightvalue,
            BoundBinaryType.SUBTRACTION => leftvalue - rightvalue,
            BoundBinaryType.DIVISION => leftvalue / rightvalue,
            BoundBinaryType.MULTIPLICATION => leftvalue * rightvalue,
            BoundBinaryType.MOD => leftvalue % rightvalue,
            _ => null
        };
        /*
        if (left is double || right is double)
        {
            return expression.op?.BinaryType switch
            {
                BoundBinaryType.ADDITION => Convert.ToDouble(left) + Convert.ToDouble(right),
                BoundBinaryType.SUBTRACTION => Convert.ToDouble(left) - Convert.ToDouble(right),
                BoundBinaryType.DIVISION => Convert.ToDouble(left) / Convert.ToDouble(right),
                BoundBinaryType.MULTIPLICATION => Convert.ToDouble(left) * Convert.ToDouble(right),
                BoundBinaryType.MOD => Convert.ToDouble(left) % Convert.ToDouble(right),
                _ => null
            };
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
        */
    }
    private object? EvaluateLiteral(BoundLiteralExpression expression)
    {
        return expression.value;
    }
}