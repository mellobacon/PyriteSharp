using PyriteSharp.source.Syntax.Parser.Expressions;

namespace PyriteSharp.source.Syntax.Binder;

public class Binder
{
    public BoundExpression BindExpression(Expression expression)
    {
        return expression.Type switch
        {
            TokenType.BINARY_EXPRESSION => BindBinaryExpression((BinaryExpression)expression),
            TokenType.LITERAL_EXPRESSION => BindLiteralExpression((LiteralExpression)expression),
            _ => throw new Exception($"Unexpected syntax [{expression.Type}] (Binder)")
        };
    }

    BoundBinaryExpression BindBinaryExpression(BinaryExpression expression)
    {
        BoundExpression left = BindExpression(expression.left);
        BoundExpression right = BindExpression(expression.right);
        BoundBinaryOperator? op = BoundBinaryOperator.GetBinaryOperator(left.BoundType, expression.op.Type, right.BoundType);
        return new BoundBinaryExpression(left, op, right);
    }

    BoundLiteralExpression BindLiteralExpression(LiteralExpression expression)
    {
        object value = expression.value ?? 0;
        return new BoundLiteralExpression(value);
    }
}