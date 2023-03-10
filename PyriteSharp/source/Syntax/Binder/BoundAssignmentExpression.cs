namespace PyriteSharp.source.Syntax.Binder;

public class BoundAssignmentExpression : BoundExpression
{
    public Variable Variable;
    public Token Op;
    public BoundExpression Expression;

    public bool IsCompound;
    public BoundAssignmentExpression(Variable v, Token op, BoundExpression expression, bool isCompound)
    {
        Variable = v;
        Op = op;
        Expression = expression;
        IsCompound = isCompound;
    }

    public override BoundType BoundType => BoundType.ASSIGNMENT;
    public override Type ValueType => Expression.ValueType;
}