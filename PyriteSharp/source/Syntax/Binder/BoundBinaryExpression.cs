namespace PyriteSharp.source.Syntax.Binder;

public class BoundBinaryExpression : BoundExpression
{
    public BoundExpression left;
    public BoundBinaryOperator? op;
    public BoundExpression right;
    public BoundBinaryExpression(BoundExpression left, BoundBinaryOperator? op, BoundExpression right)
    {
        this.left = left;
        this.op = op;
        this.right = right;
    }

    public override BoundType BoundType => BoundType.BINARY;
}