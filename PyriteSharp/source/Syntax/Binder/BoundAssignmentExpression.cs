namespace PyriteSharp.source.Syntax.Binder;

public class BoundAssignmentExpression : BoundExpression
{
    public Variable Variable;
    public Token Op;
    public BoundExpression Expression;
    public BoundAssignmentExpression(Variable v, Token op, BoundExpression expression)
    {
        Variable = v;
        Op = op;
        Expression = expression;
    }

    public override BoundType BoundType => BoundType.ASSIGNMENT;
    public override Type ValueType => Expression.ValueType;
}