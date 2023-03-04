using PyriteSharp.source.Syntax.Binder;

namespace PyriteSharp.source.Syntax.Evaluator;

public class Evaluator
{
    private readonly Dictionary<string, object?> _variables;
    private object? value;
    private readonly BoundExpression _root;

    public Evaluator(BoundExpression expression, Dictionary<string, object?> variables)
    {
        _root = expression;
        _variables = variables;
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
            case BoundType.ASSIGNMENT:
                return EvaluateAssignmentExpression((BoundAssignmentExpression)expression);
            case BoundType.BINARY:
                return EvaluateBinaryExpression((BoundBinaryExpression)expression);
            case BoundType.LITERAL:
                return EvaluateLiteral((BoundLiteralExpression)expression);
        }

        return null;
    }

    private object? EvaluateAssignmentExpression(BoundAssignmentExpression expression)
    {
        if (_variables[expression.Variable.Name] == null)
        {
            object? v = EvaluateExpression(expression.Expression);
            _variables[expression.Variable.Name] = v;
            return v;
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
            bool b => b,
            _ => null
        };

        dynamic? rightvalue = right switch
        {
            double d => d,
            float f => f,
            int i => i,
            bool b => b,
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
            BoundBinaryType.BITWISE_OR => leftvalue | rightvalue,
            BoundBinaryType.BITWISE_AND => leftvalue & rightvalue,
            BoundBinaryType.BITWISE_EXCLUSIVE_OR => leftvalue ^ rightvalue,
            BoundBinaryType.LOGICAL_OR => leftvalue || rightvalue,
            BoundBinaryType.LOGICAL_AND => leftvalue && rightvalue,
            BoundBinaryType.LOGICAL_EQUALS => leftvalue == rightvalue,
            BoundBinaryType.LOGICAL_NOT_EQUALS => leftvalue != rightvalue,
            BoundBinaryType.LESS_THAN => leftvalue < rightvalue,
            BoundBinaryType.MORE_THAN => leftvalue > rightvalue,
            _ => null
        };
    }
    private object? EvaluateLiteral(BoundLiteralExpression expression)
    {
        return expression.Value;
    }
}