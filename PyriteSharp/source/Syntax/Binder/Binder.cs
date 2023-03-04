using PyriteSharp.source.Syntax.Parser.Expressions;

namespace PyriteSharp.source.Syntax.Binder;

public class Binder
{
    public readonly Dictionary<string, object?> Variables = new();
    public BoundExpression BindExpression(Expression expression)
    {
        return expression.Type switch
        {
            TokenType.ASSIGNMENT_EXPRESSION => BindAssignmentExpression((AssignmentExpression)expression),
            TokenType.GROUPED_EXPRESSION => BindGroupedExpression((GroupedExpression)expression),
            TokenType.BINARY_EXPRESSION => BindBinaryExpression((BinaryExpression)expression),
            TokenType.LITERAL_EXPRESSION => BindLiteralExpression((LiteralExpression)expression),
            _ => throw new Exception($"Unexpected syntax [{expression.Type}] (Binder)")
        };
    }

    private BoundExpression BindAssignmentExpression(AssignmentExpression expression)
    {
        BoundExpression e = BindExpression(expression.Expression);
        Variable variable = new Variable()
        {
            Name = expression.Variable.Text,
            Type = expression.GetType()
        };
        
        Variables.Add(variable.Name, null);
        Token op = expression.Op;
        return new BoundAssignmentExpression(variable, op, e);
    }

    private BoundExpression BindGroupedExpression(GroupedExpression expression)
    {
        return BindExpression(expression.Expression);
    }

    private BoundBinaryExpression BindBinaryExpression(BinaryExpression expression)
    {
        BoundExpression left = BindExpression(expression.Left);
        BoundExpression right = BindExpression(expression.Right);
        BoundBinaryOperator? op = BoundBinaryOperator.GetBinaryOperator(left.ValueType, expression.Op.Type, right.ValueType);
        return new BoundBinaryExpression(left, op, right);
    }

    private BoundLiteralExpression BindLiteralExpression(LiteralExpression expression)
    {
        object value = expression.Value ?? 0;
        return new BoundLiteralExpression(value);
    }
}