using PyriteSharp.source.Syntax.Parser.Expressions;

namespace PyriteSharp.source.Syntax.Binder;

public class Binder
{
    public BoundExpression BindExpression(Expression expression)
    {
        return expression.Type switch
        {
            TokenType.GROUPED_EXPRESSION => BindGroupedExpression((GroupedExpression)expression),
            TokenType.BINARY_EXPRESSION => BindBinaryExpression((BinaryExpression)expression),
            TokenType.LITERAL_EXPRESSION => BindLiteralExpression((LiteralExpression)expression),
            _ => throw new Exception($"Unexpected syntax [{expression.Type}] (Binder)")
        };
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