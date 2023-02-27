namespace PyriteSharp.source.Syntax.Binder;

public class BoundBinaryExpression : BoundExpression
{
    public readonly BoundExpression Left;
    public readonly BoundBinaryOperator? Op;
    public readonly BoundExpression Right;
    public BoundBinaryExpression(BoundExpression left, BoundBinaryOperator? op, BoundExpression right)
    {
        Left = left;
        Op = op;
        Right = right;
    }

    public override Type ValueType => Op!.ExpectedType;
    public override BoundType BoundType => BoundType.BINARY;
}